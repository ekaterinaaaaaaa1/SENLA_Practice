namespace task_2
{
    using System.Diagnostics;
    using System.Text;
    using task_2.Exceptions;

    class Program
    {
        /// <summary>
        /// Calculates the greatest common divisor of two numbers.
        /// </summary>
        /// <param name="firstNum">The first number.</param>
        /// <param name="secondNum">The seconf number.</param>
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
        /// <param name="secondNum">The seconf number.</param>
        /// <returns>The least common multiple of firstNum and secondNum.</returns>
        public static int LeastCommonMultiple(int firstNum, int secondNum)
        {
            return firstNum * secondNum / GreatestCommonDivisor(firstNum, secondNum);
        }

        static void Main()
        {
            int firstNum = 0, secondNum = 0;

            try
            {
                Console.Write("Введите первое число: ");
                firstNum = Convert.ToInt32(Console.ReadLine());

                if (firstNum <= 0)
                {
                    throw new NotPositiveIntegerNumberException();
                }

                Console.Write("Введите второе число: ");
                secondNum = Convert.ToInt32(Console.ReadLine());

                if (secondNum <= 0)
                {
                    throw new NotPositiveIntegerNumberException();
                }
            }
            catch (FormatException e)
            {
                StackTrace st = new StackTrace(e, true);
                StackFrame frame = st.GetFrame(st.FrameCount - 1);

                StringBuilder sb = new StringBuilder("Ошибка: неверный тип данных при вводе");
                sb.Append(frame.GetFileLineNumber() == 50 ? " первого" : " второго");
                sb.Append(" числа!");

                Console.WriteLine(sb.ToString());
                Environment.Exit(0);
            }
            catch (OverflowException e)
            {
                StackTrace st = new StackTrace(e, true);
                StackFrame frame = st.GetFrame(st.FrameCount - 1);

                StringBuilder sb = new StringBuilder("Ошибка: введённое");
                sb.Append(frame.GetFileLineNumber() == 50 ? " первое" : " второе");
                sb.Append(" число по модулю превосходит максимально возможное значение!");

                Console.WriteLine(sb.ToString());
                Environment.Exit(0);
            }
            catch (NotPositiveIntegerNumberException e)
            {
                StackTrace st = new StackTrace(e, true);
                StackFrame frame = st.GetFrame(st.FrameCount - 1);

                StringBuilder sb = new StringBuilder("Ошибка: введённое");
                sb.Append(frame.GetFileLineNumber() == 54 ? " первое" : " второе");
                sb.Append(" число не натуральное!");

                Console.WriteLine(sb.ToString());
                Environment.Exit(0);
            }

            int greatestCommonDivisor = GreatestCommonDivisor(firstNum, secondNum);
            int leastCommonMultiple = LeastCommonMultiple(firstNum, secondNum);

            Console.WriteLine($"Наибольший общий делитель чисел {firstNum} и {secondNum} равен {greatestCommonDivisor}.");
            Console.WriteLine($"Наименьшее общее кратное чисел {firstNum} и {secondNum} равно {leastCommonMultiple}.");
        }
    }
}