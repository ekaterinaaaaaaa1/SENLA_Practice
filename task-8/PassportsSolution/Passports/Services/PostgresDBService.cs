using EFCore.BulkExtensions;
using Passports.Converter;
using Passports.Database;
using Passports.Models;
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
                List<Passport> inactivePassportsFromDb = _context.InactivePassports.ToList();

                string? line = await streamReader.ReadLineAsync();
                while ((line = await streamReader.ReadLineAsync()) != null)
                {
                    Passport? passport = CsvParserService.Parse(line);
                    if (passport != null)
                    {
                        csvPassports.Add(passport);
                    }
                }

                AddActivePassports(inactivePassportsFromDb, csvPassports);
                AddInactivePassports(inactivePassportsFromDb, csvPassports);
                AddFromActiveToInactivePassports(inactivePassportsFromDb, csvPassports);

                await _context.BulkSaveChangesAsync();
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void AddActivePassports(List<Passport> inactivePassportsFromDb, List<Passport> csvPassports)
        {
            var activePassports = inactivePassportsFromDb.Except(csvPassports, new Passport());

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
                    //_context.PassportHistory.Add(passportHistory);
                }

                _context.BulkInsert(passportHistories);
            }
        }

        private void AddInactivePassports(List<Passport> inactivePassportsFromDb, List<Passport> csvPassports)
        {
            var inactivePassports = csvPassports.Except(inactivePassportsFromDb, new Passport());

            _context.BulkInsert(inactivePassports);

            /*if (inactivePassports.Any())
            {
                foreach (Passport passport in inactivePassports)
                {
                    _context.InactivePassports.Add(passport);
                }
            }*/
        }

        private void AddFromActiveToInactivePassports(List<Passport> inactivePassportsFromDb, List<Passport> csvPassports)
        {
            var fromActiveToInactivePassports = inactivePassportsFromDb.Where(p => p.IsActive).Intersect(csvPassports, new Passport());

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
    }
}
