using NUnit.Framework;
using System;

namespace Algorithms.DataStructure.MaxHeap
{
    [TestFixture]
    public class MaxHeapTestFixture
    {
        [Test]
        public void TestMaxHeap()
        {
            const int capacity = 20;
            const int max = 1000;

            var heap = new MaxHeap<int>(capacity);
            var random = new Random();
            for (int i = 0; i < capacity; i++)
            {
                heap.Add(random.Next(1, max));
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
