namespace Task_2
{
    class Program
    {
        public static int Gcd(int firstNum, int secondNum)
        {
            while ((firstNum != 0) && (secondNum != 0))
            {
                if (firstNum > secondNum)
                {
                    firstNum %= secondNum;
                }
                else
                {
                    secondNum %= firstNum;
                }
            }

            return firstNum + secondNum;
        }

        public static int Scm(int firstNum, int secondNum)
        {
            return firstNum * secondNum / Gcd(firstNum, secondNum);
        }

        static void Main()
        {
            Console.Write("Введите первое число: ");
            bool result = int.TryParse(Console.ReadLine(), out int firstNum);
            if (!result)
            {
                Console.WriteLine("Ошибка: введено не целое число!");
                Environment.Exit(0);
            }
            Console.Write("Введите второе число: ");
            result = int.TryParse(Console.ReadLine(), out int secondNum);
            if (!result)
            {
                Console.WriteLine("Ошибка: введено не целое число!");
                Environment.Exit(0);
            }

            Console.WriteLine($"Наибольший общий делитель чисел {firstNum} и {secondNum} равен {Gcd(firstNum, secondNum)}");
            Console.WriteLine($"Наименьшее общее кратное чисел {firstNum} и {secondNum} равно {Scm(firstNum, secondNum)}");
        }
    }
}