using NUnit.Framework;
using System;
using System.Diagnostics;

namespace Algorithms.Sort
{
    public class BaseSortTestFixture
    {
        private IComparable[] _array;
        private const int _size = 50000;
        private const int _max = 50000;


        [SetUp]
        public void SetUp()
        {
            _array = new IComparable[_size];
            var random = new Random();
            for (int i = 0; i < _array.Length; i++)
            {
                _array[i] = random.Next(1, _max);
            }
        }

        protected void ExecuteSortAndCheck(Action<IComparable[]> sort)
        {
            Stopwatch stopWatch = Stopwatch.StartNew();
            sort(_array);
            stopWatch.Stop();
            Console.WriteLine($"Sorting Time: {stopWatch.ElapsedMilliseconds} ms");
            CheckAscendingArray();
        }

        protected static void Swap(IComparable[] array, int i, int j)
        {
            IComparable temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }

        private void CheckAscendingArray()
        {
            for (int i = 0; i < _array.Length - 2; i++)
            {
                Assert.LessOrEqual(_array[i], _array[i + 1]);
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
