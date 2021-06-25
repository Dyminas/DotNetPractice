using NUnit.Framework;
using System.Collections.Generic;

namespace Algorithms.DataStructure.SymbolTable
{
    [TestFixture]
    public class SymbolTableTestFixture
    {
        private static void TestSymbolTable(SymbolTable<char, int> symbolTable, string text, char invalidKey)
        {
            var reference = new Dictionary<char, int>();
            int i = 1;

            Assert.IsTrue(symbolTable.IsEmpty);

            foreach (var ch in text)
            {
                symbolTable.Put(ch, i);
                reference[ch] = i;
                ++i;
            }

            Assert.AreEqual(reference.Count, symbolTable.Count);
            Assert.IsFalse(symbolTable.ContainsKey(invalidKey));

            foreach (var pair in reference)
            {
                Assert.AreEqual(pair.Value, symbolTable.GetValue(pair.Key));
                symbolTable.Delete(pair.Key);
                Assert.IsFalse(symbolTable.ContainsKey(pair.Key));
            }

            Assert.IsTrue(symbolTable.IsEmpty);
        }

        [TestCase("Search Example", 'z')]
        [TestCase("I am the ruler", 'x')]
        public void TestSymbolTableBasedOnLinkedList(string text, char invalidKey)
        {
            var symbolTable = new SymbolTableBasedOnLinkedList<char, int>();
            TestSymbolTable(symbolTable, text, invalidKey);
        }

        [TestCase("Search Example", 'z')]
        [TestCase("I am the ruler", 'x')]
        public void TestSymbolTableBasedOnOrderedArray(string text, char invalidKey)
        {
            var symbolTable = new SymbolTableBasedOnOrderedArray<char, int>();
            TestSymbolTable(symbolTable, text, invalidKey);
        }

        [TestCase("Search Example", 'z')]
        [TestCase("I am the ruler", 'x')]
        [TestCase("This is a longer test case for sysbol table based on binary search tree", 'm')]
        public void TestBinarySearchTree(string text, char invalidKey)
        {
            var symbolTable = new BinarySearchTree<char, int>();
            TestSymbolTable(symbolTable, text, invalidKey);
        }
    }
}
