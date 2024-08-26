namespace WordsCounter
{
    /// <summary>
    /// Contains method for comparing strings by lengths.
    /// </summary>
    public class StringLengthComparer : IComparer<string>
    {
        /// <summary>
        /// Compares two strings by length.
        /// </summary>
        /// <param name="str1">The first string to compare.</param>
        /// <param name="str2">The second string to compare.</param>
        /// <returns>A positive integer if the length of str1 is longer, a negative integer if the length of str2 is longer, zero if the lengths are equal.</returns>
        /// <exception cref="ArgumentNullException"></exception>
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
