namespace task_7
{
    /// <summary>
    /// Analyses Queue<T> methods.
    /// </summary>
    /// <typeparam name="T">Data type of Queue.</typeparam>
    public class QueueAnalyser<T> : Analyser<T> where T : IComparable<T>
    {
        public QueueAnalyser(int lengthOfArray, int numberOfCalls) : base(lengthOfArray, numberOfCalls) { }

        /// <summary>
        /// Analyses Add method.
        /// </summary>
        /// <returns>Returns the number of nanoseconds spent on executing the method.</returns>
        public double Add()
        {
            Queue<T> queue = new Queue<T>(dataGenerator.Data);

            stopWatch.Restart();
            for (int i = 0; i < NumberOfCalls; i++)
            {
                queue.Enqueue(dataGenerator.Items[i]);
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
            Queue<T> queue = new Queue<T>(dataGenerator.Data);

            stopWatch.Restart();
            for (int i = 0; i < NumberOfCalls; i++)
            {
                queue.Dequeue();
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
            Queue<T> queue = new Queue<T>(dataGenerator.Data);

            stopWatch.Restart();
            for (int i = 0; i < NumberOfCalls; i++)
            {
                queue.Contains(dataGenerator.Data[dataGenerator.FindIndexes[i]]);
            }
            stopWatch.Stop();

            return stopWatch.Elapsed.TotalNanoseconds;
        }
    }
}
