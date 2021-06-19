using NUnit.Framework;
using System;
using System.Linq;

namespace Algorithms.DataStructure.Heap
{
    public class IndexMaxHeap<T> where T : IComparable
    {
        private const int DefaultCapacity = 4;

        // _indexesOfIndex[_indexes[i]] = _indexes[_indexesOfIndex[i]] = i
        private int[] _indexesOfIndex;
        private int[] _indexes;
        private T[] _items;

        private int _size;

        private static readonly T[] _emptyItems = new T[0];
        private static readonly int[] _emptyIndexes = new int[0];

        public int Count => _size;

        public bool IsEmpty => _size == 0;

        public int Capacity
        {
            get => _items.Length;
            set
            {
                if (value < _items.Length)
                {
                    throw new ArgumentOutOfRangeException("New capacity cannot be less than current one!");
                }

                if (value != _items.Length)
                {
                    if (value > 0)
                    {
                        int[] newindexesOfIndex = Enumerable.Repeat(-1, value).ToArray();
                        int[] newIndexes = Enumerable.Repeat(-1, value).ToArray();
                        T[] newItems = new T[value];
                        if (_size > 0)
                        {
                            Array.Copy(_indexesOfIndex, newindexesOfIndex, _indexesOfIndex.Length);
                            Array.Copy(_indexes, newIndexes, _size);
                            Array.Copy(_items, newItems, _items.Length);
                        }
                        _indexesOfIndex = newindexesOfIndex;
                        _indexes = newIndexes;
                        _items = newItems;
                    }
                    else
                    {
                        _indexesOfIndex = _emptyIndexes;
                        _indexes = _emptyIndexes;
                        _items = _emptyItems;
                    }
                }
            }
        }

        public IndexMaxHeap(int capacity)
        {
            if (capacity < 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            _indexesOfIndex = Enumerable.Repeat(-1, capacity).ToArray();
            _indexes = Enumerable.Repeat(-1, capacity).ToArray();
            _items = new T[capacity];
            _size = 0;
        }

        public void Insert(int i, T item)
        {
            if (i < 0)
            {
                throw new IndexOutOfRangeException();
            }

            if (Contains(i))
            {
                throw new ArgumentException("Cannot insert item with the same key!");
            }

            if (i >= _items.Length)
            {
                Grow(i + 1);
            }

            _indexesOfIndex[i] = _size;
            _indexes[_size] = i;
            _items[i] = item;
            ShiftUp(_size++);
        }

        public bool Contains(int i)
        {
            return i < _indexesOfIndex.Length && _indexesOfIndex[i] != -1;
        }

        private void Grow(int capacity)
        {
            int newcapacity = _items.Length == 0 ? DefaultCapacity : 2 * _items.Length;
            Capacity = newcapacity < capacity ? capacity : newcapacity;
        }

        private void ShiftUp(int k)
        {
            int temp = _indexes[k];
            int parent = (k - 1) / 2;
            while (k > 0 && _items[_indexes[parent]].CompareTo(_items[temp]) < 0)
            {
                _indexes[k] = _indexes[parent];
                _indexesOfIndex[_indexes[k]] = k;
                k = parent;
                parent = (k - 1) / 2;
            }
            _indexes[k] = temp;
            _indexesOfIndex[_indexes[k]] = k;
        }

        public T ExtractMax()
        {
            if (_size < 1)
            {
                throw new InvalidOperationException();
            }

            T result = _items[_indexes[0]];
            _indexesOfIndex[_indexes[0]] = -1;
            _items[_indexes[0]] = default;
            _indexes[0] = _indexes[--_size];
            _indexes[_size] = -1;
            ShiftDown(0);
            return result;
        }

        private void ShiftDown(int index)
        {
            if (_size == 0)
            {
                return;
            }

            int MaxChild,
                leftChild = index * 2 + 1,
                rightChild = leftChild + 1;

            int temp = _indexes[index];

            while (leftChild < _size)
            {
                if (rightChild < _size && _items[_indexes[rightChild]].CompareTo(_items[_indexes[leftChild]]) > 0)
                {
                    MaxChild = rightChild;
                }
                else
                {
                    MaxChild = leftChild;
                }

                if (_items[_indexes[MaxChild]].CompareTo(_items[temp]) <= 0)
                {
                    break;
                }

                _indexes[index] = _indexes[MaxChild];
                _indexesOfIndex[_indexes[index]] = index;
                index = MaxChild;
                leftChild = index * 2 + 1;
                rightChild = leftChild + 1;
            }

            _indexes[index] = temp;
            _indexesOfIndex[_indexes[index]] = index;
        }

        public void Change(int i, T item)
        {
            if (!Contains(i))
            {
                throw new ArgumentException("No item with index: " + i);
            }
            _items[i] = item;
            ShiftUp(i);
            ShiftDown(i);
        }

        public void CheckIndexes()
        {
            for (int i = 0; i < _size; i++)
            {
                Assert.AreEqual(_indexesOfIndex[_indexes[i]], i);
                if (_indexesOfIndex[i] != -1)
                {
                    Assert.AreEqual(_indexes[_indexesOfIndex[i]], i);
                }
            }
        }

        public void CheckMaxHeap()
        {
            int leftChild, rightChild;
            for (int i = 0; i < _size; i++)
            {
                leftChild = i * 2 + 1;
                if (leftChild < _size)
                {
                    Assert.GreaterOrEqual(_items[_indexes[i]], _items[_indexes[leftChild]]);
                }

                rightChild = i * 2 + 2;
                if (rightChild < _size)
                {
                    Assert.GreaterOrEqual(_items[_indexes[i]], _items[_indexes[rightChild]]);
                }
            }
        }
    }
}
