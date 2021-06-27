using NUnit.Framework;
using System.Linq;
using System.Text;

namespace Algorithms.DataStructure.SymbolTable
{
    [TestFixture]
    public class BinarySearchTreeTestFixture
    {
        [TestCase("Search Example", 6)]
        [TestCase("I am the ruler", 5)]
        [TestCase("This is a longer test case for sysbol table based on binary search tree", 7)]
        public void TestHeight(string text, int expected)
        {
            var tree = new BinarySearchTree<char, int>();
            int i = 1;
            foreach (var ch in text)
            {
                tree.Put(ch, i);
            }
            Assert.AreEqual(expected, tree.Height);
            Assert.AreEqual(expected, tree.HeightWithRecursion);
        }

        [TestCase("Search Example", "S Eeacrhmlpx")]
        [TestCase("I am the ruler", "I amheltru")]
        public void TestPreOrderTraversal(string text, string expected)
        {
            var tree = new BinarySearchTree<char, int>();
            int i = 1;
            foreach (var ch in text)
            {
                tree.Put(ch, i);
            }
            var keys = tree.PreOrder().ToArray();
            StringBuilder stringBuilder = new();
            stringBuilder.Append(keys);
            var result = stringBuilder.ToString();
            Assert.AreEqual(expected, result);
        }

        [TestCase("Search Example", " ESacehlmprx")]
        [TestCase("I am the ruler", " Iaehlmrtu")]
        public void TestInOrderTraversal(string text, string expected)
        {
            var tree = new BinarySearchTree<char, int>();
            int i = 1;
            foreach (var ch in text)
            {
                tree.Put(ch, i);
            }
            var keys = tree.InOrder().ToArray();
            StringBuilder stringBuilder = new();
            stringBuilder.Append(keys);
            var result = stringBuilder.ToString();
            Assert.AreEqual(expected, result);
        }

        [TestCase("Search Example", "E calpmhxreS")]
        [TestCase("I am the ruler", " elhrutmaI")]
        public void TestPostOrderTraversal(string text, string expected)
        {
            var tree = new BinarySearchTree<char, int>();
            int i = 1;
            foreach (var ch in text)
            {
                tree.Put(ch, i);
            }
            var keys = tree.PostOrder().ToArray();
            StringBuilder stringBuilder = new();
            stringBuilder.Append(keys);
            var result = stringBuilder.ToString();
            Assert.AreEqual(expected, result);
        }
    }
}
