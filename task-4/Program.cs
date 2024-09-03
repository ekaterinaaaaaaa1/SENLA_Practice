namespace task_4
{
    using System.Resources;
    using task_4.Exceptions;
    class Program
    {
        static void Main()
        {            
            string text = string.Empty, word = string.Empty;
            try
            {
                Console.Write("Введите текст: ");
                text = Console.ReadLine().ToLower();
                if (String.IsNullOrEmpty(text))
                {
                    throw new EmptyStringException();
                }
                Console.Write("Введите слово: ");
                word = Console.ReadLine().ToLower();
                if (String.IsNullOrEmpty(word))
                {
                    throw new EmptyStringException();
                }
            }
            catch (EmptyStringException)
            {
                Console.WriteLine(Resources.EmptyString);
                Environment.Exit(0);
            }

            Console.WriteLine(string.Format(Resources.WordsNumber, text.Replace(word, "*").Count(x => x == '*')));
        }
    }
}