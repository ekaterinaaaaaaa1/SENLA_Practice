namespace Task_1
{
    using System;

    class Program
    {
        public static bool isEven(int number)
        {
            return number % 2 == 0;
        }

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
            Console.Write("Введите целое число: ");
            bool result = int.TryParse(Console.ReadLine(), out int number);
            if (!result)
            {
                Console.WriteLine("Ошибка: введено не целое число!");
                Environment.Exit(0);
            }

            Console.Write($"Число {number} - ");
            if (isPrime(number))
            {
                Console.Write("простое ");
            }
            else
            {
                Console.Write("составное ");
            }
            if (isEven(number))
            {
                Console.Write("чётное ");
            }
            else
            {
                Console.Write("нечётное ");
            }
            Console.WriteLine("число");
        }
    }
}