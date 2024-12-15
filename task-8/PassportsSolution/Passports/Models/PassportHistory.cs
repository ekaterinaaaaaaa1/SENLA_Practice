namespace Passports.Models
{
    /// <summary>
    /// Represents a Russian passport history entity.
    /// </summary>
    public class PassportHistory : BasePassportHistory
    {
        /// <summary>
        /// The passport series.
        /// </summary>
        public short PassportSeries { get; set; }

        /// <summary>
        /// The navigation property for the passport.
        /// </summary>
        public Passport Passport { get; set; } = null!;
    }
}
