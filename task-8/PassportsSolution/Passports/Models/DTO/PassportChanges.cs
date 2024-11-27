namespace Passports.Models.DTO
{
    public class PassportChanges
    {
        public DateOnly? Start { get; set; }
        public DateOnly? End { get; set; }
        public bool IsActive { get; set; }
    }
}
