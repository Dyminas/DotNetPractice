using NUnit.Framework;
using System;

namespace Algorithms.Sort
{
    public class BaseSortTestFixture
    {
        protected int[] _array;
        protected const int SIZE = 30000;

        [SetUp]
        public void SetUp()
        {
            _array = new int[SIZE];
            var random = new Random();
            for (int i = 0; i < _array.Length; i++)
            {
                _array[i] = random.Next(1, 30000);
            }
        }

        protected void CheckAscendingArray()
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
