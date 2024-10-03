namespace task_7
{
    /// <summary>
    /// Calls all the collections methods.
    /// </summary>
    /// <typeparam name="T">Data type of collection.</typeparam>
    public class CallFunctions<T> where T : IComparable<T>
    {
        private static string timeSpentOnAdd = "Time spent on Add: {0:f2}";
        private static string timeSpentOnInsert = "Time spent on Insert: {0:f2}";
        private static string timeSpentOnRemove = "Time spent on Remove: {0:f2}";
        private static string timeSpentOnFind = "Time spent on Find: {0:f2}";
        private static string timeSpentOnSort = "Time spent on Sort: {0:f2}";
        private static string timeSpentOnContains = "Time spent on Contains: {0:f2}";

        public CallFunctions() { }

        /// <summary>
        /// Calls all List<T> methods.
        /// </summary>
        /// <param name="listAnalyser">List to analyse.</param>
        public void CallAllFunctions(ListAnalyser<T> listAnalyser)
        {
            Console.WriteLine(timeSpentOnAdd, listAnalyser.Add());
            Console.WriteLine(timeSpentOnInsert, listAnalyser.Insert());
            Console.WriteLine(timeSpentOnRemove, listAnalyser.Remove());
            Console.WriteLine(timeSpentOnFind, listAnalyser.Find());
            Console.WriteLine(timeSpentOnSort, listAnalyser.Sort());
            Console.WriteLine(timeSpentOnContains, listAnalyser.Contains());
        }

        /// <summary>
        /// Calls all LinkedList<T> methods.
        /// </summary>
        /// <param name="linkedListAnalyser">LinkedList to analyse.</param>
        public void CallAllFunctions(LinkedListAnalyser<T> linkedListAnalyser)
        {
            Console.WriteLine(timeSpentOnAdd, linkedListAnalyser.Add());
            Console.WriteLine(timeSpentOnInsert, linkedListAnalyser.Insert());
            Console.WriteLine(timeSpentOnRemove, linkedListAnalyser.Remove());
            Console.WriteLine(timeSpentOnFind, linkedListAnalyser.Find());
            Console.WriteLine(timeSpentOnContains, linkedListAnalyser.Contains());
        }

        /// <summary>
        /// Calls all Queue<T> methods.
        /// </summary>
        /// <param name="queueAnalyser">Queue to analyse.</param>
        public void CallAllFunctions(QueueAnalyser<T> queueAnalyser)
        {
            Console.WriteLine(timeSpentOnAdd, queueAnalyser.Add());
            Console.WriteLine(timeSpentOnRemove, queueAnalyser.Remove());
            Console.WriteLine(timeSpentOnContains, queueAnalyser.Contains());
        }

        /// <summary>
        /// Calls all Stack<T> methods.
        /// </summary>
        /// <param name="stackAnalyser">Stack to analyse.</param>
        public void CallAllFunctions(StackAnalyser<T> stackAnalyser)
        {
            Console.WriteLine(timeSpentOnAdd, stackAnalyser.Add());
            Console.WriteLine(timeSpentOnRemove, stackAnalyser.Remove());
            Console.WriteLine(timeSpentOnContains, stackAnalyser.Contains());
        }

        /// <summary>
        /// Calls all Dictionary<T> methods.
        /// </summary>
        /// <param name="dictionaryAnalyser">Dictionary to analyse.</param>
        public void CallAllFunctions(DictionaryAnalyser<T> dictionaryAnalyser)
        {
            Console.WriteLine(timeSpentOnAdd, dictionaryAnalyser.Add());
            Console.WriteLine(timeSpentOnRemove, dictionaryAnalyser.Remove());
            Console.WriteLine(timeSpentOnContains, dictionaryAnalyser.Contains());
        }

        /// <summary>
        /// Calls all HashSet<T> methods.
        /// </summary>
        /// <param name="hashSetAnalyser">HashSet to analyse.</param>
        public void CallAllFunctions(HashSetAnalyser<T> hashSetAnalyser)
        {
            Console.WriteLine(timeSpentOnAdd, hashSetAnalyser.Add());
            Console.WriteLine(timeSpentOnRemove, hashSetAnalyser.Remove());
            Console.WriteLine(timeSpentOnContains, hashSetAnalyser.Contains());
        }
    }
}
