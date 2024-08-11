namespace Task_3
{
    using System.Globalization;
    class Program
    {
        static void Main()
        {
            Console.Write("Введите предложение: ");
            string str = Console.ReadLine();
            if (str == "")
            {
                Console.WriteLine("Ошибка: пустая строка!");
                Environment.Exit(0);
            }
            string[] words = str.Split(" ");
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