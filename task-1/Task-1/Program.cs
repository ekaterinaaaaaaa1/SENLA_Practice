namespace Task_1
{
    using System;

    class Program
    {
        public static Boolean isEven(int number)
        {
            return number % 2 == 0;
        }

        public static Boolean isPrime(int number)
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
            try
            {
                double number = Convert.ToDouble(Console.ReadLine());

                if (Math.Floor(number) != number)
                {
                    Console.WriteLine("Ошибка: введено дробное число!");
                    Environment.Exit(0);
                }

                Console.Write($"Введённое число {number} - ");

                if (isPrime((int)number))
                {
                    Console.Write("простое ");
                }
                else
                {
                    Console.Write("составное ");
                }

                if (isEven((int)number))
                {
                    Console.Write("чётное ");
                }
                else
                {
                    Console.Write("нечётное ");
                }
                Console.WriteLine("число");
            }
            catch
            {
                Console.WriteLine("Ошибка: введено не целое число!");
            }
        }
    }
}