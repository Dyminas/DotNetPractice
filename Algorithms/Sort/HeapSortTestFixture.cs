using Algorithms.DataStructure.MaxHeap;
using NUnit.Framework;
using System;

namespace Algorithms.Sort
{
    [TestFixture]
    public class HeapSortTestFixture : BaseSortTestFixture
    {
        [Test]
        public void TestHeapSort()
        {
            ExecuteSortAndCheck(HeapSort);
        }

        private static void HeapSort(IComparable[] array)
        {
            int length = array.Length;

            // Heapify
            for (int i = (length - 2) / 2; i >= 0; i--)
            {
                MaxHeap<IComparable>.ShiftDown(array, length, i);
            }

            // Sort
            for (int i = length - 1; i > 0; i--)
            {
                Swap(array, i, 0);
                MaxHeap<IComparable>.ShiftDown(array, i, 0);
            }
        }
    }
}
