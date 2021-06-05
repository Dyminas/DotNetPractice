using NUnit.Framework;
using System;

namespace Algorithms.Sort
{
    [TestFixture]
    public class QuickSortTestFixture
    {
        private int[] _array;
        private const int SIZE = 256;

        [SetUp]
        public void SetUp()
        {
            _array = new int[SIZE];
            var random = new Random();
            for (int i = 0; i < _array.Length; i++)
            {
                _array[i] = random.Next(1, 3072);
            }
        }

        [Test]
        public void TestQuickSort()
        {
            QuickSort(_array);
            for (int i = 0; i < _array.Length - 2; i++)
            {
                Assert.IsTrue(_array[i] <= _array[i + 1]);
            }
        }

        private static void QuickSort(int[] array)
        {
            QuickSort(array, 0, array.Length - 1);
        }

        private static void QuickSort(int[] array, int left, int right)
        {
            if (left < right)
            {
                int l = left, r = right;
                int temp = array[l];
                while (l < r)
                {
                    while (l < r && array[r] >= temp)
                    {
                        r--;
                    }
                    if (l < r)
                    {
                        array[l] = array[r];
                    }

                    while (l < r && array[l] < temp)
                    {
                        l++;
                    }
                    if (l < r)
                    {
                        array[r] = array[l];
                    }
                }
                array[r] = temp;
                QuickSort(array, left, l);
                QuickSort(array, l + 1, right);
            }
        }
    }
}
