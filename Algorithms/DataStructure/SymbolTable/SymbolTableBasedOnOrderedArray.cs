using System;
using System.Collections.Generic;

namespace Algorithms.DataStructure.SymbolTable
{
    public class SymbolTableBasedOnOrderedArray<TKey, TValue> : SymbolTable<TKey, TValue>
        where TKey : IComparable<TKey>
    {
        private const int DefaultCapacity = 4;

        private static readonly TKey[] _emptyKeys = Array.Empty<TKey>();
        private static readonly TValue[] _emptyValues = Array.Empty<TValue>();

        private TKey[] _keys;
        private TValue[] _values;

        public SymbolTableBasedOnOrderedArray() : this(DefaultCapacity) { }

        public SymbolTableBasedOnOrderedArray(int capacity)
        {
            if (capacity < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(capacity), "Capacity cannot be negative!");
            }
            _keys = new TKey[capacity];
            _values = new TValue[capacity];
        }

        private void Grow(int capacity)
        {
            int newcapacity = _keys.Length == 0 ? DefaultCapacity : 2 * _keys.Length;
            Resize(newcapacity < capacity ? capacity : newcapacity);
        }

        private void Resize(int capacity)
        {
            if (capacity < _size)
            {
                throw new ArgumentOutOfRangeException(nameof(capacity), "Capacity cannot be less than element count!");
            }

            if (capacity != _keys.Length)
            {
                if (capacity > 0)
                {
                    TKey[] newKeys = new TKey[capacity];
                    TValue[] newValues = new TValue[capacity];
                    if (_size > 0)
                    {
                        for (int i = 0; i < _size; i++)
                        {
                            newKeys[i] = _keys[i];
                            newValues[i] = _values[i];
                        }
                    }
                    _keys = newKeys;
                    _values = newValues;
                }
                else
                {
                    _keys = _emptyKeys;
                    _values = _emptyValues;
                }
            }
        }

        // Return the number of keys in the symbol table strictly less than the given key
        public int Rank(TKey key)
        {
            if (null == key)
            {
                throw new ArgumentNullException(nameof(key));
            }

            // using binary search
            int l = 0, r = _size - 1;
            int mid, cmp;
            while (l <= r)
            {
                mid = (l + r) / 2;
                cmp = key.CompareTo(_keys[mid]);

                if (cmp > 0)
                {
                    l = mid + 1;
                }
                else if (cmp < 0)
                {
                    r = mid - 1;
                }
                else
                {
                    return mid;
                }
            }

            return l;
        }

        public override TValue GetValue(TKey key)
        {
            if (null == key)
            {
                throw new ArgumentNullException(nameof(key));
            }

            int i = Rank(key);
            if (i < _size && _keys[i].CompareTo(key) == 0)
            {
                return _values[i];
            }

            throw new KeyNotFoundException($"The given key {key} was not present in the collection");
        }

        public override bool TryGetValue(TKey key, out TValue value)
        {
            if (null == key)
            {
                throw new ArgumentNullException(nameof(key));
            }

            int i = Rank(key);
            if (i < _size && _keys[i].CompareTo(key) == 0)
            {
                value = _values[i];
                return true;
            }

            value = default;
            return false;
        }

        public override bool ContainsKey(TKey key)
        {
            if (null == key)
            {
                throw new ArgumentNullException(nameof(key));
            }

            int i = Rank(key);
            if (i < _size && _keys[i].CompareTo(key) == 0)
            {
                return true;
            }
            return false;
        }

        public override void Put(TKey key, TValue value)
        {
            if (null == key)
            {
                throw new ArgumentNullException(nameof(key));
            }

            int i = Rank(key);
            if (i < _size && _keys[i].CompareTo(key) == 0)
            {
                _values[i] = value;
                return;
            }

            if (_size == _keys.Length)
            {
                Grow(_size + 1);
            }

            for (int j = _size; j > i; j--)
            {
                _keys[j] = _keys[j - 1];
                _values[j] = _values[j - 1];
            }
            _keys[i] = key;
            _values[i] = value;
            _size++;
        }

        public override void Delete(TKey key)
        {
            if (null == key)
            {
                throw new ArgumentNullException(nameof(key));
            }

            int i = Rank(key);
            if (i < _size && _keys[i].CompareTo(key) != 0)
            {
                throw new KeyNotFoundException($"The given key {key} was not present in the collection");
            }

            for (int j = i + 1; j < _size; j++)
            {
                _keys[j - 1] = _keys[j];
                _values[j - 1] = _values[j];
            }
            _size--;
            _keys[_size] = default;
            _values[_size] = default;

            if (_size == _keys.Length / 4 && _size > DefaultCapacity)
            {
                Resize(_keys.Length / 2);
            }
        }
    }
}
