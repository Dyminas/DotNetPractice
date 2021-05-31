using System;

namespace DesignPattern.CreationalPatterns.SingletonPattern
{
    [Serializable]
    public class Singleton
    {
        private static Singleton _instance;
        private static bool _instantiated = false;

        static Singleton()
        {
            _instance = new Singleton();
            _instantiated = true;
        }
        
        private Singleton()
        {
            if (_instantiated)
            {
                throw new NotSupportedException("Do not try to break singleton pattern!");
            }
            Console.WriteLine("Created an instance");
        }

        public static Singleton GetInstance()
        {
            return _instance;
        }
    }
}
