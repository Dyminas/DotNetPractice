using NUnit.Framework;

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

        private static void SelectionSort(int[] array)
        {
            int tempIndex, swapTemp;
            for (int i = 0; i < array.Length - 1; i++)
            {
                tempIndex = i;
                for (int j = i + 1; j < array.Length; j++)
                {
                    if (array[tempIndex] > array[j])
                    {
                        tempIndex = j;
                    }
                }

                swapTemp = array[i];
                array[i] = array[tempIndex];
                array[tempIndex] = swapTemp;
            }
        }
    }
}
