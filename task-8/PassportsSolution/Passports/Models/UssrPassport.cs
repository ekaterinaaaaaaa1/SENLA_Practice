namespace Passports.Models
{
    public class UssrPassport
    {
        public string Series { get; set; } = null!;
        public int Number { get; set; }
        public bool IsActive { get; set; }

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
