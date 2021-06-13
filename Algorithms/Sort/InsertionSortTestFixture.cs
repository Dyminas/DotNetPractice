using NUnit.Framework;

namespace Algorithms.Sort
{
    [TestFixture]
    public class InsertionSortTestFixture : BaseSortTestFixture
    {
        [Test]
        public void TestInsertionSort()
        {
            ExecuteSortAndCheck(InsertionSort);
        }

        private static void InsertionSort(int[] array)
        {
            int temp, i, j;
            for (i = 1; i < array.Length; i++)
            {
                temp = array[i];
                for (j = i; j > 0 && temp < array[j - 1]; j--)
                {
                    array[j] = array[j - 1];
                }
                array[j] = temp;
            }
        }
    }
}
