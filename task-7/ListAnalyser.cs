namespace task_7
{
    /// <summary>
    /// Analyses List<T> methods.
    /// </summary>
    public class ListAnalyser<T> : Analyser<T> where T : IComparable<T>
    {
        public ListAnalyser(int lengthOfArray, int numberOfCalls) : base(lengthOfArray, numberOfCalls) { }

        /// <summary>
        /// Analyses Add method.
        /// </summary>
        /// <returns>Returns the number of nanoseconds spent on executing the method.</returns>
        public double Add()
        {
            List<T> list = new List<T>(dataGenerator.Data);

            stopWatch.Restart();
            for (int i = 0; i < NumberOfCalls; i++)
            {
                list.Add(dataGenerator.Items[i]);
            }
            stopWatch.Stop();

            return stopWatch.Elapsed.TotalNanoseconds;
        }

        /// <summary>
        /// Analyses Insert method.
        /// </summary>
        /// <returns>Returns the number of nanoseconds spent on executing the method.</returns>
        public double Insert()
        {
            List<T> list = new List<T>(dataGenerator.Data);

            stopWatch.Restart();
            for (int i = 0; i < NumberOfCalls; i++)
            {
                list.Insert(dataGenerator.InsertIndexes[i], dataGenerator.Items[i]);
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
            List<T> list = new List<T>(dataGenerator.Data);

            stopWatch.Restart();
            for (int i = 0; i < NumberOfCalls; i++)
            {
                list.Remove(dataGenerator.Data[dataGenerator.RemoveIndexes[i]]);
            }
            stopWatch.Stop();

            return stopWatch.Elapsed.TotalNanoseconds;
        }

        /// <summary>
        /// Analyses Find method.
        /// </summary>
        /// <returns>Returns the number of nanoseconds spent on executing the method.</returns>
        public double Find()
        {
            List<T> list = new List<T>(dataGenerator.Data);

            stopWatch.Restart();
            for (int i = 0; i < NumberOfCalls; i++)
            {
                list.Find(a => a.Equals(dataGenerator.Data[dataGenerator.FindIndexes[i]]));
            }
            stopWatch.Stop();

            return stopWatch.Elapsed.TotalNanoseconds;
        }

        /// <summary>
        /// Analyses Sort method.
        /// </summary>
        /// <returns>Returns the number of nanoseconds spent on executing the method.</returns>
        public double Sort()
        {
            List<T> list = new List<T>(dataGenerator.Data);
            
            stopWatch.Restart();
            list.Sort();
            stopWatch.Stop();

            return stopWatch.Elapsed.TotalNanoseconds;
        }

        /// <summary>
        /// Analyses Contains method.
        /// </summary>
        /// <returns>Returns the number of nanoseconds spent on executing the method.</returns>
        public double Contains()
        {
            List<T> list = new List<T>(dataGenerator.Data);

            stopWatch.Restart();
            for (int i = 0; i < NumberOfCalls; i++)
            {
                list.Contains(dataGenerator.Data[dataGenerator.FindIndexes[i]]);
            }
            stopWatch.Stop();

            return stopWatch.Elapsed.TotalNanoseconds;
        }
    }
}
