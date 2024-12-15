namespace Passports.Models
{
    /// <summary>
    /// Represent a passport entity.
    /// </summary>
    public abstract class BasePassport
    {
        /// <summary>
        /// The passport number.
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// The passport activity.
        /// </summary>
        public bool IsActive { get; set; }
    }
}
