using Passports.Database;
using Passports.Models;
using Passports.Services.Interfaces;

namespace Passports.Services
{
    public class PostgresDBService : IDBService
    {
        private readonly ApplicationContext _context;

        private string _path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Converter", "data1", "Data1.csv");

        public PostgresDBService(ApplicationContext context)
        {
            _context = context;
        }

        public Passport? GetPassport(short series, int number)
        {
            return _context.InactivePassports.Find(series, number);
        }

        public void Copy()
        {
            try
            {
                using StreamReader streamReader = new StreamReader(_path);

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
