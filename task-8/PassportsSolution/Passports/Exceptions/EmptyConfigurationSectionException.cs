namespace Passports.Exceptions
{
    public class EmptyConfigurationSectionException : Exception
    {
        public string SectionName { get; }
        public override string Message => $"Section \"{SectionName}\" does not exist or is empty.";

        public EmptyConfigurationSectionException(string sectionName)
        {
            SectionName = sectionName;
        }
    }
}
