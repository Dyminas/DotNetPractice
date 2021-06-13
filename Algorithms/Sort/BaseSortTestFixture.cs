using NUnit.Framework;
using System;
using System.Diagnostics;

namespace Algorithms.Sort
{
    public class BaseSortTestFixture
    {
        private int[] _array;
        private const int _size = 50000;
        private const int _max = 50000;


        [SetUp]
        public void SetUp()
        {
            _array = new int[_size];
            var random = new Random();
            for (int i = 0; i < _array.Length; i++)
            {
                _array[i] = random.Next(1, _max);
            }
        }

        protected void ExecuteSortAndCheck(Action<int[]> sort)
        {
            Stopwatch stopWatch = Stopwatch.StartNew();
            sort(_array);
            stopWatch.Stop();
            Console.WriteLine($"Sorting Time: {stopWatch.ElapsedMilliseconds} ms");
            CheckAscendingArray();
        }

        private void CheckAscendingArray()
        {
            for (int i = 0; i < _array.Length - 2; i++)
            {
                Assert.IsTrue(_array[i] <= _array[i + 1]);
            }
        }

        protected void PrintArray()
        {
            for (int i = 0; i < _array.Length; i++)
            {
                Console.WriteLine(_array[i]);
            }
        }
    }
}
