namespace task_7
{
    /// <summary>
    /// Calls all the collections methods.
    /// </summary>
    /// <typeparam name="T">Data type of collection.</typeparam>
    public class CallFunctions<T> where T : IComparable<T>
    {
        public CallFunctions() { }

        /// <summary>
        /// Calls all List<T> methods.
        /// </summary>
        /// <param name="listAnalyser">List to analyse.</param>
        public void CallAllFunctions(ListAnalyser<T> listAnalyser)
        {
            Console.WriteLine("Time spent on Add: {0:f2}", listAnalyser.Add());
            Console.WriteLine("Time spent on Insert: {0:f2}", listAnalyser.Insert());
            Console.WriteLine("Time spent on Remove: {0:f2}", listAnalyser.Remove());
            Console.WriteLine("Time spent on Find: {0:f2}", listAnalyser.Find());
            Console.WriteLine("Time spent on Sort: {0:f2}", listAnalyser.Sort());
            Console.WriteLine("Time spent on Contains: {0:f2}", listAnalyser.Contains());
        }

        /// <summary>
        /// Calls all LinkedList<T> methods.
        /// </summary>
        /// <param name="linkedListAnalyser">LinkedList to analyse.</param>
        public void CallAllFunctions(LinkedListAnalyser<T> linkedListAnalyser)
        {
            Console.WriteLine("Time spent on Add: {0:f2}", linkedListAnalyser.Add());
            Console.WriteLine("Time spent on Insert: {0:f2}", linkedListAnalyser.Insert());
            Console.WriteLine("Time spent on Remove: {0:f2}", linkedListAnalyser.Remove());
            Console.WriteLine("Time spent on Find: {0:f2}", linkedListAnalyser.Find());
            Console.WriteLine("Time spent on Contains: {0:f2}", linkedListAnalyser.Contains());
        }

        /// <summary>
        /// Calls all Queue<T> methods.
        /// </summary>
        /// <param name="queueAnalyser">Queue to analyse.</param>
        public void CallAllFunctions(QueueAnalyser<T> queueAnalyser)
        {
            Console.WriteLine("Time spent on Add: {0:f2}", queueAnalyser.Add());
            Console.WriteLine("Time spent on Remove: {0:f2}", queueAnalyser.Remove());
            Console.WriteLine("Time spent on Contains: {0:f2}", queueAnalyser.Contains());
        }

        /// <summary>
        /// Calls all Stack<T> methods.
        /// </summary>
        /// <param name="stackAnalyser">Stack to analyse.</param>
        public void CallAllFunctions(StackAnalyser<T> stackAnalyser)
        {
            Console.WriteLine("Time spent on Add: {0:f2}", stackAnalyser.Add());
            Console.WriteLine("Time spent on Remove: {0:f2}", stackAnalyser.Remove());
            Console.WriteLine("Time spent on Contains: {0:f2}", stackAnalyser.Contains());
        }

        /// <summary>
        /// Calls all Dictionary<T> methods.
        /// </summary>
        /// <param name="dictionaryAnalyser">Dictionary to analyse.</param>
        public void CallAllFunctions(DictionaryAnalyser<T> dictionaryAnalyser)
        {
            Console.WriteLine("Time spent on Add: {0:f2}", dictionaryAnalyser.Add());
            Console.WriteLine("Time spent on Remove: {0:f2}", dictionaryAnalyser.Remove());
            Console.WriteLine("Time spent on Contains: {0:f2}", dictionaryAnalyser.Contains());
        }

        /// <summary>
        /// Calls all HashSet<T> methods.
        /// </summary>
        /// <param name="hashSetAnalyser">HashSet to analyse.</param>
        public void CallAllFunctions(HashSetAnalyser<T> hashSetAnalyser)
        {
            Console.WriteLine("Time spent on Add: {0:f2}", hashSetAnalyser.Add());
            Console.WriteLine("Time spent on Remove: {0:f2}", hashSetAnalyser.Remove());
            Console.WriteLine("Time spent on Contains: {0:f2}", hashSetAnalyser.Contains());
        }
    }
}
