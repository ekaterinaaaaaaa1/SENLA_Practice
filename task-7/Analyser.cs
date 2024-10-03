using System.Diagnostics;

namespace task_7
{
    /// <summary>
    /// An abstract class to analyse collections.
    /// </summary>
    /// <typeparam name="T">Data type of the collection.</typeparam>
    public abstract class Analyser<T>
    {
        /// <summary>
        /// An instance of DataGenerator<T>.
        /// </summary>
        public DataGenerator<T> dataGenerator;

        /// <summary>
        /// An instance of Stopwatch.
        /// </summary>
        public Stopwatch stopWatch;

        /// <summary>
        /// Contains the length of the initial data.
        /// </summary>
        public int LengthOfArray { get; }

        /// <summary>
        /// Contains the number of the methods calls to count the time.
        /// </summary>
        public int NumberOfCalls { get; }

        public Analyser(int lengthOfArray, int numberOfCalls)
        {
            LengthOfArray = lengthOfArray;
            NumberOfCalls = numberOfCalls;
            dataGenerator = new DataGenerator<T>(lengthOfArray, numberOfCalls);
            stopWatch = new Stopwatch();
        }
    }
}
