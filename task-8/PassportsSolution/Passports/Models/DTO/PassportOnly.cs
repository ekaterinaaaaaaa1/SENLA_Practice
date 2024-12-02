namespace Passports.Models.DTO
{
    [Serializable]
    public class PassportOnly
    {
        public short Series { get; set; }
        public int Number { get; set; }
    }
}
