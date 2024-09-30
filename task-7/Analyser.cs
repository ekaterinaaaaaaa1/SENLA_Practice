﻿using System.Diagnostics;

namespace task_7
{
    public abstract class Analyser<T>
    {
        public DataGenerator<T> dataGenerator;
        public Stopwatch stopWatch;

        public int LengthOfArray { get; }
        public int NumberOfCalls { get; }

        public Analyser(int lengthOfArray, int numberOfCalls)
        {
            LengthOfArray = lengthOfArray;
            NumberOfCalls = numberOfCalls;
            dataGenerator = new DataGenerator<T>(lengthOfArray, numberOfCalls);
            stopWatch = new Stopwatch();
        }

        public abstract long Add();
        public abstract long Insert();
        public abstract long Delete();
        public abstract long Find();
        public abstract long Sort();
    }
}
