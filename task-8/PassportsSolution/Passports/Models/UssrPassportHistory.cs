namespace Passports.Models
{
    /// <summary>
    /// Represents a USSR passport history entity.
    /// </summary>
    public class UssrPassportHistory : BasePassportHistory
    {
        /// <summary>
        /// The passport series.
        /// </summary>
        public string PassportSeries { get; set; } = null!;

        /// <summary>
        /// The navigation property for the passport.
        /// </summary>
        public UssrPassport UssrPassport { get; set; } = null!;
    }
}
