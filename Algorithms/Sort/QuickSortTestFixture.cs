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

        private static void QuickSortWithTwoWays(IComparable[] array)
        {
            QuickSortWithTwoWays(array, 0, array.Length - 1);
        }

        private static void QuickSortWithTwoWays(IComparable[] array, int left, int right)
        {
            if (left >= right)
            {
                return;
            }

            int l = left, r = right;

            // We can use a random pivot, but it costs more time here.
            IComparable temp = array[l];

            while (l < r)
            {
                while (l < r && array[r].CompareTo(temp) >= 0)
                {
                    r--;
                }
                if (l < r)
                {
                    array[l] = array[r];
                }

                while (l < r && array[l].CompareTo(temp) < 0)
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

        private static void QuickSortWithThreeWays(IComparable[] array)
        {
            QuickSortWithThreeWays(array, 0, array.Length - 1);
        }

        private static void QuickSortWithThreeWays(IComparable[] array, int left, int right)
        {
            if (left >= right)
            {
                return;
            }

            // We can use a random pivot, but it costs more time here.
            // Swap(array, left, new Random().Next(left, right));
            IComparable temp = array[left];

            int l = left, r = right, i = left + 1;

            while (i <= r)
            {
                if (array[i].CompareTo(temp) < 0)
                {
                    Swap(array, i++, l++);
                }
                else if (array[i].CompareTo(temp) > 0)
                {
                    Swap(array, i, r--);
                }
                else
                {
                    i++;
                }
            }

            QuickSortWithThreeWays(array, left, l - 1);
            QuickSortWithThreeWays(array, r + 1, right);
        }
    }
}
