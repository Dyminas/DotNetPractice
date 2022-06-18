using System;

namespace Algorithms.DataStructure.SymbolTable
{
    public class SeparateChainingHashTable<TKey, TValue> : SymbolTable<TKey, TValue>
        where TKey : IEquatable<TKey>
    {
        private const int DefaultCapacity = 997;
        private readonly int _capacity;
        private readonly SymbolTable<TKey, TValue>[] _buckets;

        public SeparateChainingHashTable() : this(DefaultCapacity) { }

        public SeparateChainingHashTable(int capacity)
        {
            _capacity = capacity;
            _buckets = new SymbolTableBasedOnLinkedList<TKey, TValue>[_capacity];
            for (int i = 0; i < _buckets.Length; i++)
                _buckets[i] = new SymbolTableBasedOnLinkedList<TKey, TValue>();
        }

        private int Hash(TKey key) => (key.GetHashCode() & 0x7FFFFFFF) % _capacity;

        public override bool ContainsKey(TKey key) => _buckets[Hash(key)].ContainsKey(key);

        public override TValue GetValue(TKey key) => _buckets[Hash(key)].GetValue(key);

        public override bool TryGetValue(TKey key, out TValue value) => _buckets[Hash(key)].TryGetValue(key, out value);

        public override void Put(TKey key, TValue value)
        {
            SymbolTable<TKey, TValue> bucket = _buckets[Hash(key)];
            int oldSize = bucket.Count;
            bucket.Put(key, value);
            if (oldSize < bucket.Count) _size++;
        }

        public override void Delete(TKey key)
        {
            _buckets[Hash(key)].Delete(key);
            _size--;
        }
    }
}
