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
                await yandexDiskService.DownloadFile(_directory);

                using StreamReader streamReader = new StreamReader(_csvFile);

                string? line = streamReader.ReadLine();
                while ((line = streamReader.ReadLine()) != null)
                {
                    Passport? passport = CsvParserService.Parse(line);

                    if (passport != null)
                    {
                        Passport? existingPassport = _context.InactivePassports.Find(passport.Series, passport.Number);
                        if (existingPassport != null)
                        {
                            existingPassport.IsActive = false;
                        }
                        else
                        {
                            _context.InactivePassports.Add(passport);
                        }
                    }
                }

                _context.SaveChanges();
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
