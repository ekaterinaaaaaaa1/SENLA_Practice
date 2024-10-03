namespace task_7
{
    class Program
    {
        private const int LENGTH_OF_ARRAY = 1000;
        private const int NUMBER_OF_CALLS = 100;
        
        static void Main(string[] args)
        {
            CallFunctions<int> intCallFunctions = new CallFunctions<int>();
            CallFunctions<double> doubleCallFunctions = new CallFunctions<double>();
            CallFunctions<string> stringCallFunctions = new CallFunctions<string>();

            Console.WriteLine("-------List Analyser-------");
            Console.WriteLine("-----Int List Analyser-----");
            intCallFunctions.CallAllFunctions(new ListAnalyser<int>(LENGTH_OF_ARRAY, NUMBER_OF_CALLS));
            Console.WriteLine("-----Double List Analyser-----");
            doubleCallFunctions.CallAllFunctions(new ListAnalyser<double>(LENGTH_OF_ARRAY, NUMBER_OF_CALLS));
            Console.WriteLine("-----String List Analyser-----");
            stringCallFunctions.CallAllFunctions(new ListAnalyser<string>(LENGTH_OF_ARRAY, NUMBER_OF_CALLS));

            Console.WriteLine("\n-------LinkedList Analyser-------");
            Console.WriteLine("-----Int LinkedList Analyser-----");
            intCallFunctions.CallAllFunctions(new LinkedListAnalyser<int>(LENGTH_OF_ARRAY, NUMBER_OF_CALLS));
            Console.WriteLine("-----Double LinkedList Analyser-----");
            doubleCallFunctions.CallAllFunctions(new LinkedListAnalyser<double>(LENGTH_OF_ARRAY, NUMBER_OF_CALLS));
            Console.WriteLine("-----String LinkedList Analyser-----");
            stringCallFunctions.CallAllFunctions(new LinkedListAnalyser<string>(LENGTH_OF_ARRAY, NUMBER_OF_CALLS));

            Console.WriteLine("\n-------Queue Analyser-------");
            Console.WriteLine("-----Int Queue Analyser-----");
            intCallFunctions.CallAllFunctions(new QueueAnalyser<int>(LENGTH_OF_ARRAY, NUMBER_OF_CALLS));
            Console.WriteLine("-----Double Queue Analyser-----");
            doubleCallFunctions.CallAllFunctions(new QueueAnalyser<double>(LENGTH_OF_ARRAY, NUMBER_OF_CALLS));
            Console.WriteLine("-----String Queue Analyser-----");
            stringCallFunctions.CallAllFunctions(new QueueAnalyser<string>(LENGTH_OF_ARRAY, NUMBER_OF_CALLS));

            Console.WriteLine("\n-------Stack Analyser-------");
            Console.WriteLine("-----Int Stack Analyser-----");
            intCallFunctions.CallAllFunctions(new StackAnalyser<int>(LENGTH_OF_ARRAY, NUMBER_OF_CALLS));
            Console.WriteLine("-----Double Stack Analyser-----");
            doubleCallFunctions.CallAllFunctions(new StackAnalyser<double>(LENGTH_OF_ARRAY, NUMBER_OF_CALLS));
            Console.WriteLine("-----String Stack Analyser-----");
            stringCallFunctions.CallAllFunctions(new StackAnalyser<string>(LENGTH_OF_ARRAY, NUMBER_OF_CALLS));

            Console.WriteLine("\n-------Dictionary Analyser-------");
            Console.WriteLine("-----Int Dictionary Analyser-----");
            intCallFunctions.CallAllFunctions(new DictionaryAnalyser<int>(LENGTH_OF_ARRAY, NUMBER_OF_CALLS));
            Console.WriteLine("-----Double Dictionary Analyser-----");
            doubleCallFunctions.CallAllFunctions(new DictionaryAnalyser<double>(LENGTH_OF_ARRAY, NUMBER_OF_CALLS));
            Console.WriteLine("-----String Dictionary Analyser-----");
            stringCallFunctions.CallAllFunctions(new DictionaryAnalyser<string>(LENGTH_OF_ARRAY, NUMBER_OF_CALLS));

            Console.WriteLine("\n-------HashSet Analyser-------");
            Console.WriteLine("-----Int HashSet Analyser-----");
            intCallFunctions.CallAllFunctions(new HashSetAnalyser<int>(LENGTH_OF_ARRAY, NUMBER_OF_CALLS));
            Console.WriteLine("-----Double HashSet Analyser-----");
            doubleCallFunctions.CallAllFunctions(new HashSetAnalyser<double>(LENGTH_OF_ARRAY, NUMBER_OF_CALLS));
            Console.WriteLine("-----String HashSet Analyser-----");
            stringCallFunctions.CallAllFunctions(new HashSetAnalyser<string>(LENGTH_OF_ARRAY, NUMBER_OF_CALLS));
        }
    }
}