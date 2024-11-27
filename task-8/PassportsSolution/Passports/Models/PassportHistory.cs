using System.ComponentModel.DataAnnotations.Schema;

namespace Passports.Models
{
    public class PassportHistory
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public short PassportSeries { get; set; }
        public int PassportNumber { get; set; }
        public Passport Passport { get; set; } = null!;
        public DateOnly ActiveStart { get; set; }
        public DateOnly? ActiveEnd { get; set; }
    }
}
