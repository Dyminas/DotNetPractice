using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithms.DataStructure.Heap.Test
{
    [TestFixture]
    public class IndexMaxHeapTestFixture
    {
        [Test]
        public void TestIndexMaxHeap()
        {
            const int capacity = 15;
            const int count = 20;
            const int maxItemValue = 1000;
            int[] skipIndex = new int[] { 3, 17 };

            var heap = new IndexMaxHeap<int>(capacity);
            var random = new Random();
            var insertedIndex = new List<int>();

            for (int i = 0; i < count; i++)
            {
                if (skipIndex.Contains(i)) continue;
                heap.Insert(i, random.Next(1, maxItemValue));
                heap.CheckIndexes();
                heap.CheckMaxHeap();
            }

            foreach (var index in insertedIndex)
            {
                heap.Change(index, random.Next(1, maxItemValue));
                heap.CheckIndexes();
                heap.CheckMaxHeap();
            }

            while (!heap.IsEmpty)
            {
                Console.WriteLine(heap.ExtractMax());
                heap.CheckIndexes();
                heap.CheckMaxHeap();
            }
        }
    }
}
