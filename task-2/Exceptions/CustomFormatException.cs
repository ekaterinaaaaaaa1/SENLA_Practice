namespace task_2.Exceptions
{
    public class CustomFormatException : Exception
    {

        public CustomFormatException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
