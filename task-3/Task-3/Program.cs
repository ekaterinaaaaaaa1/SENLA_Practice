namespace Task_3
{
    using System.Globalization;
    using static System.String;
    using Task_3.Exceptions;
    class Program
    {
        static void Main()
        {
            string str = string.Empty;
            try
            {
                Console.Write("Введите предложение: ");
                str = Console.ReadLine();

                if (IsNullOrEmpty(str))
                {
                    throw new EmptyStringException();
                }
            }
            catch (EmptyStringException)
            {
                Console.WriteLine("Ошибка: пустая строка!");
                Environment.Exit(0);
            }
            
            string[] words = str.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            Array.Sort(words);
            foreach (string word in words)
            {
                TextInfo textInfo = new CultureInfo("RU", false).TextInfo;
                Console.WriteLine(textInfo.ToTitleCase(word));
            }
            Console.WriteLine($"Количество слов: {words.Length}");
        }
    }
}