using NUnit.Framework;
using System;

namespace Algorithms.DataStructure.Heap
{
    public class MaxHeap<T> where T : IComparable
    {
        private const int DefaultCapacity = 4;

        private T[] _items;
        private int _size;

        private static readonly T[] _emptyArray = Array.Empty<T>();

        public int Count => _size;

        public bool IsEmpty => _size == 0;

        public int Capacity
        {
            get => _items.Length;
            set
            {
                if (value < _size)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "New capacity cannot be less than current one!");
                }

                if (value != _items.Length)
                {
                    if (value > 0)
                    {
                        T[] newItems = new T[value];
                        if (_size > 0)
                        {
                            Array.Copy(_items, newItems, _size);
                        }
                        _items = newItems;
                    }
                    else
                    {
                        _items = _emptyArray;
                    }
                }
            }
        }

        public MaxHeap(int capacity)
        {
            if (capacity < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(capacity), "Capacity cannot be negative!");
            }
            _items = new T[capacity];
            _size = 0;
        }

        public void Insert(T item)
        {
            if (_size == _items.Length)
            {
                Grow(_size + 1);
            }

            _items[_size] = item;
            ShiftUp(_size++);
        }

        private void Grow(int capacity)
        {
            int newcapacity = _items.Length == 0 ? DefaultCapacity : 2 * _items.Length;
            Capacity = newcapacity < capacity ? capacity : newcapacity;
        }

        private void ShiftUp(int index)
        {
            T temp = _items[index];
            int parent = (index - 1) / 2;
            while (index > 0 && _items[parent].CompareTo(temp) < 0)
            {
                _items[index] = _items[parent];
                index = parent;
                parent = (index - 1) / 2;
            }
            _items[index] = temp;
        }

        public T ExtractMax()
        {
            if (_size < 1)
            {
                throw new InvalidOperationException();
            }
            T result = _items[0];
            _items[0] = _items[--_size];
            _items[_size] = default;
            ShiftDown(_items, _size, 0);
            return result;
        }

        public static void ShiftDown(T[] array, int length, int index)
        {
            int MaxChild,
                leftChild = index * 2 + 1,
                rightChild = leftChild + 1;

            T temp = array[index];

            while (leftChild < length)
            {
                if (rightChild < length && array[rightChild].CompareTo(array[leftChild]) > 0)
                {
                    MaxChild = rightChild;
                }
                else
                {
                    MaxChild = leftChild;
                }

                if (array[MaxChild].CompareTo(temp) <= 0)
                {
                    break;
                }

                array[index] = array[MaxChild];
                index = MaxChild;
                leftChild = index * 2 + 1;
                rightChild = leftChild + 1;
            }

            array[index] = temp;
        }

        public void CheckMaxHeap()
        {
            int leftChild, rightChild;
            for (int i = 0; i < _size; i++)
            {
                leftChild = i * 2 + 1;
                if (leftChild < _size)
                {
                    Assert.GreaterOrEqual(_items[i], _items[leftChild]);
                }

                rightChild = i * 2 + 2;
                if (rightChild < _size)
                {
                    Assert.GreaterOrEqual(_items[i], _items[rightChild]);
                }
            }
        }
    }
}
