﻿using EFCore.BulkExtensions;
using Microsoft.Extensions.Options;
using Passports.Converter;
using Passports.Database;
using Passports.Exceptions;
using Passports.Models;
using Passports.Models.DTO;
using Passports.Models.Extensions;
using Passports.Options;
using Passports.Services.Interfaces;
using System.Text.RegularExpressions;

namespace Passports.Services
{
    /// <summary>
    /// Represents PostgreSQL service.
    /// </summary>
    public class PostgresDBService : IDBService
    {
        private readonly ApplicationContext _context;
        private AppSettings? _appSettings;
        private YandexDiskService? _yandexDiskService;

        private readonly int _gmtOffset;
        private readonly int _batchSize;

        private static string _directory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Converter", "data1");
        private static string _csvFile = Path.Combine(_directory, "Data1.csv");

        private static Regex SeriesRegex { get; } = new(@"^\d{4}$");
        private static Regex UssrSeriesRegex { get; } = new(@"^(I|X|V){1,6}-[А-Я]{2}$");
        private static Regex NumberRegex { get; } = new(@"^\d{6}$");

        /// <summary>
        /// PostgresDBService constructor.
        /// </summary>
        /// <param name="context">DbContext of the application.</param>
        /// <param name="options">AppSettings section values.</param>
        public PostgresDBService(ApplicationContext context, IOptions<AppSettings> options)
        {
            _context = context;
            _appSettings = options.Value;
            _yandexDiskService = new YandexDiskService(options);

            try
            {
                string? gmtOffsetValue = _appSettings.GmtOffset;
                if (string.IsNullOrWhiteSpace(gmtOffsetValue))
                {
                    throw new EmptyConfigurationSectionException(_appSettings.GmtOffset.GetType().Name);
                }
                if (!int.TryParse(gmtOffsetValue, out int gmtOffset))
                {
                    throw new ParseException();
                }

                _gmtOffset = gmtOffset;

                string? batchSizeValue = _appSettings.BatchSize;
                if (string.IsNullOrWhiteSpace(batchSizeValue))
                {
                    throw new EmptyConfigurationSectionException(_appSettings.BatchSize.GetType().Name);
                }
                if (!int.TryParse(batchSizeValue, out int batchSize))
                {
                    throw new ParseException();
                }

                _batchSize = batchSize;
            }
            catch (Exception ex) when (ex is ParseException || ex is EmptyConfigurationSectionException)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public Passport? GetPassport(string series, string number)
        {
            return _context.InactivePassports.Find(short.Parse(series), int.Parse(number));
        }

        public UssrPassport? GetUssrPassport(string series, string number)
        {
            return _context.InactiveUssrPassports.Find(series, int.Parse(number));
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
            
            var passportHistories = _context.PassportHistory.ToList().OrderBy(p => p.Id).Where(p => (p.ActiveStart >= startDate) && ((p.ActiveEnd ?? DateOnly.FromDateTime(DateTime.Now)) <= endDate)).GroupBy(p => new PassportOnly() { Series = p.PassportSeries.ToString(), Number = p.PassportNumber });
            foreach (var group in passportHistories)
            {
                var keyValuePair = new KeyValuePair<PassportOnly, List<PassportChanges>>(group.Key, CreatePassportHistory(group.ToList()));
                passportsHistoriesByDate.Add(keyValuePair);
            }

            return passportsHistoriesByDate;
        }

        public List<KeyValuePair<PassportOnly, List<PassportChanges>>> GetUssrPassportsHistoriesByDate(DateOnly startDate, DateOnly endDate)
        {
            var passportsHistoriesByDate = new List<KeyValuePair<PassportOnly, List<PassportChanges>>>();

            var passportHistories = _context.UssrPassportHistory.OrderBy(p => p.Id).Where(p => (p.ActiveStart >= startDate) && ((p.ActiveEnd ?? DateOnly.FromDateTime(DateTime.Now)) <= endDate)).GroupBy(p => new PassportOnly() { Series = p.PassportSeries, Number = p.PassportNumber });
            foreach (var group in passportHistories)
            {
                var keyValuePair = new KeyValuePair<PassportOnly, List<PassportChanges>>(group.Key, CreatePassportHistory(group.ToList()));
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

        public bool CheckPassportFormat(string series, string number) => SeriesRegex.IsMatch(series) && NumberRegex.IsMatch(number);

        public bool CheckUssrPassportFormat(string series, string number) => UssrSeriesRegex.IsMatch(series) && NumberRegex.IsMatch(number);

        public async void Copy()
        {
            try
            {
                if (_yandexDiskService == null)
                {
                    throw new YandexDiskException();
                }
                await _yandexDiskService.DownloadFileAsync(_directory);

                using StreamReader streamReader = new StreamReader(_csvFile);

                List<Passport> csvPassports = new List<Passport>();
                List<UssrPassport> csvUssrPassports = new List<UssrPassport>();
                List<Passport> inactivePassportsFromDb = _context.InactivePassports.ToList();
                List<UssrPassport> inactiveUssrPassportsFromDb = _context.InactiveUssrPassports.ToList();

                bool next = true;
                string? line = await streamReader.ReadLineAsync();
                while (next)
                {
                    for (int j = 0; j < _batchSize; j++)
                    {
                        if ((line = await streamReader.ReadLineAsync()) == null)
                        {
                            next = false;
                            break;
                        }

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

                    csvPassports.Clear();
                    csvUssrPassports.Clear();
                }
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (YandexDiskException)
            {
                Console.WriteLine("Access to the Yandex Disk is denied.");
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

                await BulkInsertExtension<PassportHistory>.BulkInsertByBatchesAsync(_context, passportHistories, _batchSize);
            }
        }

        private async Task AddInactivePassports(List<Passport> inactivePassportsFromDb, List<Passport> csvPassports)
        {
            var inactivePassports = csvPassports.Except(inactivePassportsFromDb);
            
            if (inactivePassports.Any())
            {
                await BulkInsertExtension<Passport>.BulkInsertByBatchesAsync(_context, inactivePassports, _batchSize);
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

                await BulkInsertExtension<UssrPassportHistory>.BulkInsertByBatchesAsync(_context, ussrPassportHistories, _batchSize);
            }
        }

        private async Task AddInactiveUssrPassports(List<UssrPassport> inactiveUssrPassportsFromDb, List<UssrPassport> csvUssrPassports)
        {
            var inactiveUssrPassports = csvUssrPassports.Except(inactiveUssrPassportsFromDb);

            if (inactiveUssrPassports.Any())
            {
                await BulkInsertExtension<UssrPassport>.BulkInsertByBatchesAsync(_context, inactiveUssrPassports, _batchSize);
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
