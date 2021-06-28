using NUnit.Framework;
using System.Linq;
using System.Text;

namespace Algorithms.DataStructure.SymbolTable
{
    [TestFixture]
    public class RedBlackTreeTestFixture
    {
        private static RedBlackTree<char, int> CreateTreeByText(string text)
        {
            var tree = new RedBlackTree<char, int>();
            int i = 1;
            foreach (var ch in text)
            {
                tree.Put(ch, i++);
            }
            return tree;
        }

        [TestCase("Search Example", 5)]
        public void TestHeight(string text, int expected)
        {
            var tree = CreateTreeByText(text);
            Assert.AreEqual(expected, tree.Height);
            Assert.AreEqual(expected, tree.HeightWithRecursion);
        }

        [TestCase("Search Example", "maE Seclhrpx")]
        public void TestPreOrderTraversal(string text, string expected)
        {
            var tree = CreateTreeByText(text);
            var keys = tree.PreOrder().ToArray();
            StringBuilder stringBuilder = new();
            stringBuilder.Append(keys);
            var result = stringBuilder.ToString();
            Assert.AreEqual(expected, result);
        }

        [TestCase("Search Example", " ESacehlmprx")]
        public void TestInOrderTraversal(string text, string expected)
        {
            var tree = CreateTreeByText(text);
            var keys = tree.InOrder().ToArray();
            StringBuilder stringBuilder = new();
            stringBuilder.Append(keys);
            var result = stringBuilder.ToString();
            Assert.AreEqual(expected, result);
        }

        [TestCase("Search Example", " SEchleapxrm")]
        public void TestPostOrderTraversal(string text, string expected)
        {
            var tree = CreateTreeByText(text);
            var keys = tree.PostOrder().ToArray();
            StringBuilder stringBuilder = new();
            stringBuilder.Append(keys);
            var result = stringBuilder.ToString();
            Assert.AreEqual(expected, result);
        }

        private static RedBlackTree<char, int> TestDeleteMinAndMax(string text, string expectedAfterDeleteMin, string expectedAfterDeleteMax)
        {
            var tree = CreateTreeByText(text);

            tree.DeleteMin();
            var keys = tree.PreOrder().ToArray();
            StringBuilder stringBuilder = new();
            stringBuilder.Append(keys);
            var result = stringBuilder.ToString();
            Assert.AreEqual(expectedAfterDeleteMin, result);
            Assert.AreEqual(expectedAfterDeleteMin.Length, tree.Count);

            tree.DeleteMax();
            keys = tree.PreOrder().ToArray();
            stringBuilder = new();
            stringBuilder.Append(keys);
            result = stringBuilder.ToString();
            Assert.AreEqual(expectedAfterDeleteMax, result);
            Assert.AreEqual(expectedAfterDeleteMax.Length, tree.Count);

            return tree;
        }

        [TestCase("Search Example", "meaSEclhrpx", "eaSEcmlhrp")]
        public void TestDeleteMin(string text, string expectedAfterDeleteMin, string expectedAfterDeleteMax)
        {
            var tree = TestDeleteMinAndMax(text, expectedAfterDeleteMin, expectedAfterDeleteMax);

            tree.DeleteMax();

            while (!tree.IsEmpty)
            {
                tree.DeleteMin();
            }
        }

        [TestCase("Search Example", "meaSEclhrpx", "eaSEcmlhrp")]
        public void TestDeleteMax(string text, string expectedAfterDeleteMin, string expectedAfterDeleteMax)
        {
            var tree = TestDeleteMinAndMax(text, expectedAfterDeleteMin, expectedAfterDeleteMax);

            tree.DeleteMin();

            while (!tree.IsEmpty)
            {
                tree.DeleteMax();
            }
        }

        //[TestCase("Search Example", 'e', "maE Shclrpx")]
        [TestCase("Search Example", 'e', "maE Shclrpx")]
        public void TestDelete(string text, char keyToDelete, string expected)
        {
            var tree = CreateTreeByText(text);

            tree.Delete(keyToDelete);
            var keys = tree.PreOrder().ToArray();
            StringBuilder stringBuilder = new();
            stringBuilder.Append(keys);
            var result = stringBuilder.ToString();
            Assert.AreEqual(expected, result);
            Assert.AreEqual(expected.Length, tree.Count);

            for (int i = 0; i < expected.Length; i++)
            {
                tree.Delete(expected[i]);
            }
            Assert.IsTrue(tree.IsEmpty);
        }
    }
}
