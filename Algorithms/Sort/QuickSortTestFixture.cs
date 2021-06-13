using NUnit.Framework;
using System;

namespace Algorithms.Sort
{
    [TestFixture]
    public class QuickSortTestFixture : BaseSortTestFixture
    {
        [Test]
        public void TestQuickSortWithTwoWays()
        {
            ExecuteSortAndCheck(QuickSortWithTwoWays);
        }

        private static void QuickSortWithTwoWays(int[] array)
        {
            QuickSortWithTwoWays(array, 0, array.Length - 1);
        }

        private static void QuickSortWithTwoWays(int[] array, int left, int right)
        {
            if (left >= right)
            {
                return;
            }

            int l = left, r = right;

            // We can use a random pivot, but it costs more time here.
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
            QuickSortWithTwoWays(array, left, l - 1);
            QuickSortWithTwoWays(array, l + 1, right);
        }

        [Test]
        public void TestQuickSortWithThreeWays()
        {
            ExecuteSortAndCheck(QuickSortWithThreeWays);
        }

        private static void QuickSortWithThreeWays(int[] array)
        {
            QuickSortWithThreeWays(array, 0, array.Length - 1);
        }

        private static void QuickSortWithThreeWays(int[] array, int left, int right)
        {
            if (left >= right)
            {
                return;
            }

            // We can use a random pivot, but it costs more time here.
            // Swap(array, left, new Random().Next(left, right));
            int temp = array[left];

            int l = left, r = right, i = left + 1;
            int swap;

            while (i <= r)
            {
                if (array[i] < temp)
                {
                    // Function calling will cost more time.
                    // Swap(array, i++, l++);
                    swap = array[i];
                    array[i] = array[l];
                    array[l] = swap;
                    ++i;
                    ++l;
                }
                else if (array[i] > temp)
                {
                    // Swap(array, i, r--);
                    swap = array[i];
                    array[i] = array[r];
                    array[r] = swap;
                    --r;
                }
                else
                {
                    i++;
                }
            }

            QuickSortWithThreeWays(array, left, l - 1);
            QuickSortWithThreeWays(array, r + 1, right);
        }

        private static void Swap(int[] array, int i, int j)
        {
            int temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }
    }
}
