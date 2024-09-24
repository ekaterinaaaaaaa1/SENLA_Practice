using System.Text;

namespace task_7
{
    public class DataGenerator<T>
    {
        private const int NUMBER_OF_CALLS = 100;
        private const int LENGTH_OF_ARRAY = 1000;

        //private static T[]? data = new T[LENGTH_OF_ARRAY];
        //private static T[]? items = new T[NUMBER_OF_CALLS];

        //private static int[] indexes = new int[NUMBER_OF_CALLS];

        public T[]? Items { get; }
        public T[]? Data { get; }
        public int[] Indexes { get; }

        public DataGenerator()
        {
            Data = GenerateData(LENGTH_OF_ARRAY);
            Items = GenerateData(NUMBER_OF_CALLS);
            Indexes = GenerateDataInt(NUMBER_OF_CALLS);
        }

        private T[]? GenerateData(int dataSize)
        {
            if (typeof(T) == typeof(int))
            {
                return GenerateDataInt(dataSize) as T[];
            }

            if (typeof(T) == typeof(double))
            {
                return GenerateDataDouble(dataSize) as T[];
            }

            return GenerateDataString(dataSize) as T[];
        }

        private int[] GenerateDataInt(int dataSize)
        {
            int[] generatedDataInt = new int[dataSize];
            Random random = new Random();

            for (int i = 0; i < dataSize; i++)
            {
                generatedDataInt[i] = random.Next();
            }

            return generatedDataInt;
        }

        private double[] GenerateDataDouble(int dataSize)
        {
            double[] generatedDataDouble = new double[dataSize];
            Random random = new Random();

            for (int i = 0; i < dataSize; i++)
            {
                generatedDataDouble[i] = random.NextDouble();
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

                generatedDataString[i] = stringBuilder.ToString();
            }

            return generatedDataString;
        }
    }
}
