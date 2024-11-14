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

        public async void Copy()
        {
            try
            {
                YandexDiskService yandexDiskService = new YandexDiskService(_configuration);
                await yandexDiskService.DownloadFileAsync(_directory);

                using StreamReader streamReader = new StreamReader(_csvFile);

                List<Passport> csvPassports = new List<Passport>();

                string? line = streamReader.ReadLine();
                
                while ((line = streamReader.ReadLine()) != null)
                {
                    Passport? passport = CsvParserService.Parse(line);
                    if (passport != null)
                    {
                        csvPassports.Add(passport);
                    }
                }

                var activePassports = _context.InactivePassports.ToList().Except(csvPassports);
                Console.WriteLine(activePassports);
                if (activePassports != null)
                {
                    foreach (Passport passport in activePassports)
                    {
                        PassportHistory passportHistory = new PassportHistory()
                        {
                            Id = _context.PassportHistory.Count(),
                            PassportNumber = passport.Number,
                            PassportSeries = passport.Series,
                            Passport = passport,
                            ActiveStart = DateTime.Now
                        };

                        _context.PassportHistory.Add(passportHistory);
                    }
                    Console.WriteLine("count of active: {0}", activePassports.Count());
                    _context.SaveChanges();
                }

                var inactivePassports = csvPassports.Except(_context.InactivePassports.ToList());
                if (inactivePassports != null)
                {
                    foreach (Passport passport in inactivePassports)
                    {
                        Passport? existingPassport = _context.InactivePassports.Find(passport.Series, passport.Number);
                        if (existingPassport != null)
                        {
                            existingPassport.IsActive = false;
                            PassportHistory passportHistory = _context.PassportHistory.Last(p => (p.PassportSeries == passport.Series) && (p.PassportNumber == passport.Number));
                            passportHistory.ActiveEnd = DateTime.Now;
                        }
                        else
                        {
                            _context.InactivePassports.Add(passport);
                        }
                    }
                    Console.WriteLine("count of inactive: {0}", inactivePassports.Count());
                    _context.SaveChanges();
                }
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
