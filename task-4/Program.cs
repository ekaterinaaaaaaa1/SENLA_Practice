namespace Task_4
{
    class Program
    {
        static void Main()
        {
            Console.Write("Введите текст: ");
            string text = Console.ReadLine().ToLower();
            if (text == null)
            {
                Console.WriteLine("Ошибка: пустая строка!");
                Environment.Exit(0);
            }
            Console.Write("Введите слово: ");
            string word = Console.ReadLine().ToLower();
            if (word == null)
            {
                Console.WriteLine("Ошибка: пустая строка!");
                Environment.Exit(0);
            }

            Console.WriteLine("Слово употребляется в тексте {0} раз(-а)", text.Replace(word, "*").Count(x => x == '*'));
        }
    }
}