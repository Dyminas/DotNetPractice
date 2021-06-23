using System;
using System.Collections.Generic;

namespace Algorithms.DataStructure.SymbolTable
{
    public class BinarySearchTree<TKey, TValue> : SymbolTable<TKey, TValue>
        where TKey : IComparable<TKey>
    {
        private class Node
        {
            public TKey Key { get; set; }
            public TValue Value { get; set; }
            public Node Left { get; set; }
            public Node Right { get; set; }

            public Node(TKey key, TValue value)
            {
                Key = key;
                Value = value;
            }
        }

        private Node _root;

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
                throw new KeyNotFoundException($"The given key {key} was not present in the collection");
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
                ++_size;
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

            throw new KeyNotFoundException($"The given key {key} was not present in the collection");
        }
    }
}
