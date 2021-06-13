using NUnit.Framework;

namespace Algorithms.Sort
{
    [TestFixture]
    public class QuickSortTestFixture : BaseSortTestFixture
    {
        [Test]
        public void TestQuickSort()
        {
            ExecuteSortAndCheck(QuickSort);
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
                QuickSort(array, left, l - 1);
                QuickSort(array, l + 1, right);
            }
        }
    }
}
