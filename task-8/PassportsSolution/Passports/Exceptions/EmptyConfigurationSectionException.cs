namespace Passports.Exceptions
{
    /// <summary>
    /// Represents an empty configuration section exception.
    /// </summary>
    public class EmptyConfigurationSectionException : Exception
    {
        /// <summary>
        /// The name of the section that is empty.
        /// </summary>
        public string SectionName { get; }
        public override string Message => $"Section \"{SectionName}\" does not exist or is empty.";

        /// <summary>
        /// EmptyConfigurationSectionException constructor.
        /// </summary>
        /// <param name="sectionName">The name of the section that is empty.</param>
        public EmptyConfigurationSectionException(string sectionName)
        {
            SectionName = sectionName;
        }
    }
}
