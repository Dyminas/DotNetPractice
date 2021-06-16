using NUnit.Framework;
using System;

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

        private static void InsertionSort(IComparable[] array)
        {
            int i, j;
            IComparable temp;
            for (i = 1; i < array.Length; i++)
            {
                temp = array[i];
                for (j = i; j > 0 && temp.CompareTo(array[j - 1]) < 0; j--)
                {
                    array[j] = array[j - 1];
                }
                array[j] = temp;
            }
        }
    }
}
