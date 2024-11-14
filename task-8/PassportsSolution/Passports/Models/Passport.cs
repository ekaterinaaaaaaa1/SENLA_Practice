namespace Passports.Models
{
    public class Passport
    {
        public short Series { get; set; }
        public int Number { get; set; }
        public bool IsActive { get; set; }

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
