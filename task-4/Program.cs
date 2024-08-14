namespace task_4
{
    using task_4.Exceptions;
    class Program
    {
        static void Main()
        {
            string text = "", word = "";
            try
            {
                Console.Write("Введите текст: ");
                text = Console.ReadLine().ToLower();
                if (text == "")
                {
                    throw new EmptyStringException();
                }
                Console.Write("Введите слово: ");
                word = Console.ReadLine().ToLower();
                if (word == "")
                {
                    throw new EmptyStringException();
                }
            }
            catch (EmptyStringException)
            {
                Console.WriteLine("Ошибка: пустая строка!");
                Environment.Exit(0);
            }

            Console.WriteLine("Слово употребляется в тексте {0} раз(-а).", text.Replace(word, "*").Count(x => x == '*'));
        }
    }
}