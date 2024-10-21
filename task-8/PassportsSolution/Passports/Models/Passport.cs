namespace Passports.Models
{
    public class Passport
    {
        public int Id { get; set; }
        public string Series { get; set; } = null!;
        public string Number { get; set; } = null!;
    }
}
