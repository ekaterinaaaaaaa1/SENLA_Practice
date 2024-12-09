namespace Passports.Models
{
    public class UssrPassportHistory
    {
        /// <summary>
        /// The passport history Id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The passport series.
        /// </summary>
        public string PassportSeries { get; set; } = null!;

        /// <summary>
        /// The passport number.
        /// </summary>
        public int PassportNumber { get; set; }

        /// <summary>
        /// The navigation property for the passport.
        /// </summary>
        public UssrPassport UssrPassport { get; set; } = null!;

        /// <summary>
        /// The start date of the active state.
        /// </summary>
        public DateOnly ActiveStart { get; set; }

        /// <summary>
        /// The end date of the active state.
        /// </summary>
        public DateOnly? ActiveEnd { get; set; }
    }
}
