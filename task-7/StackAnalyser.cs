namespace task_7
{
    /// <summary>
    /// Analyses Stack<T> methods.
    /// </summary>
    /// <typeparam name="T">Data type of Stack.</typeparam>
    public class StackAnalyser<T> : Analyser<T> where T : IComparable<T>
    {
        public StackAnalyser(int lengthOfArray, int numberOfCalls) : base(lengthOfArray, numberOfCalls) { }

        /// <summary>
        /// Analyses Add method.
        /// </summary>
        /// <returns>Returns the number of nanoseconds spent on executing the method.</returns>
        public double Add()
        {
            Stack<T> stack = new Stack<T>(dataGenerator.Data);

            stopWatch.Restart();
            for (int i = 0; i < NumberOfCalls; i++)
            {
                stack.Push(dataGenerator.Items[i]);
            }
            stopWatch.Stop();

            return stopWatch.Elapsed.TotalNanoseconds;
        }

        /// <summary>
        /// Analyses Remove method.
        /// </summary>
        /// <returns>Returns the number of nanoseconds spent on executing the method.</returns>
        public double Remove()
        {
            Stack<T> stack = new Stack<T>(dataGenerator.Data);

            stopWatch.Restart();
            for (int i = 0; i < NumberOfCalls; i++)
            {
                stack.Pop();
            }
            stopWatch.Stop();

            return stopWatch.Elapsed.TotalNanoseconds;
        }

        /// <summary>
        /// Analyses Contains method.
        /// </summary>
        /// <returns>Returns the number of nanoseconds spent on executing the method.</returns>
        public double Contains()
        {
            Stack<T> stack = new Stack<T>(dataGenerator.Data);

            stopWatch.Restart();
            for (int i = 0; i < NumberOfCalls; i++)
            {
                stack.Contains(dataGenerator.Data[dataGenerator.FindIndexes[i]]);
            }
            stopWatch.Stop();

            return stopWatch.Elapsed.TotalNanoseconds;
        }
    }
}
