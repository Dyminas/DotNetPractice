using System;
using System.Collections.Generic;

namespace Algorithms.DataStructure.SymbolTable
{
    public class SymbolTableBasedOnLinkedList<TKey, TValue> : SymbolTable<TKey, TValue>
        where TKey : IEquatable<TKey>
    {
        private class Node
        {
            public TKey Key { get; set; }
            public TValue Value { get; set; }
            public Node Previous { get; set; }
            public Node Next { get; set; }

            public Node(TKey key, TValue value, Node next)
            {
                Key = key;
                Value = value;
                Next = next;
                if (null != next)
                {
                    next.Previous = this;
                }
            }
        }

        private Node _head;

        private Node FindNode(TKey key)
        {
            for (Node n = _head; n != null; n = n.Next)
            {
                if (key.Equals(n.Key))
                    return n;
            }
            return null;
        }

        public override bool ContainsKey(TKey key)
        {
            if (null == key)
            {
                throw new ArgumentNullException(nameof(key));
            }
            return null != FindNode(key);
        }

        public override TValue GetValue(TKey key)
        {
            if (null == key)
            {
                throw new ArgumentNullException(nameof(key));
            }
            Node node = FindNode(key);
            if (null == node)
            {
                throw new KeyNotFoundException($"The given key {key} was not present in the collection");
            }
            else
            {
                return node.Value;
            }
        }

        public override bool TryGetValue(TKey key, out TValue value)
        {
            if (null == key)
            {
                throw new ArgumentNullException(nameof(key));
            }
            Node node = FindNode(key);
            if (null != node)
            {
                value = node.Value;
                return true;
            }
            value = default;
            return false;
        }

        public override void Put(TKey key, TValue value)
        {
            if (null == key)
            {
                throw new ArgumentNullException(nameof(key));
            }
            Node node = FindNode(key);
            if (null == node)
            {
                _head = new Node(key, value, _head);
                _size++;
            }
            else
            {
                node.Value = value;
            }
        }

        public override void Delete(TKey key)
        {
            if (null == key)
            {
                throw new ArgumentNullException(nameof(key));
            }
            Node node = FindNode(key);

            if (null == node)
            {
                throw new KeyNotFoundException($"The given key {key} was not present in the collection");
            }
            else
            {
                DeleteNode(node);
            }
        }

        private void DeleteNode(Node node)
        {
            Node previous = node.Previous;
            Node next = node.Next;

            if (null != next)
            {
                next.Previous = previous;
            }

            if (null == previous)
            {
                _head = next;
            }
            else
            {
                previous.Next = next;
            }

            _size--;
        }
    }
}
