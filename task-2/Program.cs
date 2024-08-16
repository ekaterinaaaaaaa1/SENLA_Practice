namespace task_2
{
    using task_2.Exceptions;

    class Program
    {
        /// <summary>
        /// Calculates the greatest common divisor of two numbers.
        /// </summary>
        /// <param name="firstNum">The first number.</param>
        /// <param name="secondNum">The second number.</param>
        /// <returns>The greatest common divisor of firstNum and secondNum.</returns>
        public static int GreatestCommonDivisor(int firstNum, int secondNum)
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

        /// <summary>
        /// Calculates the least common multiple of two numbers.
        /// </summary>
        /// <param name="firstNum">The first number.</param>
        /// <param name="secondNum">The second number.</param>
        /// <returns>The least common multiple of firstNum and secondNum.</returns>
        public static int LeastCommonMultiple(int firstNum, int secondNum)
        {
            return firstNum * secondNum / GreatestCommonDivisor(firstNum, secondNum);
        }

        public static int Enter(string numberOfInput)
        {
            int number = 0;
            try
            {
                Console.Write($"Введите {numberOfInput} число: ");
                number = Convert.ToInt32(Console.ReadLine());

                if (number <= 0)
                {
                    throw new NotPositiveIntegerNumberException($"Ошибка: введённое {numberOfInput} число не натуральное!");
                }
            }
            catch (FormatException e)
            {
                throw new CustomFormatException($"Ошибка: введённое {numberOfInput} значение неверного типа данных!", e);
            }
            catch (OverflowException e)
            {
                throw new CustomOverflowException($"Ошибка: введённое {numberOfInput} число по модулю превышает максимально возможное значение!", e);
            }

            return number;
        }

        static void Main()
        {
            int firstNum = 0, secondNum = 0;

            try
            {
                firstNum = Enter("первое");
                secondNum = Enter("второе");
            }
            catch (CustomFormatException e)
            {
                Console.WriteLine(e.InnerException.Message);
                Console.WriteLine(e.Message);
                Environment.Exit(0);
            }
            catch (CustomOverflowException e)
            {
                Console.WriteLine(e.InnerException.Message);
                Console.WriteLine(e.Message);
                Environment.Exit(0);
            }
            catch (NotPositiveIntegerNumberException e)
            {
                Console.WriteLine(e.Message);
                Environment.Exit(0);
            }

            int greatestCommonDivisor = GreatestCommonDivisor(firstNum, secondNum);
            int leastCommonMultiple = LeastCommonMultiple(firstNum, secondNum);

            Console.WriteLine($"Наибольший общий делитель чисел {firstNum} и {secondNum} равен {greatestCommonDivisor}.");
            Console.WriteLine($"Наименьшее общее кратное чисел {firstNum} и {secondNum} равно {leastCommonMultiple}.");
        }
    }
}