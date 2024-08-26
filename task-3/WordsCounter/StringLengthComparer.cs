namespace WordsCounter
{
    public class StringLengthComparer : IComparer<string>
    {
        public int Compare(string? str1, string? str2)
        {
            if (str1 == null || str2 == null)
            {
                throw new ArgumentNullException("Ошибка: некорректное значение параметра!");
            }

            return str1.Length - str2.Length;
        }
    }
}
