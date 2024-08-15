namespace task_2.Exceptions
{
    public class CustomOverflowException : Exception
    {
        public string numberOfInput { get; }

        public CustomOverflowException(string numberOfInput)
        {
            this.numberOfInput = numberOfInput;
        }
    }
}
