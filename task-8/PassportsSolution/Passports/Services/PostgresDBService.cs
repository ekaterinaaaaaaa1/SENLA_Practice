using EFCore.BulkExtensions;
using Passports.Converter;
using Passports.Database;
using Passports.Models;
using Passports.Models.Extensions;
using Passports.Services.Interfaces;

namespace Passports.Services
{
    public class PostgresDBService : IDBService
    {
        private readonly ApplicationContext _context;
        private readonly IConfiguration _configuration;

        private static string _directory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Converter", "data1");
        private static string _csvFile = Path.Combine(_directory, "Data1.csv");

        public PostgresDBService(ApplicationContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public Passport? GetPassport(short series, int number)
        {
            return _context.InactivePassports.Find(series, number);
        }

        /*public List<PassportHistory> GetPassportHistory(Passport passport)
        {
            return _context.PassportHistory.OrderBy(p => p.Id).Where(p => p.PassportSeries == passport.Series && p.PassportNumber == passport.Number).ToList();
        }*/

        public async void Copy()
        {
            try
            {
                YandexDiskService yandexDiskService = new YandexDiskService(_configuration);
                await yandexDiskService.DownloadFileAsync(_directory);

                using StreamReader streamReader = new StreamReader(_csvFile);

                List<Passport> csvPassports = new List<Passport>();
                List<UssrPassport> csvUssrPassports = new List<UssrPassport>();
                List<Passport> inactivePassportsFromDb = _context.InactivePassports.ToList();
                List<UssrPassport> inactiveUssrPassportsFromDb = _context.InactiveUssrPassports.ToList();

                string? line = await streamReader.ReadLineAsync();
                while ((line = await streamReader.ReadLineAsync()) != null)
                {
                    object? parsedLine = CsvParserService.Parse(line);
                    if (parsedLine != null)
                    {
                        if (parsedLine is Passport)
                        {
                            csvPassports.Add((Passport)parsedLine);
                        }
                        else
                        {
                            csvUssrPassports.Add((UssrPassport)parsedLine);
                        }
                    }
                }
                
                await AddActivePassports(inactivePassportsFromDb, csvPassports);
                await AddInactivePassports(inactivePassportsFromDb, csvPassports);
                AddFromActiveToInactivePassports(inactivePassportsFromDb, csvPassports);

                await AddActiveUssrPassports(inactiveUssrPassportsFromDb, csvUssrPassports);
                await AddInactiveUssrPassports(inactiveUssrPassportsFromDb, csvUssrPassports);
                AddFromActiveToInactiveUssrPassports(inactiveUssrPassportsFromDb, csvUssrPassports);

                await _context.BulkSaveChangesAsync();
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private async Task AddActivePassports(List<Passport> inactivePassportsFromDb, List<Passport> csvPassports)
        {
            var activePassports = inactivePassportsFromDb.Except(csvPassports).ToList();

            if (activePassports.Any())
            {
                List<PassportHistory> passportHistories = new List<PassportHistory>();

                foreach (Passport passport in activePassports)
                {
                    passport.IsActive = true;

                    PassportHistory passportHistory = new PassportHistory()
                    {
                        PassportNumber = passport.Number,
                        PassportSeries = passport.Series,
                        Passport = passport,
                        ActiveStart = DateTime.Now
                    };

                    passportHistories.Add(passportHistory);
                }

                await BulkInsertExtension<PassportHistory>.BulkInsertByBatchesAsync(_context, passportHistories);
            }
        }

        private async Task AddInactivePassports(List<Passport> inactivePassportsFromDb, List<Passport> csvPassports)
        {
            var inactivePassports = csvPassports.Except(inactivePassportsFromDb);
            
            if (inactivePassports.Any())
            {
                await BulkInsertExtension<Passport>.BulkInsertByBatchesAsync(_context, inactivePassports);
            }
        }

        private void AddFromActiveToInactivePassports(List<Passport> inactivePassportsFromDb, List<Passport> csvPassports)
        {
            var fromActiveToInactivePassports = inactivePassportsFromDb.Where(p => p.IsActive).Intersect(csvPassports);

            if (fromActiveToInactivePassports.Any())
            {
                foreach (Passport passport in fromActiveToInactivePassports)
                {
                    passport.IsActive = false;

                    PassportHistory passportHistory = _context.PassportHistory.OrderBy(p => p.Id).Last(p => (p.PassportSeries == passport.Series) && (p.PassportNumber == passport.Number));
                    passportHistory.ActiveEnd = DateTime.Now;
                }
            }
        }

        private async Task AddActiveUssrPassports(List<UssrPassport> inactiveUssrPassportsFromDb, List<UssrPassport> csvUssrPassports)
        {
            var activeUssrPassports = inactiveUssrPassportsFromDb.Except(csvUssrPassports).ToList();

            if (activeUssrPassports.Any())
            {
                List<UssrPassportHistory> ussrPassportHistories = new List<UssrPassportHistory>();

                foreach (UssrPassport ussrPassport in activeUssrPassports)
                {
                    ussrPassport.IsActive = true;

                    UssrPassportHistory ussrPassportHistory = new UssrPassportHistory()
                    {
                        PassportNumber = ussrPassport.Number,
                        PassportSeries = ussrPassport.Series,
                        UssrPassport = ussrPassport,
                        ActiveStart = DateTime.Now
                    };

                    ussrPassportHistories.Add(ussrPassportHistory);
                }

                await BulkInsertExtension<UssrPassportHistory>.BulkInsertByBatchesAsync(_context, ussrPassportHistories);
            }
        }

        private async Task AddInactiveUssrPassports(List<UssrPassport> inactiveUssrPassportsFromDb, List<UssrPassport> csvUssrPassports)
        {
            var inactiveUssrPassports = csvUssrPassports.Except(inactiveUssrPassportsFromDb);

            if (inactiveUssrPassports.Any())
            {
                await BulkInsertExtension<UssrPassport>.BulkInsertByBatchesAsync(_context, inactiveUssrPassports);
            }
        }

        private void AddFromActiveToInactiveUssrPassports(List<UssrPassport> inactiveUssrPassportsFromDb, List<UssrPassport> csvUssrPassports)
        {
            var fromActiveToInactiveUssrPassports = inactiveUssrPassportsFromDb.Where(p => p.IsActive).Intersect(csvUssrPassports);

            if (fromActiveToInactiveUssrPassports.Any())
            {
                foreach (UssrPassport ussrPassport in fromActiveToInactiveUssrPassports)
                {
                    ussrPassport.IsActive = false;

                    UssrPassportHistory ussrPassportHistory = _context.UssrPassportHistory.OrderBy(p => p.Id).Last(p => (p.PassportSeries == ussrPassport.Series) && (p.PassportNumber == ussrPassport.Number));
                    ussrPassportHistory.ActiveEnd = DateTime.Now;
                }
            }
        }
    }
}
