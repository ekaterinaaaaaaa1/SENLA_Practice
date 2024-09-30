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
        /// Indexes to use in some methods.
        /// </summary>
        public int[] Indexes { get; }

        public DataGenerator(int lengthOfArray, int numberOfCalls)
        {
            Data = GenerateData(lengthOfArray);
            Items = GenerateData(numberOfCalls);
            Indexes = GenerateDataInt(numberOfCalls, lengthOfArray);
        }

        private T[] GenerateData(int dataSize)
        {
            if (typeof(T) == typeof(int))
            {
                return GenerateDataInt(dataSize, dataSize).Cast<T>().ToArray();
            }

            if (typeof(T) == typeof(double))
            {
                return GenerateDataDouble(dataSize).Cast<T>().ToArray();
            }

            return GenerateDataString(dataSize).Cast<T>().ToArray();
        }

        private int[] GenerateDataInt(int dataSize, int maxRandom)
        {
            int[] generatedDataInt = new int[dataSize];
            Random random = new Random();

            for (int i = 0; i < dataSize; i++)
            {
                int generated = random.Next(maxRandom - i);
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
    }
}
