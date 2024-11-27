using Passports.Models;
using Passports.Models.DTO;

namespace Passports.Services.Interfaces
{
    public interface IDBService
    {
        public Passport? GetPassport(short series, int number);
        public UssrPassport? GetUssrPassport(string series, int number);
        public List<PassportChanges> GetPassportHistory(Passport passport);
        public List<PassportChanges> GetUssrPassportHistory(UssrPassport passport);
        public Dictionary<string, List<PassportChanges>> GetPassportsHistoriesByDate(DateOnly startDate, DateOnly endDate);
        public Dictionary<string, List<PassportChanges>> GetUssrPassportsHistoriesByDate(DateOnly startDate, DateOnly endDate);

        public void Copy();
    }
}
