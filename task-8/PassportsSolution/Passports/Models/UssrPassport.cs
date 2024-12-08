namespace Passports.Models
{
    /// <summary>
    /// Represents a USSR passport entity.
    /// </summary>
    public class UssrPassport
    {
        /// <summary>
        /// The passport series.
        /// </summary>
        public string Series { get; set; } = null!;

        /// <summary>
        /// The passport number.
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// The passport activity.
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// The navigation property for the passport history collection.
        /// </summary>
        public ICollection<UssrPassportHistory> UssrPassportHistories { get; set; } = null!;

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
