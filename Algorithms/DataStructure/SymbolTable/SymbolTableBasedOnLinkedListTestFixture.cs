using NUnit.Framework;
using System.Collections.Generic;

namespace Algorithms.DataStructure.SymbolTable
{
    [TestFixture]
    public class SymbolTableBasedOnLinkedListTestFixture
    {
        [TestCase("Search Example", 'z')]
        [TestCase("I am the ruler", 'x')]
        public void TestSymbolTable(string text, char invalidKey)
        {
            var table = new SymbolTableBasedOnLinkedList<char, int>();
            var reference = new Dictionary<char, int>();
            int i = 1;

            Assert.IsTrue(table.IsEmpty);

            foreach (var ch in text)
            {
                table.Put(ch, i);
                reference[ch] = i;
                ++i;
            }

            Assert.AreEqual(reference.Count, table.Count);
            Assert.IsFalse(table.ContainsKey(invalidKey));

            foreach (var pair in reference)
            {
                Assert.AreEqual(pair.Value, table.GetValue(pair.Key));
                table.Delete(pair.Key);
                Assert.IsFalse(table.ContainsKey(pair.Key));
            }

            Assert.IsTrue(table.IsEmpty);
        }
    }
}
