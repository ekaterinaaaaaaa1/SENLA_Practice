namespace task_7
{
    /// <summary>
    /// Analyses HashSet<T> methods.
    /// </summary>
    /// <typeparam name="T">Data type of HashSet.</typeparam>
    public class HashSetAnalyser<T> : Analyser<T> where T : IComparable<T>
    {
        public HashSetAnalyser(int lengthOfArray, int numberOfCalls) : base(lengthOfArray, numberOfCalls) { }

        /// <summary>
        /// Analyses Add method.
        /// </summary>
        /// <returns>Returns the number of nanoseconds spent on executing the method.</returns>
        public double Add()
        {
            HashSet<T> hashSet = new HashSet<T>(dataGenerator.Data);

            stopWatch.Restart();
            for (int i = 0; i < NumberOfCalls; i++)
            {
                hashSet.Add(dataGenerator.Items[i]);
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
            HashSet<T> hashSet = new HashSet<T>(dataGenerator.Data);

            stopWatch.Restart();
            for (int i = 0; i < NumberOfCalls; i++)
            {
                hashSet.Remove(dataGenerator.Data[dataGenerator.RemoveIndexes[i]]);
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
            HashSet<T> hashSet = new HashSet<T>(dataGenerator.Data);

            stopWatch.Restart();
            for (int i = 0; i < NumberOfCalls; i++)
            {
                hashSet.Contains(dataGenerator.Data[dataGenerator.FindIndexes[i]]);
            }
            stopWatch.Stop();

            return stopWatch.Elapsed.TotalNanoseconds;
        }
    }
}
