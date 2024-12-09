namespace Passports.Models.DTO
{
    /// <summary>
    /// Contains the properties to describe the passport activity change.
    /// </summary>
    public class PassportChanges
    {
        /// <summary>
        /// The start date of the new state.
        /// </summary>
        public DateOnly? Start { get; set; }

        /// <summary>
        /// The end date of the new state.
        /// </summary>
        public DateOnly? End { get; set; }

        /// <summary>
        /// The passport activity.
        /// </summary>
        public bool IsActive { get; set; }
    }
}
