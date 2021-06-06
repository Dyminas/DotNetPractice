using NUnit.Framework;

namespace Algorithms.Sort
{
    [TestFixture]
    public class InsertionSortTestFixture : BaseSortTestFixture
    {
        [Test]
        public void TestInsertionSort()
        {
            InsertionSort(_array);
            CheckAscendingArray();
        }

        private static void InsertionSort(int[] array)
        {
            int temp;
            for (int i = 1; i < array.Length; i++)
            {
                for (int j = i; j > 0; j--)
                {
                    if (array[j - 1] > array[j])
                    {
                        temp = array[j - 1];
                        array[j - 1] = array[j];
                        array[j] = temp;
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
    }
}
