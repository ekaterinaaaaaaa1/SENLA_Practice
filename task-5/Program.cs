namespace Task_5
{
    class Program
    {
        const int MAX_VALUE = 100;

        delegate bool Check(int value);
        
        /// <summary>
        /// Checks whether an integer is a palindrome or not.
        /// </summary>
        /// <param name="number">Integer to check.</param>
        /// <returns>True if a palindrome, false if not a palindrome.</returns>
        public static bool IsPalindrome(int number)
        {
            if (number == ReverseNumber(number))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Writes an integer in reverse.
        /// </summary>
        /// <param name="number">Integer to reverse.</param>
        /// <returns>Integer number in reverse.</returns>
        public static int ReverseNumber(int number)
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

            Check check = IsPalindrome;

            Console.WriteLine($"Числа-палиндромы от 0 до {N}:");
            for (int i = 0; i <= N; i++)
            {
                if (check(i))
                {
                    Console.WriteLine(i);
                }
            }
        }
    }
}