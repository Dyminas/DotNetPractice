using System;

namespace DesignPattern.CreationalPatterns.SingletonPattern
{
    public class SingletonWithLock
    {
        private static SingletonWithLock _instance;
        private static bool _instantiated = false;

        private SingletonWithLock()
        {
            if (_instantiated)
            {
                throw new NotSupportedException();
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
                        _instance = new SingletonWithLock();
                        _instantiated = true;
                    }
                }
            }
            return _instance;
        }
    }
}
