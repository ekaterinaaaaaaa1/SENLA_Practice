namespace Passports.Models
{
    /// <summary>
    /// Represent a Russian passport entity.
    /// </summary>
    public class Passport : BasePassport
    {
        /// <summary>
        /// The passport series.
        /// </summary>
        public short Series { get; set; }

        /// <summary>
        /// The navigation property for the passport history collection.
        /// </summary>
        public ICollection<PassportHistory> PassportHistories { get; set; } = null!;

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
