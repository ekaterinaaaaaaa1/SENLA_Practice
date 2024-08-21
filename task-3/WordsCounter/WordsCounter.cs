﻿using System.Globalization;
using WordsCounter.Exceptions;

namespace WordsCounter
{
    /// <summary>
    /// Contains a function for counting and outputting words.
    /// </summary>
    public class WordsCounter
    {
        /// <summary>
        /// Outputs words in alhapetical order with a capital letter and counts their number.
        /// </summary>
        public static void Count()
        {
            string str = string.Empty;
            try
            {
                Console.Write("Введите предложение: ");
                str = Console.ReadLine();

                if (string.IsNullOrEmpty(str))
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
            Console.ReadKey();
        }
    }
}
