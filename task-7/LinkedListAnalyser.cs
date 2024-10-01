using System.Collections.Generic;

namespace task_7
{
    /// <summary>
    /// Analyses LinkedList<T> methods.
    /// </summary>
    public class LinkedListAnalyser<T> : Analyser<T> where T : IComparable<T>
    {
        public LinkedListAnalyser(int lengthOfArray, int numberOfCalls) : base(lengthOfArray, numberOfCalls) { }

        /// <summary>
        /// Analyses Add method.
        /// </summary>
        /// <returns>Returns the number of nanoseconds spent on executing the method.</returns>
        public double Add()
        {
            LinkedList<T> linkedList = new LinkedList<T>(dataGenerator.Data);

            stopWatch.Restart();
            for (int i = 0; i < NumberOfCalls; i++)
            {
                linkedList.AddLast(dataGenerator.Items[i]);
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
            LinkedList<T> linkedList = new LinkedList<T>(dataGenerator.Data);

            double time = 0;
            for (int i = 0; i < NumberOfCalls; i++)
            {
                LinkedListNode<T>? node = linkedList.First;
                for (int j = 0; j < dataGenerator.InsertIndexes[i] - 1; j++)
                {
                    node = node?.Next;
                }

                if (node != null)
                {
                    stopWatch.Restart();
                    linkedList.AddAfter(node, dataGenerator.Items[i]);
                    stopWatch.Stop();
                }

                time += stopWatch.Elapsed.TotalNanoseconds;
            }

            return time;
        }

        /// <summary>
        /// Analyses Remove method.
        /// </summary>
        /// <returns>Returns the number of nanoseconds spent on executing the method.</returns>
        public double Remove()
        {
            LinkedList<T> linkedList = new LinkedList<T>(dataGenerator.Data);

            double time = 0;
            for (int i = 0; i < NumberOfCalls; i++)
            {
                LinkedListNode<T>? node = linkedList.First;
                for (int j = 0; j < dataGenerator.RemoveIndexes[i] - 1; j++)
                {
                    node = node?.Next;
                }

                if (node != null)
                {
                    stopWatch.Restart();
                    linkedList.Remove(node);
                    stopWatch.Stop();
                }

                time += stopWatch.Elapsed.TotalNanoseconds;
            }

            return time;
        }

        /// <summary>
        /// Analyses Find method.
        /// </summary>
        /// <returns>Returns the number of nanoseconds spent on executing the method.</returns>
        public double Find()
        {
            LinkedList<T> linkedList = new LinkedList<T>(dataGenerator.Data);

            stopWatch.Restart();
            for (int i = 0; i < NumberOfCalls; i++)
            {
                linkedList.Find(dataGenerator.Data[dataGenerator.FindIndexes[i]]);
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
            LinkedList<T> linkedList = new LinkedList<T>(dataGenerator.Data);

            stopWatch.Restart();
            for (int i = 0; i < NumberOfCalls; i++)
            {
                linkedList.Contains(dataGenerator.Data[dataGenerator.FindIndexes[i]]);
            }
            stopWatch.Stop();

            return stopWatch.Elapsed.TotalNanoseconds;
        }
    }
}
