namespace task_7
{
    /// <summary>
    /// Analyses List<T> methods.
    /// </summary>
    public class ListAnalyser<T> : Analyser<T> where T : Comparer<T>
    {
        public ListAnalyser(int lengthOfArray, int numberOfCalls) : base(lengthOfArray, numberOfCalls) { }

        public override long Add()
        {
            List<T> list = new List<T>(dataGenerator.Data);

            stopWatch.Restart();
            for (int i = 0; i < NumberOfCalls; i++)
            {
                list.Add(dataGenerator.Items[i]);
            }
            stopWatch.Stop();

            return stopWatch.ElapsedMilliseconds;
        }

        public override long Insert()
        {
            List<T> list = new List<T>(dataGenerator.Data);

            stopWatch.Restart();
            for (int i = 0; i < NumberOfCalls; i++)
            {
                list.Insert(dataGenerator.Indexes[i], dataGenerator.Items[i]);
            }
            stopWatch.Stop();

            return stopWatch.ElapsedMilliseconds;
        }

        public override long Delete()
        {
            List<T> list = new List<T>(dataGenerator.Data);
            List<T> deleted = new List<T>();
            for (int i = 0; i < NumberOfCalls; i++)
            {
                deleted.Add(dataGenerator.Data[dataGenerator.Indexes[i]]);
            }

            stopWatch.Restart();
            for (int i = 0; i <= NumberOfCalls; i++)
            {
                list.Remove(deleted[i]);
            }
            stopWatch.Stop();

            return stopWatch.ElapsedMilliseconds;
        }

        public override long Find()
        {
            List<T> list = new List<T>(dataGenerator.Data);

            stopWatch.Restart();
            for (int i = 0; i < NumberOfCalls; i++)
            {
                list.Find(a => a == dataGenerator.Data[dataGenerator.Indexes[i]]);
            }
            stopWatch.Stop();

            return stopWatch.ElapsedMilliseconds;
        }

        public override long Sort()
        {
            List<T> list = new List<T>(dataGenerator.Data);
            
            stopWatch.Restart();
            list.Sort();
            stopWatch.Stop();

            return stopWatch.ElapsedMilliseconds;
        }
    }
}
