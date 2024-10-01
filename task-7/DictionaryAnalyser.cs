namespace task_7
{
    /// <summary>
    /// Analyses Dictionary<T> methods.
    /// </summary>
    public class DictionaryAnalyser<T> : Analyser<T> where T : IComparable<T>
    {
        public DictionaryAnalyser(int lengthOfArray, int numberOfCalls) : base(lengthOfArray, numberOfCalls) { }

        /// <summary>
        /// Analyses Add method.
        /// </summary>
        /// <returns>Returns the number of nanoseconds spent on executing the method.</returns>
        public double Add()
        {
            List<KeyValuePair<T, T>> keyValuePairs = new List<KeyValuePair<T, T>>();
            for (int i = 0; i < LengthOfArray; i++)
            {
                keyValuePairs.Add(new KeyValuePair<T, T>(dataGenerator.Data[i], dataGenerator.Data[LengthOfArray - 1 - i]));
            }
            Dictionary<T, T> dictionary = new Dictionary<T, T>(keyValuePairs);

            stopWatch.Restart();
            for (int i = 0; i < NumberOfCalls; i++)
            {
                dictionary.Add(dataGenerator.Items[i], dataGenerator.Items[NumberOfCalls - 1 - i]);
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
            List<KeyValuePair<T, T>> keyValuePairs = new List<KeyValuePair<T, T>>();
            for (int i = 0; i < LengthOfArray; i++)
            {
                keyValuePairs.Add(new KeyValuePair<T, T>(dataGenerator.Data[i], dataGenerator.Data[LengthOfArray - 1 - i]));
            }
            Dictionary<T, T> dictionary = new Dictionary<T, T>(keyValuePairs);

            stopWatch.Restart();
            for (int i = 0; i < NumberOfCalls; i++)
            {
                dictionary.Remove(dataGenerator.Data[dataGenerator.RemoveIndexes[i]]);
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
            List<KeyValuePair<T, T>> keyValuePairs = new List<KeyValuePair<T, T>>();
            for (int i = 0; i < LengthOfArray; i++)
            {
                keyValuePairs.Add(new KeyValuePair<T, T>(dataGenerator.Data[i], dataGenerator.Data[LengthOfArray - 1 - i]));
            }
            Dictionary<T, T> dictionary = new Dictionary<T, T>(keyValuePairs);

            stopWatch.Restart();
            for (int i = 0; i < NumberOfCalls; i++)
            {
                dictionary.ContainsValue(dataGenerator.Data[dataGenerator.FindIndexes[i]]);
            }
            stopWatch.Stop();

            return stopWatch.Elapsed.TotalNanoseconds;
        }
    }
}
