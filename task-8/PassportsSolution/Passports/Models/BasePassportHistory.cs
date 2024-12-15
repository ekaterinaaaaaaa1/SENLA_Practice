namespace Passports.Models
{
    /// <summary>
    /// Represents a passport history entity.
    /// </summary>
    public abstract class BasePassportHistory
    {
        /// <summary>
        /// The passport history Id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The passport number.
        /// </summary>
        public int PassportNumber { get; set; }

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
