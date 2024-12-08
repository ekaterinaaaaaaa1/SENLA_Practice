using Passports.Models;
using Passports.Models.DTO;

namespace Passports.Services.Interfaces
{
    public interface IDBService
    {
        /// <summary>
        /// Gets a passport and its activity by series and number.
        /// </summary>
        /// <param name="series">The passport series.</param>
        /// <param name="number">The passport number.</param>
        /// <returns>Returns the Passport object or null if it does not exist.</returns>
        public Passport? GetPassport(string series, string number);

        /// <summary>
        /// Gets a USSR passport and its activity by series and number.
        /// </summary>
        /// <param name="series">The passport series.</param>
        /// <param name="number">The passport number.</param>
        /// <returns>Returns the UssrPassport object or null if it does not exist</returns>
        public UssrPassport? GetUssrPassport(string series, string number);

        /// <summary>
        /// Gets a passport changes list.
        /// </summary>
        /// <param name="passport">The Passport object to get the passport history.</param>
        /// <returns>Returns the list of the passport changes history.</returns>
        public List<PassportChanges> GetPassportHistory(Passport passport);

        /// <summary>
        /// Gets a USSR passport changes list.
        /// </summary>
        /// <param name="passport">The UssrPassport object to get the passport history</param>
        /// <returns>Returns the list of the passport changes history.</returns>
        public List<PassportChanges> GetUssrPassportHistory(UssrPassport passport);

        /// <summary>
        /// Gets all the passports changes for the period.
        /// </summary>
        /// <param name="startDate">Start date of the period.</param>
        /// <param name="endDate">End date of the period.</param>
        /// <returns>Returns the list of the passports changes histories.</returns>
        public List<KeyValuePair<PassportOnly, List<PassportChanges>>> GetPassportsHistoriesByDate(DateOnly startDate, DateOnly endDate);

        /// <summary>
        /// Gets all the USSR passports changes for the period.
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public List<KeyValuePair<UssrPassportOnly, List<PassportChanges>>> GetUssrPassportsHistoriesByDate(DateOnly startDate, DateOnly endDate);

        /// <summary>
        /// Uploads data from CSV to the database.
        /// </summary>
        public void Copy();
    }
}
