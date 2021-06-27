using System;
using System.Collections.Generic;

namespace Algorithms.DataStructure.SymbolTable
{
    public class BinarySearchTree<TKey, TValue> : SymbolTable<TKey, TValue>
        where TKey : IComparable<TKey>
    {
        protected class Node
        {
            public TKey Key { get; set; }
            public TValue Value { get; set; }
            public Node Left { get; set; }
            public Node Right { get; set; }
            public bool Color { get; set; }

            public Node(TKey key, TValue value)
            {
                Key = key;
                Value = value;
            }

            public Node(TKey key, TValue value, bool color)
            {
                Key = key;
                Value = value;
                Color = color;
            }
        }

        protected Node _root;

        public int Height
        {
            get
            {
                if (null == _root)
                {
                    return 0;
                }

                Queue<Node> queue = new();
                Node node;
                int height = 0;
                queue.Enqueue(_root);
                queue.Enqueue(null);

                while (queue.Count > 0)
                {
                    node = queue.Dequeue();
                    if (null != node)
                    {
                        if (null != node.Left)
                        {
                            queue.Enqueue(node.Left);
                        }
                        if (null != node.Right)
                        {
                            queue.Enqueue(node.Right);
                        }
                    }
                    else
                    {
                        height++;
                        if (queue.Count > 0)
                        {
                            queue.Enqueue(null);
                        }
                    }
                }

                return height;
            }
        }

        public int HeightWithRecursion => GetHeight(_root);

        private int GetHeight(Node node)
        {
            if (null == node)
            {
                return 0;
            }
            return 1 + Math.Max(GetHeight(node.Left), GetHeight(node.Right));
        }

        private static bool Find(Node node, TKey key, out Node target)
        {
            bool found = false;
            target = null;
            int cmp;

            while (null != node)
            {
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
                    found = true;
                    target = node;
                    break;
                }
            }

            return found;
        }

        public override bool ContainsKey(TKey key)
        {
            if (null == key)
            {
                throw new ArgumentNullException(nameof(key));
            }

            return Find(_root, key, out _);
        }

        public override TValue GetValue(TKey key)
        {
            if (null == key)
            {
                throw new ArgumentNullException(nameof(key));
            }

            bool found = Find(_root, key, out Node target);

            if (!found)
            {
                throw new KeyNotFoundException($"The given key {key} was not present in the tree");
            }

            return target.Value;
        }

        public override bool TryGetValue(TKey key, out TValue value)
        {
            if (null == key)
            {
                throw new ArgumentNullException(nameof(key));
            }

            bool found = Find(_root, key, out Node target);
            value = found ? target.Value : default;
            return found;
        }

        public override void Put(TKey key, TValue value)
        {
            if (null == key)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (null == _root)
            {
                _root = new Node(key, value);
                _size = 1;
                return;
            }

            Node node = _root, parent = null;
            int cmp = 0;

            while (null != node)
            {
                parent = node;
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

            if (cmp < 0)
            {
                parent.Left = new Node(key, value);
            }
            else
            {
                parent.Right = new Node(key, value);
            }
            _size++;
        }

        // Return the gived tree after delete the min node, also set "min" with the deleted node.
        // Remember to decrease "_size" after called this methed.
        private static Node GetAndDeleteMin(Node node, out Node min)
        {
            Node parent = null;
            min = node;

            while (null != min.Left)
            {
                parent = min;
                min = min.Left;
            }

            if (null != parent)
            {
                parent.Left = min.Right;
            }
            else
            {
                node = min.Right;
            }
            return node;
        }

        public override void Delete(TKey key)
        {
            if (null == key)
            {
                throw new ArgumentNullException(nameof(key));
            }

            Node node = _root, parent = null, newNode;
            int cmp;
            while (null != node)
            {
                cmp = key.CompareTo(node.Key);
                if (cmp < 0)
                {
                    parent = node;
                    node = node.Left;
                }
                else if (cmp > 0)
                {
                    parent = node;
                    node = node.Right;
                }
                else
                {
                    if (null == node.Left)
                    {
                        newNode = node.Right;
                    }
                    else if (null == node.Right)
                    {
                        newNode = node.Left;
                    }
                    else
                    {
                        newNode = GetAndDeleteMin(node.Right, out Node minOfRight);
                        minOfRight.Left = node.Left;
                        minOfRight.Right = newNode;
                        newNode = minOfRight;
                    }

                    if (null == parent)
                    {
                        _root = newNode;
                    }
                    else if (parent.Left?.Key.CompareTo(key) == 0)
                    {
                        parent.Left = newNode;
                    }
                    else
                    {
                        parent.Right = newNode;
                    }
                    _size--;
                    return;
                }
            }

            throw new KeyNotFoundException($"The given key {key} was not present in the tree");
        }

        public IEnumerable<TKey> PreOrder()
        {
            List<TKey> result = new(_size);

            if (null == _root)
            {
                return result;
            }

            Stack<Node> stack = new();
            stack.Push(_root);

            while (stack.TryPop(out Node node))
            {
                result.Add(node.Key);
                if (null != node.Right)
                {
                    stack.Push(node.Right);
                }
                if (null != node.Left)
                {
                    stack.Push(node.Left);
                }
            }

            return result;
        }

        public IEnumerable<TKey> InOrder()
        {
            List<TKey> result = new(_size);

            if (null == _root)
            {
                return result;
            }

            Stack<Node> stack = new();
            Node node = _root;

            while (null != node || stack.Count > 0)
            {
                if (null != node)
                {
                    stack.Push(node);
                    node = node.Left;
                }
                else
                {
                    stack.TryPop(out node);
                    result.Add(node.Key);
                    node = node?.Right;
                }
            }

            return result;
        }

        // Visit left children and right children in the opposite order of "PreOrder"
        // Then reverse the result sequence
        public IEnumerable<TKey> PostOrder()
        {
            Stack<TKey> result = new(_size);

            if (null == _root)
            {
                return result;
            }

            Stack<Node> stack = new();
            stack.Push(_root);

            while (stack.TryPop(out Node node))
            {
                result.Push(node.Key);
                if (null != node.Left)
                {
                    stack.Push(node.Left);
                }
                if (null != node.Right)
                {
                    stack.Push(node.Right);
                }
            }

            return result.ToArray();
        }
    }
}
