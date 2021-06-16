using NUnit.Framework;
using System;

namespace Algorithms.Sort
{
    [TestFixture]
    public class MergeSortTestFixture : BaseSortTestFixture
    {
        [Test]
        public void TestMergeSort()
        {
            ExecuteSortAndCheck(MergeSort);
        }

        private static void MergeSort(IComparable[] array)
        {
            MergeSort(array, 0, array.Length - 1);
        }

        private static void MergeSort(IComparable[] array, int left, int right)
        {
            if (left >= right)
            {
                return;
            }

            int middle = (left + right) / 2;
            MergeSort(array, left, middle);
            MergeSort(array, middle + 1, right);

            if (array[middle].CompareTo(array[middle + 1]) > 0)
            {
                Merge(array, left, middle, right);
            }
        }

        private static void Merge(IComparable[] array, int left, int middle, int right)
        {
            int length = right - left + 1;
            int[] copy = new int[length];
            Array.Copy(array, left, copy, 0, length);

            int l1 = 0, r1 = middle - left,
                l2 = r1 + 1, r2 = length - 1,
                i = left;

            while (l1 <= r1 && l2 <= r2)
            {
                if (copy[l1] < copy[l2])
                {
                    array[i++] = copy[l1++];
                }
                else
                {
                    array[i++] = copy[l2++];
                }
            }

            while (l1 <= r1)
            {
                array[i++] = copy[l1++];
            }

            while (l2 <= r2)
            {
                array[i++] = copy[l2++];
            }
        }
    }
}
