using System;

namespace DesignPattern.CreationalPatterns.SingletonPattern
{
    [Serializable]
    public class SingletonWithLock
    {
        private static SingletonWithLock _instance;
        private static bool _enableConstructor = false;

        private SingletonWithLock()
        {
            if (!_enableConstructor)
            {
                throw new NotSupportedException("Do not try to break singleton pattern!");
            }
            Console.WriteLine("Created an instance");
        }

        public static SingletonWithLock GetInstance()
        {
            if (_instance == null)
            {
                lock (typeof(Singleton))
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
    }
}
