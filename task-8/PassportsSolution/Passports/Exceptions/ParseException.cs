namespace Passports.Exceptions
{
    public class ParseException : Exception
    {
        public override string Message => "Conversion to a type is not possible.";
    }
}
