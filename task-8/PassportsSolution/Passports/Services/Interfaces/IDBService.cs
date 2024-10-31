using Passports.Models;

namespace Passports.Services.Interfaces
{
    public interface IDBService
    {
        public Passport? GetPassport(short series, int number);
    }
}
