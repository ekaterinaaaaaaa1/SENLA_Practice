namespace Passports.Exceptions
{
    /// <summary>
    /// Represents a parse exception.
    /// </summary>
    public class ParseException : Exception
    {
        public override string Message => "Conversion to a type is not possible.";
    }
}
