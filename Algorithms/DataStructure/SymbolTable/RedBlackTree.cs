using System;
using System.Collections.Generic;

namespace Algorithms.DataStructure.SymbolTable
{
    public class RedBlackTree<TKey, TValue> : BinarySearchTree<TKey, TValue>
        where TKey : IComparable<TKey>
    {
        private const bool Red = true;
        private const bool Black = false;

        public override void Put(TKey key, TValue value)
        {
            if (null == key)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (null == _root)
            {
                _root = new Node(key, value, Black);
                _size = 1;
                return;
            }

            // The height of Red Black Tree is not greater than 2lgN + 1
            int maxHeight = ((int)Math.Log2(_size)) * 2 + 1;
            Stack<Node> path = new(maxHeight);

            Node node = _root;
            int cmp = 0;

            while (null != node)
            {
                path.Push(node);
                cmp = key.CompareTo(node.Key);
                if (cmp < 0)
                {
                    node = node.Left;
                }
                else if (cmp > 0)
                {
                    node = node.Right;
                }
                else
                {
                    node.Value = value;
                    return;
                }
            }

            node = path.Pop();
            if (cmp < 0)
            {
                node.Left = new Node(key, value, Red);
            }
            else
            {
                node.Right = new Node(key, value, Red);
            }

            while (path.TryPop(out Node parent))
            {
                cmp = node.Key.CompareTo(parent.Key);
                if (cmp < 0)
                {
                    parent.Left = Balance(node);
                }
                else
                {
                    parent.Right = Balance(node);
                }
                node = parent;
            }

            _root = Balance(node);
            _root.Color = Black;
            _size++;
        }

        private static Node Balance(Node node)
        {
            if (null == node)
            {
                return null;
            }

            if (Red == node.Right?.Color && Red != node.Left?.Color)
            {
                node = RotateLeft(node);
            }
            if (Red == node.Left?.Color && Red == node.Left?.Left?.Color)
            {
                node = RotateRight(node);
            }
            if (Red == node.Right?.Color && Red == node.Left?.Color)
            {
                FlipColors(node);
            }
            return node;
        }

        private static Node RotateLeft(Node node)
        {
            Node result = node.Right;
            node.Right = result.Left;
            result.Left = node;
            result.Color = node.Color;
            node.Color = Red;
            return result;
        }

        private static Node RotateRight(Node node)
        {
            Node result = node.Left;
            node.Left = result.Right;
            result.Right = node;
            result.Color = node.Color;
            node.Color = Red;
            return result;
        }

        private static void FlipColors(Node node)
        {
            node.Color = !node.Color;
            node.Left.Color = !node.Left.Color;
            node.Right.Color = !node.Right.Color;
        }

        public void DeleteMin()
        {
            if (null == _root)
            {
                throw new InvalidOperationException("Cannot delete item from an empty tree!");
            }

            if (Red != _root.Left?.Color && Red != _root.Right?.Color)
            {
                _root.Color = Red;
            }

            _root = GetAndDeleteMin(_root, out _);
            --_size;

            if (null != _root)
            {
                _root.Color = Black;
            }
        }

        // Return the gived tree after delete the min node from it, also set "min" with the deleted node.
        // Remember to decrease "_size" after called this methed.
        private Node GetAndDeleteMin(Node node, out Node min)
        {
            int maxHeight = ((int)Math.Log2(_size)) * 2;
            Stack<Node> path = new(maxHeight);

            while (null != node.Left)
            {
                if (Red != node.Left.Color && Red != node.Left.Left?.Color)
                {
                    node = MoveLeftRed(node);
                }
                path.Push(node);
                node = node.Left;
            }

            min = node;

            if (path.TryPop(out node))
            {
                node.Left = Balance(min.Right);
            }
            else
            {
                return Balance(min.Right);
            }

            while (path.TryPop(out Node parent))
            {
                parent.Left = Balance(node);
                node = parent;
            }

            return Balance(node);
        }

        private static Node MoveLeftRed(Node node)
        {
            FlipColors(node);
            if (Red == node.Right.Left?.Color)
            {
                node.Right = RotateRight(node.Right);
                node = RotateLeft(node);
                FlipColors(node);
            }
            return node;
        }

        public void DeleteMax()
        {
            if (null == _root)
            {
                throw new InvalidOperationException("Cannot delete item from an empty tree!");
            }

            if (Red != _root.Left?.Color && Red != _root.Right?.Color)
            {
                _root.Color = Red;
            }

            _root = GetAndDeleteMax(_root, out _);
            --_size;

            if (null != _root)
            {
                _root.Color = Black;
            }
        }

        // Return the gived tree after delete the max node from it, also set "max" with the deleted node.
        // Remember to decrease "_size" after called this methed.
        private Node GetAndDeleteMax(Node node, out Node max)
        {
            int maxHeight = ((int)Math.Log2(_size)) * 2;
            Stack<Node> path = new(maxHeight);

            while (true)
            {
                if (Red == node.Left?.Color)
                {
                    node = RotateRight(node);
                }

                if (null == node.Right)
                {
                    break;
                }

                if (Red != node.Right.Color && Red != node.Right.Left?.Color)
                {
                    node = MoveRightRed(node);
                }
                path.Push(node);
                node = node.Right;
            }

            max = node;

            if (path.TryPop(out node))
            {
                node.Right = Balance(max.Left);
            }
            else
            {
                return Balance(max.Left);
            }

            while (path.TryPop(out Node parent))
            {
                parent.Right = Balance(node);
                node = parent;
            }

            return Balance(node);
        }

        private static Node MoveRightRed(Node node)
        {
            FlipColors(node);
            if (Red == node.Left.Left?.Color)
            {
                node = RotateRight(node);
                FlipColors(node);
            }
            return node;
        }

        public override void Delete(TKey key)
        {
            if (null == key)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (Red != _root.Left?.Color && Red != _root.Right?.Color)
            {
                _root.Color = Red;
            }

            int maxHeight = ((int)Math.Log2(_size)) * 2;
            Stack<Node> path = new(maxHeight);
            Node node = _root;

            while (null != node)
            {
                if (key.CompareTo(node.Key) < 0)
                {
                    if (null != node.Left && Red != node.Left.Color && Red != node.Left.Left?.Color)
                    {
                        node = MoveLeftRed(node);
                    }
                    path.Push(node);
                    node = node.Left;
                }
                else
                {
                    // Ensure that right child of current node is not a 2-Node if it exists
                    if (Red == node.Left?.Color)
                    {
                        node = RotateRight(node);
                    }
                    if (null != node.Right && Red != node.Right.Color && Red != node.Right.Left?.Color)
                    {
                        node = MoveRightRed(node);
                    }

                    // node may be changed, we need to re-compare the key
                    if (key.CompareTo(node.Key) == 0)
                    {
                        break;
                    }

                    path.Push(node);
                    node = node.Right;
                }
            }

            if (null == node)
            {
                throw new KeyNotFoundException($"The given key {key} was not present in the tree");
            }

            if (null == node.Left)
            {
                node = node.Right;
            }
            else if (null == node.Right)
            {
                node = node.Left;
            }
            else
            {
                Node newRight = GetAndDeleteMin(node.Right, out Node minOfRight);
                minOfRight.Left = node.Left;
                minOfRight.Right = newRight;
                minOfRight.Color = node.Color;
                node = minOfRight;
            }

            while (path.TryPop(out Node parent))
            {
                if (key.CompareTo(parent.Key) < 0)
                {
                    parent.Left = Balance(node);
                }
                else
                {
                    parent.Right = Balance(node);
                }
                node = parent;
            }

            --_size;
            _root = Balance(node);
            if (null != _root)
            {
                _root.Color = Black;
            }
        }
    }
}
