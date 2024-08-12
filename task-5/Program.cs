namespace Task_5
{
    class Program
    {
        const int MAX_VALUE = 100;
        
        public static bool isPalindrome(int number)
        {
            if (number == reverseNumber(number))
            {
                return true;
            }

            return false;
        }

        public static int reverseNumber(int number)
        {
            char[] charArray = number.ToString().ToCharArray();
            Array.Reverse(charArray);

            return Convert.ToInt32(new string(charArray));
        }

        static void Main()
        {
            Console.Write($"Введите целое число от 0 до {MAX_VALUE}: ");
            if (!int.TryParse(Console.ReadLine(), out int N))
            {
                Console.WriteLine("Ошибка: неверный тип данных!");
                Environment.Exit(0);
            }
            if ((N < 0) || (N > MAX_VALUE))
            {
                Console.WriteLine("Ошибка: введённое число вне допустимых значений!");
                Environment.Exit(0);
            }

            Console.WriteLine($"Числа-палиндромы от 0 до {N}:");
            for (int i = 0; i <= N; i++)
            {
                if (isPalindrome(i))
                {
                    Console.WriteLine(i);
                }
            }
        }
    }
}