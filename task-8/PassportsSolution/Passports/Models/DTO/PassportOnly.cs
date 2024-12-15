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
        public string Series { get; set; } = null!;

        /// <summary>
        /// The passport number.
        /// </summary>
        public int Number { get; set; }

        public override bool Equals(object? obj)
        {
            if ((obj is Passport passport) && short.TryParse(Series, out short series))
            {
                return passport.Series == series && passport.Number == Number;
            }
            if (obj is UssrPassport ussrPassport)
            {
                return ussrPassport.Series == Series && ussrPassport.Number == Number;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Series, Number);
        }
    }
}
