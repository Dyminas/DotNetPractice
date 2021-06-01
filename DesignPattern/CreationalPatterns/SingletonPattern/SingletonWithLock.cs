using System;
using System.Runtime.Serialization;

namespace DesignPattern.CreationalPatterns.SingletonPattern
{
    [Serializable]
    public class SingletonWithLock
    {
        private static SingletonWithLock _instance;
        private static readonly object objLock = new();
        private static bool _enableConstructor = false;

        private SingletonWithLock()
        {
            lock (objLock)
            {
                if (!_enableConstructor)
                {
                    throw new NotSupportedException("Do not try to break singleton pattern!");
                }
                Console.WriteLine("Created an instance");
            }
        }

        public static SingletonWithLock GetInstance()
        {
            if (_instance == null)
            {
                lock (objLock)
                {
                    if (_instance == null)
                    {
                        _enableConstructor = true;
                        _instance = new SingletonWithLock();
                        _enableConstructor = false;
                    }
                }
            }
            return _instance;
        }

        [OnDeserializing]
        internal void OnDeserializing(StreamingContext context)
        {
            throw new NotSupportedException("Do not try to break singleton pattern!");
        }
    }
}
