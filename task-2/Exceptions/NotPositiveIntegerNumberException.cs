namespace task_2.Exceptions
{
    public class NotPositiveIntegerNumberException : Exception
    {
        public NotPositiveIntegerNumberException(string? message) : base(message)
        {
        }
    }
}