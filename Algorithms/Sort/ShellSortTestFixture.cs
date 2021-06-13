using NUnit.Framework;

namespace Algorithms.Sort
{
    [TestFixture]
    public class ShellSortTestFixture : BaseSortTestFixture
    {
        [Test]
        public void TestShellSort()
        {
            ExecuteSortAndCheck(ShellSort);
        }

        private static void ShellSort(int[] array) 
        {
            int temp, gap, i, j;
            for (gap = array.Length / 2; gap > 0; gap /= 2)
            {
                for (i = gap; i < array.Length; i++)
                {
                    temp = array[i];
                    for (j = i; j >= gap && temp < array[j - gap]; j -= gap)
                    {
                        array[j] = array[j - gap];
                    }
                    array[j] = temp;
                }
            }
        }
    }
}
