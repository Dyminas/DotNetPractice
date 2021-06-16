using NUnit.Framework;
using System;

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

        private static void ShellSort(IComparable[] array) 
        {
            int gap, i, j;
            IComparable temp;
            for (gap = array.Length / 2; gap > 0; gap /= 2)
            {
                for (i = gap; i < array.Length; i++)
                {
                    temp = array[i];
                    for (j = i; j >= gap && temp.CompareTo(array[j - gap]) < 0; j -= gap)
                    {
                        array[j] = array[j - gap];
                    }
                    array[j] = temp;
                }
            }
        }
    }
}
