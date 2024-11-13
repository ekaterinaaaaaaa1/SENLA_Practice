using System.ComponentModel.DataAnnotations.Schema;

namespace Passports.Models
{
    public class PassportHistory
    {
        public int Id { get; set; }
        public short PassportsSeries { get; set; }
        public int PassportsNumber { get; set; }
        public Passport Passport { get; set; } = null!;
        public DateTime ActiveStart { get; set; }
        public DateTime ActiveEnd { get; set; }
    }
}
