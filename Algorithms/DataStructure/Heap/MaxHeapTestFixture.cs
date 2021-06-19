using NUnit.Framework;
using System;

namespace Algorithms.DataStructure.Heap
{
    [TestFixture]
    public class MaxHeapTestFixture
    {
        [Test]
        public void TestMaxHeap()
        {
            const int capacity = 15;
            const int count = 20;
            const int max = 1000;

            var heap = new MaxHeap<int>(capacity);
            var random = new Random();
            for (int i = 0; i < count; i++)
            {
                heap.Insert(random.Next(1, max));
            }
            heap.CheckMaxHeap();

            while (!heap.IsEmpty)
            {
                Console.WriteLine(heap.ExtractMax());
                heap.CheckMaxHeap();
            }
        }
    }
}
