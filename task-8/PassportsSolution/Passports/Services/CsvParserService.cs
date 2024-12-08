using Passports.Models;

namespace Passports.Services
{
    /// <summary>
    /// Returns CSV parser service.
    /// </summary>
    public static class CsvParserService
    {
        private const int SERIES_LENGTH = 4;
        private const int USSR_MAX_SERIES_LENGTH = 9;
        private const int NUMBER_LENGTH = 6;

        /// <summary>
        /// parses an input string to create Passport or UssrPassport object.
        /// </summary>
        /// <param name="inputString">An input string to parse.</param>
        /// <returns>Returns an object if the string has been parsed or null overwise.</returns>
        public static object? Parse(string inputString)
        {
            string[] csv = inputString.Split(',');
            int seriesLength = csv[0].Length;
            int numberLength = csv[1].Length;

            if ((seriesLength == SERIES_LENGTH) && short.TryParse(csv[0], out short series) && (numberLength == NUMBER_LENGTH) && int.TryParse(csv[1], out int number))
            {
                return new Passport() { Series = series, Number = number, IsActive = false };
            }

            if ((seriesLength >= SERIES_LENGTH) && (seriesLength <= USSR_MAX_SERIES_LENGTH) && (numberLength == NUMBER_LENGTH) && int.TryParse(csv[1], out int ussrNumber))
            {
                return new UssrPassport() { Series = csv[0], Number = ussrNumber, IsActive = false };
            }

            return null;
        }
    }
}
