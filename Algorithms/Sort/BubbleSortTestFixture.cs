using NUnit.Framework;
using System;

namespace Algorithms.Sort
{
    [TestFixture]
    public class BubbleSortTestFixture : BaseSortTestFixture
    {
        [Test]
        public void TestBubbleSort()
        {
            ExecuteSortAndCheck(BubbleSort);
        }

        private static void BubbleSort(IComparable[] array)
        {
            for (int i = 0; i < array.Length - 1; i++)
            {
                for (int j = 1; j < array.Length - i; j++)
                {
                    if (array[j].CompareTo(array[j - 1]) < 0)
                    {
                        Swap(array, j, j - 1);
                    }
                }
            }
        }
    }
}
