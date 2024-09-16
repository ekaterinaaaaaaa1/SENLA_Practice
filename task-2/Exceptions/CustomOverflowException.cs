namespace task_2.Exceptions
{
    public class CustomOverflowException : Exception
    {
        public CustomOverflowException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
