namespace Passports.Models.DTO
{
    /// <summary>
    /// Contains only the USSR passport series and number.
    /// </summary>
    public class UssrPassportOnly
    {
        /// <summary>
        /// The passport series.
        /// </summary>
        public string Series { get; set; } = null!;

        /// <summary>
        /// The passport number.
        /// </summary>
        public int Number { get; set; }
       
        public override bool Equals(object? obj)
        {
            if (obj is UssrPassport passport)
            {
                return passport.Series == Series && passport.Number == Number;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Series, Number);
        }
    }
}
