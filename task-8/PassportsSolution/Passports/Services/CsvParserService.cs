using Passports.Models;

namespace Passports.Services
{
    public static class CsvParserService
    {
        private const int SERIES_LENGTH = 4;
        private const int NUMBER_LENGTH = 6;

        public static Passport? Parse(string inputString)
        {
            string[] csv = inputString.Split(',');

            if ((csv[0].Length == SERIES_LENGTH) && (csv[1].Length == NUMBER_LENGTH) && short.TryParse(csv[0], out short series) && int.TryParse(csv[1], out int number))
            {
                return new Passport() { Series = series, Number = number, IsActive = false };
            }

            return null;
        }
    }
}
