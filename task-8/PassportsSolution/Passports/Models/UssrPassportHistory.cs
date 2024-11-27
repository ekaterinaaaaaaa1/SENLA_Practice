using System.ComponentModel.DataAnnotations.Schema;

namespace Passports.Models
{
    public class UssrPassportHistory
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string PassportSeries { get; set; } = null!;
        public int PassportNumber { get; set; }
        public UssrPassport UssrPassport { get; set; } = null!;
        public DateOnly ActiveStart { get; set; }
        public DateOnly? ActiveEnd { get; set; }
    }
}
