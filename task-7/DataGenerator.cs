using System.Text;

namespace task_7
{
    /// <summary>
    /// Generates random data to analyse the collections methods.
    /// </summary>
    /// <typeparam name="T">Type of the random data.</typeparam>
    public class DataGenerator<T>
    {
        /// <summary>
        /// Initial data.
        /// </summary>
        public T[] Data { get; }
        /// <summary>
        /// Data to add items to the collection.
        /// </summary>
        public T[] Items { get; }
        /// <summary>
        /// Indexes to use in Insert method.
        /// </summary>
        public int[] InsertIndexes { get; }
        /// <summary>
        /// Indexes to use in Remove method.
        /// </summary>
        public int[] RemoveIndexes { get; }
        /// <summary>
        /// Indexes to use in Find method.
        /// </summary>
        public int[] FindIndexes { get; }

        public DataGenerator(int lengthOfArray, int numberOfCalls)
        {
            Data = GenerateData(lengthOfArray);
            Items = GenerateData(numberOfCalls);
            InsertIndexes = GenerateInsertIndexes(numberOfCalls, lengthOfArray);
            RemoveIndexes = GenerateRemoveIndexes(numberOfCalls, lengthOfArray);
            FindIndexes = GenerateFindIndexes(numberOfCalls, lengthOfArray);
        }

        private T[] GenerateData(int dataSize)
        {
            if (typeof(T) == typeof(int))
            {
                return GenerateDataInt(dataSize).Cast<T>().ToArray();
            }

            if (typeof(T) == typeof(double))
            {
                return GenerateDataDouble(dataSize).Cast<T>().ToArray();
            }

            return GenerateDataString(dataSize).Cast<T>().ToArray();
        }

        private int[] GenerateDataInt(int dataSize)
        {
            int[] generatedDataInt = new int[dataSize];
            Random random = new Random();

            for (int i = 0; i < dataSize; i++)
            {
                int generated = random.Next();

                if (!generatedDataInt.Contains(generated))
                {
                    generatedDataInt[i] = generated;
                }
                else
                {
                    i--;
                }
            }

            return generatedDataInt;
        }

        private double[] GenerateDataDouble(int dataSize)
        {
            double[] generatedDataDouble = new double[dataSize];
            Random random = new Random();

            for (int i = 0; i < dataSize; i++)
            {
                double generated = random.NextDouble();
                if (!generatedDataDouble.Contains(generated))
                {
                    generatedDataDouble[i] = generated;
                }
                else
                {
                    i--;
                }
            }

            return generatedDataDouble;
        }

        private string[] GenerateDataString(int dataSize)
        {
            string[] generatedDataString = new string[dataSize];

            string alphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            int alphabetLength = alphabet.Length;

            int stringLength = 8;
            Random random = new Random();

            for (int i = 0; i < dataSize; i++)
            {
                StringBuilder stringBuilder = new StringBuilder();
                for (int j = 0; j < stringLength; j++)
                {
                    stringBuilder.Append(alphabet[random.Next(alphabetLength)]);
                }

                string generated = stringBuilder.ToString();
                if (!generatedDataString.Contains(generated))
                {
                    generatedDataString[i] = generated;
                }
                else
                {
                    i--;
                }
            }

            return generatedDataString;
        }

        private int[] GenerateInsertIndexes(int dataSize, int maxRandom)
        {
            int[] generatedInsertIndexes = new int[dataSize];
            Random random = new Random();

            for (int i = 0; i < dataSize; i++)
            {
                generatedInsertIndexes[i] = random.Next(maxRandom + i);
            }

            return generatedInsertIndexes;
        }

        private int[] GenerateRemoveIndexes(int dataSize, int maxRandom)
        {
            int[] generatedRemoveIndexes = new int[dataSize];
            Random random = new Random();

            for (int i = 0; i < dataSize; i++)
            {
                generatedRemoveIndexes[i] = random.Next(maxRandom - i);
            }

            return generatedRemoveIndexes;
        }

        private int[] GenerateFindIndexes(int dataSize, int maxRandom)
        {
            int[] generatedFindIndexes = new int[dataSize];
            Random random = new Random();

            for (int i = 0; i < dataSize; i++)
            {
                generatedFindIndexes[i] = random.Next(maxRandom);
            }

            return generatedFindIndexes;
        }
    }
}
