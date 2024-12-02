namespace Passports.Models.DTO
{
    public class UssrPassportOnly
    {
        public string Series { get; set; } = null!;
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
