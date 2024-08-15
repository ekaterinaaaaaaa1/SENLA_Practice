namespace Task_1
{
    using System;
    using System.Text;
    using Task_1.Exceptions;

    class Program
    {
        /// <summary>
        /// Checks whether an integer is even.
        /// </summary>
        /// <param name="number">Number to check.</param>
        /// <returns>True if even, false if odd.</returns>
        public static bool isEven(int number)
        {
            return number % 2 == 0;
        }

        /// <summary>
        /// Checks whether an integer is prime.
        /// </summary>
        /// <param name="number">Number to check.</param>
        /// <returns>True if prime, false if composite.</returns>
        public static bool isPrime(int number)
        {
            int i = 2;

            while (i <= Math.Sqrt(number))
            {
                if (number % i == 0)
                {
                    return false;
                }
                i++;
            }

            return true;
        }

        static void Main()
        {
            int number = 0;
            try
            {
                Console.Write("Введите натуральное число: ");
                number = Convert.ToInt32(Console.ReadLine());
                if (number <= 0)
                {
                    throw new NotPositiveIntegerNumberException();
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Ошибка: неверный тип входных данных!");
                Environment.Exit(0);
            }
            catch (OverflowException)
            {
                Console.WriteLine("Ошибка: введённое число по модулю превосходит максимально возможное значение!");
                Environment.Exit(0);
            }
            catch (NotPositiveIntegerNumberException)
            {
                Console.WriteLine("Ошибка: введено не натуральное число!");
                Environment.Exit(0);
            }

            StringBuilder sb = new StringBuilder($"Число {number} - ");
            if (isPrime(number))
            {
                sb.Append("простое ");
            }
            else
            {
                sb.Append("составное ");
            }
            if (isEven(number))
            {
                sb.Append("чётное ");
            }
            else
            {
                sb.Append("нечётное ");
            }
            sb.Append("число");

            Console.WriteLine(sb.ToString());
        }
    }
}