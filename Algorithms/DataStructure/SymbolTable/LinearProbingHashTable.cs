using System;
using System.Collections.Generic;

namespace Algorithms.DataStructure.SymbolTable
{
    public class LinearProbingHashTable<TKey, TValue> : SymbolTable<TKey, TValue>
        where TKey : IEquatable<TKey>
    {
        private class Element
        {
            public TKey Key { get; set; }
            public TValue Value { get; set; }

            public Element(TKey key, TValue value)
            {
                Key = key;
                Value = value;
            }
        }

        private const int DefaultCapacity = 16;
        private int _capacity;
        private Element[] _elements;

        public LinearProbingHashTable() : this(DefaultCapacity) { }

        public LinearProbingHashTable(int capacity)
        {
            _capacity = capacity;
            _elements = new Element[capacity];
        }

        private int Hash(TKey key) => (key.GetHashCode() & 0x7FFFFFFF) % _capacity;

        private Element FindElement(TKey key)
        {
            if (key is null)
                throw new ArgumentNullException(nameof(key));

            for (int i = Hash(key); _elements[i] is not null; i = (i + 1) % _capacity)
                if (_elements[i].Key.Equals(key))
                    return _elements[i];
            return null;
        }

        public override bool ContainsKey(TKey key)
        {
            Element element = FindElement(key);
            return element is not null;
        }

        public override TValue GetValue(TKey key)
        {
            Element element = FindElement(key);
            if (element is null)
                throw new KeyNotFoundException($"The given key {key} was not present in the collection");
            else
                return element.Value;
        }

        public override bool TryGetValue(TKey key, out TValue value)
        {
            Element element = FindElement(key);
            if (element is null)
            {
                value = default;
                return false;
            }
            else
            {
                value = element.Value;
                return true;
            }
        }

        private void Resize(int newCapacity)
        {
            if (_capacity == newCapacity) return;

            var oldElements = _elements;
            _elements = new Element[newCapacity];
            _size = 0;
            _capacity = newCapacity;

            foreach (Element ele in oldElements)
                if (ele is not null)
                    Put(ele.Key, ele.Value);
        }

        public override void Put(TKey key, TValue value)
        {
            if (key is null)
                throw new ArgumentNullException(nameof(key));

            if (_size >= _capacity / 2) Resize(_capacity * 2);

            int i;
            for (i = Hash(key); _elements[i] is not null; i = (i + 1) % _capacity)
            {
                if (_elements[i].Key.Equals(key))
                {
                    _elements[i].Value = value;
                    return;
                }
            }

            _elements[i] = new Element(key, value);
            _size++;
        }

        public override void Delete(TKey key)
        {
            if (key is null)
                throw new ArgumentNullException(nameof(key));

            int i = Hash(key);
            while (_elements[i] is not null && false == _elements[i].Key.Equals(key))
                i = (i + 1) % _capacity;

            if (_elements[i] is null)
                throw new KeyNotFoundException($"The given key {key} was not present in the collection");

            _elements[i] = null;
            i = (i + 1) % _capacity;
            while (_elements[i] is not null)
            {
                TKey keyToRedo = _elements[i].Key;
                TValue valueToRedo = _elements[i].Value;
                _elements[i] = null;
                _size--;
                Put(keyToRedo, valueToRedo);
                i = (i + 1) % _capacity;
            }
            _size--;
            if (_size <= _capacity / 8) Resize(_capacity / 2);
        }
    }
}
