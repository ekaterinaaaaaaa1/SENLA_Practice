namespace Passports.Models
{
    public class Passport //: IEqualityComparer<Passport>
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

        /*public bool Equals(Passport? x, Passport? y)
        {
            if (x != null && y != null)
            {
                return x.Series == y.Series && x.Number == y.Number;
            }

            return false;
        }

        public int GetHashCode(Passport obj)
        {
            return obj != null ? HashCode.Combine(Series, Number) : 0;
        }*/
    }
}
