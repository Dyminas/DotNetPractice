using NUnit.Framework;
using System;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace DesignPattern.CreationalPatterns.SingletonPattern
{
    [TestFixture]
    public class SingletonTestFixture
    {
        [SetUp]
        public void SetUp()
        {

        }

        [Test]
        public void TestSingleton()
        {
            var instance1 = Singleton.GetInstance();
            var instance2 = Singleton.GetInstance();
            Assert.AreSame(instance1, instance2);
        }

        [Test]
        public void TestSingletonWithMultiThreading()
        {
            Singleton instance1 = null, instance2 = null;
            var thread = new Thread(() => instance1 = Singleton.GetInstance());
            var task = Task.Run(() => instance2 = Singleton.GetInstance());
            thread.Start();
            thread.Join();
            task.Wait();
            Assert.IsNotNull(instance1);
            Assert.IsNotNull(instance2);
            Assert.AreSame(instance1, instance2);
        }

        [Test]
        public void TestSingletonWithReflection()
        {
            var type = typeof(Singleton);

            Singleton instance = null;
            Assert.Catch<TargetInvocationException>(() => instance = Activator.CreateInstance(type, true) as Singleton);

            var instance1 = Singleton.GetInstance();
            var field = type.GetField("_instantiated", BindingFlags.NonPublic | BindingFlags.Static);
            field.SetValue(field, false);
            var instance2 = Activator.CreateInstance(type, true) as Singleton;
            Assert.AreNotSame(instance1, instance2);
        }

        [Test]
        public void TestSingletonWithJsonSerialization()
        {
            var instance = Singleton.GetInstance();
            string jsonString = JsonSerializer.Serialize(instance);

            // Setting constructor to private can prevent json deserialization
            Assert.Catch<NotSupportedException>(() => JsonSerializer.Deserialize<Singleton>(jsonString));
        }

        [Test]
        public void TestSingletonWithBinarySerialization()
        {
            Singleton instance1 = Singleton.GetInstance();
            Singleton instance2 = null;
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("MyFile.bin", FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, instance1);
            stream.Close();
            stream = new FileStream("MyFile.bin", FileMode.Open, FileAccess.Read, FileShare.Read);
            Assert.Catch<SerializationException>(() => instance2 = (Singleton)formatter.Deserialize(stream));
            stream.Close();
        }
    }
}
