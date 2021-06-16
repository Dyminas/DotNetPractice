using NUnit.Framework;
using System;

namespace Algorithms.Sort
{
    [TestFixture]
    public class SelectionSortTestFixture : BaseSortTestFixture
    {
        [Test]
        public void TestSelectionSort()
        {
            ExecuteSortAndCheck(SelectionSort);
        }

        private static void SelectionSort(IComparable[] array)
        {
            int tempIndex;
            for (int i = 0; i < array.Length - 1; i++)
            {
                tempIndex = i;
                for (int j = i + 1; j < array.Length; j++)
                {
                    if (array[tempIndex].CompareTo(array[j]) > 0)
                    {
                        tempIndex = j;
                    }
                }
                Swap(array, i, tempIndex);
            }
        }
    }
}
