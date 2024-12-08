namespace Passports.Models.DTO
{
    /// <summary>
    /// Contains only the passport series and number.
    /// </summary>
    public class PassportOnly
    {
        /// <summary>
        /// The passport series.
        /// </summary>
        public short Series { get; set; }

        /// <summary>
        /// The passport number.
        /// </summary>
        public int Number { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj is Passport passport)
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
