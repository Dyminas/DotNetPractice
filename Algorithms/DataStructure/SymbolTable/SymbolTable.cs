namespace Algorithms.DataStructure.SymbolTable
{
    public abstract class SymbolTable<TKey, TValue>
    {
        protected int _size;

        public int Count => _size;
        public bool IsEmpty => _size == 0;

        public abstract bool ContainsKey(TKey key);

        public abstract TValue GetValue(TKey key);

        public abstract bool TryGetValue(TKey key, out TValue value);

        public abstract void Put(TKey key, TValue value);

        public abstract void Delete(TKey key);
    }
}
