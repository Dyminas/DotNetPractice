using NUnit.Framework;

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

        private static void BubbleSort(int[] array)
        {
            int temp;
            for (int i = 0; i < array.Length - 1; i++)
            {
                for (int j = 1; j < array.Length - i; j++)
                {
                    if (array[j] < array[j - 1])
                    {
                        temp = array[j - 1];
                        array[j - 1] = array[j];
                        array[j] = temp;
                    }
                }
            }
        }
    }
}
