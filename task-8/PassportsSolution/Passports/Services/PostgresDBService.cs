using EFCore.BulkExtensions;
using Passports.Converter;
using Passports.Database;
using Passports.Exceptions;
using Passports.Models;
using Passports.Models.DTO;
using Passports.Models.Extensions;
using Passports.Services.Interfaces;

namespace Passports.Services
{
    /// <summary>
    /// Represents PostgreSQL service.
    /// </summary>
    public class PostgresDBService : IDBService
    {
        private readonly ApplicationContext _context;
        private readonly IConfiguration _configuration;
        private readonly int _gmtOffset;

        private readonly string _gmtOffsetSection = "GmtOffset";

        private static string _directory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Converter", "data1");
        private static string _csvFile = Path.Combine(_directory, "Data1.csv");

        /// <summary>
        /// PostgresDBService constructor.
        /// </summary>
        /// <param name="context">DbContext of the application.</param>
        /// <param name="configuration">A set of key/value application configuration properties.</param>
        public PostgresDBService(ApplicationContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
            try
            {
                string? gmtOffsetValue = configuration.GetSection(_gmtOffsetSection).Value;
                if (string.IsNullOrWhiteSpace(gmtOffsetValue))
                {
                    throw new EmptyConfigurationSectionException(_gmtOffsetSection);
                }
                if (!int.TryParse(gmtOffsetValue, out int gmtOffset))
                {
                    throw new ParseException();
                }

                _gmtOffset = gmtOffset;
            }
            catch (ParseException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (EmptyConfigurationSectionException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public Passport? GetPassport(short series, int number)
        {
            return _context.InactivePassports.Find(series, number);
        }

        public UssrPassport? GetUssrPassport(string series, int number)
        {
            return _context.InactiveUssrPassports.Find(series, number);
        }

        public List<PassportChanges> GetPassportHistory(Passport passport)
        {
            List<PassportChanges> passportChanges = new List<PassportChanges>();
            List<PassportHistory> passportHistories = _context.PassportHistory.OrderBy(p => p.Id).Where(p => p.PassportSeries == passport.Series && p.PassportNumber == passport.Number).ToList();
            
            if (passportHistories.Any())
            {
                passportChanges = CreatePassportHistory(passportHistories);
            }

            return passportChanges;
        }

        public List<PassportChanges> GetUssrPassportHistory(UssrPassport passport)
        {
            List<PassportChanges> passportChanges = new List<PassportChanges>();
            List<UssrPassportHistory> passportHistories = _context.UssrPassportHistory.OrderBy(p => p.Id).Where(p => p.PassportSeries == passport.Series && p.PassportNumber == passport.Number).ToList();

            if (passportHistories.Any())
            {
                passportChanges = CreatePassportHistory(passportHistories);
            }

            return passportChanges;
        }

        public List<KeyValuePair<PassportOnly, List<PassportChanges>>> GetPassportsHistoriesByDate(DateOnly startDate, DateOnly endDate)
        {
            var passportsHistoriesByDate = new List<KeyValuePair<PassportOnly, List<PassportChanges>>>();
            
            var passportHistories = _context.PassportHistory.ToList().OrderBy(p => p.Id).Where(p => (p.ActiveStart >= startDate) && ((p.ActiveEnd ?? DateOnly.FromDateTime(DateTime.Now)) <= endDate)).GroupBy(p => new PassportOnly() { Series = p.PassportSeries, Number = p.PassportNumber });
            foreach (var group in passportHistories)
            {
                var keyValuePair = new KeyValuePair<PassportOnly, List<PassportChanges>>(group.Key, CreatePassportHistory(group.ToList()));
                passportsHistoriesByDate.Add(keyValuePair);
            }

            return passportsHistoriesByDate;
        }

        public List<KeyValuePair<UssrPassportOnly, List<PassportChanges>>> GetUssrPassportsHistoriesByDate(DateOnly startDate, DateOnly endDate)
        {
            var passportsHistoriesByDate = new List<KeyValuePair<UssrPassportOnly, List<PassportChanges>>>();

            var passportHistories = _context.UssrPassportHistory.OrderBy(p => p.Id).Where(p => (p.ActiveStart >= startDate) && ((p.ActiveEnd ?? DateOnly.FromDateTime(DateTime.Now)) <= endDate)).GroupBy(p => new UssrPassportOnly() { Series = p.PassportSeries, Number = p.PassportNumber });
            foreach (var group in passportHistories)
            {
                var keyValuePair = new KeyValuePair<UssrPassportOnly, List<PassportChanges>>(group.Key, CreatePassportHistory(group.ToList()));
                passportsHistoriesByDate.Add(keyValuePair);
            }

            return passportsHistoriesByDate;
        }

        private List<PassportChanges> CreatePassportHistory(List<PassportHistory> passportHistories)
        {
            List<PassportChanges> passportChanges = new List<PassportChanges>();

            for (int i = 0; i < passportHistories.Count - 1; i++)
            {
                Console.WriteLine(passportHistories[i].ActiveStart);
                passportChanges.Add(new PassportChanges() { Start = passportHistories[i].ActiveStart, End = passportHistories[i].ActiveEnd, IsActive = true });
                passportChanges.Add(new PassportChanges() { Start = passportHistories[i].ActiveEnd, End = passportHistories[i + 1].ActiveStart, IsActive = false });
            }

            DateOnly? lastActiveEnd = passportHistories.Last().ActiveEnd;
            if (lastActiveEnd != null)
            {
                passportChanges.Add(new PassportChanges() { Start = passportHistories.Last().ActiveStart, End = passportHistories.Last().ActiveEnd, IsActive = true });
                passportChanges.Add(new PassportChanges() { Start = passportHistories.Last().ActiveEnd, End = DateOnly.FromDateTime(DateTime.Now.ToUniversalTime().AddHours(_gmtOffset)), IsActive = false });
            }
            else
            {
                passportChanges.Add(new PassportChanges() { Start = passportHistories.Last().ActiveStart, End = DateOnly.FromDateTime(DateTime.Now.ToUniversalTime().AddHours(_gmtOffset)), IsActive = true });
            }
            
            return passportChanges;
        }

        private List<PassportChanges> CreatePassportHistory(List<UssrPassportHistory> passportHistories)
        {
            List<PassportChanges> passportChanges = new List<PassportChanges>();

            for (int i = 0; i < passportHistories.Count - 1; i++)
            {
                passportChanges.Add(new PassportChanges() { Start = passportHistories[i].ActiveStart, End = passportHistories[i].ActiveEnd, IsActive = true });
                passportChanges.Add(new PassportChanges() { Start = passportHistories[i].ActiveEnd, End = passportHistories[i + 1].ActiveStart, IsActive = false });
            }

            DateOnly? lastActiveEnd = passportHistories.Last().ActiveEnd;
            if (lastActiveEnd != null)
            {
                passportChanges.Add(new PassportChanges() { Start = passportHistories.Last().ActiveStart, End = passportHistories.Last().ActiveEnd, IsActive = true });
                passportChanges.Add(new PassportChanges() { Start = passportHistories.Last().ActiveEnd, End = DateOnly.FromDateTime(DateTime.Now.ToUniversalTime().AddHours(_gmtOffset)), IsActive = false });
            }
            else
            {
                passportChanges.Add(new PassportChanges() { Start = passportHistories.Last().ActiveStart, End = DateOnly.FromDateTime(DateTime.Now.ToUniversalTime().AddHours(_gmtOffset)), IsActive = true });
            }

            return passportChanges;
        }

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
                        ActiveStart = DateOnly.FromDateTime(DateTime.Now.ToUniversalTime().AddHours(_gmtOffset))
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
                    passportHistory.ActiveEnd = DateOnly.FromDateTime(DateTime.Now.ToUniversalTime().AddHours(_gmtOffset));
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
                        ActiveStart = DateOnly.FromDateTime(DateTime.Now.ToUniversalTime().AddHours(_gmtOffset))
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
                    ussrPassportHistory.ActiveEnd = DateOnly.FromDateTime(DateTime.Now.ToUniversalTime().AddHours(_gmtOffset));
                }
            }
        }
    }
}
