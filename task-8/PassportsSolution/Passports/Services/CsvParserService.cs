using Passports.Models;

namespace Passports.Services
{
    public static class CsvParserService
    {
        public static Passport? Parse(string inputString)
        {
            string[] csv = inputString.Split(',');

            if (short.TryParse(csv[0], out short series) && int.TryParse(csv[1], out int number))
            {
                return new Passport() { Series = series, Number = number, IsActive = false };
            }

            return null;
        }
    }
}
