namespace task_2.Exceptions
{
    public class CustomFormatException : Exception
    {
        public string numberOfInput { get; }

        public CustomFormatException(string numberOfInput)
        {
            this.numberOfInput = numberOfInput;
        }
    }
}
