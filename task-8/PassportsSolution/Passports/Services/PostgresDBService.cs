using Passports.Database;
using Passports.Models;
using Passports.Services.Interfaces;

namespace Passports.Services
{
    public class PostgresDBService : IDBService
    {
        private readonly ApplicationContext _context;

        public PostgresDBService(ApplicationContext context)
        {
            _context = context;
        }

        public Passport? GetPassport(short series, int number)
        {
            return _context.InactivePassports.Find(series, number);
        }
    }
}
