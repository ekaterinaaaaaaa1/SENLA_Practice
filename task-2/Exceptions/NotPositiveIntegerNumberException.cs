﻿namespace task_2.Exceptions
{
    public class NotPositiveIntegerNumberException : Exception
    {
        public string numberOfInput { get; }

        public NotPositiveIntegerNumberException(string numberOfInput)
        {
            this.numberOfInput = numberOfInput;
        }
    }
}