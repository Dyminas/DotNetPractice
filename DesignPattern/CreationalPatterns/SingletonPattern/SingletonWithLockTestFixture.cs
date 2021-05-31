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
    public class SingletonWithLockTestFixture
    {
        [SetUp]
        public void SetUp()
        {

        }

        [Test]
        public void TestSingleton()
        {
            var instance1 = SingletonWithLock.GetInstance();
            var instance2 = SingletonWithLock.GetInstance();
            Assert.AreEqual(instance1, instance2);
        }

        [Test]
        public void TestSingletonWithMultiThreading()
        {
            SingletonWithLock instance1 = null, instance2 = null;
            var thread = new Thread(() => instance1 = SingletonWithLock.GetInstance());
            var task = Task.Run(() => instance2 = SingletonWithLock.GetInstance());
            thread.Start();
            thread.Join();
            task.Wait();
            Assert.IsNotNull(instance1);
            Assert.IsNotNull(instance2);
            Assert.AreEqual(instance1, instance2);
        }

        [Test]
        public void TestSingletonWithReflection()
        {
            var instance1 = SingletonWithLock.GetInstance();
            var type = instance1.GetType();

            Assert.Catch<TargetInvocationException>(() => Activator.CreateInstance(type, true));

            var field = type.GetField("_instantiated", BindingFlags.NonPublic | BindingFlags.Static);
            field.SetValue(field, false);
            var instance2 = Activator.CreateInstance(type, true);
            Assert.AreNotEqual(instance1, instance2);
        }

        [Test]
        public void TestSingletonWithJsonSerialization()
        {
            var instance = SingletonWithLock.GetInstance();
            string jsonString = JsonSerializer.Serialize(instance);

            // Setting constructor to private can prevent json deserialization
            Assert.Catch<NotSupportedException>(() => JsonSerializer.Deserialize<SingletonWithLock>(jsonString));
        }

        [Test]
        public void TestSingletonWithBinarySerialization()
        {
            var instance1 = SingletonWithLock.GetInstance();
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("MyFile.bin", FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, instance1);
            stream.Close();
            stream = new FileStream("MyFile.bin", FileMode.Open, FileAccess.Read, FileShare.Read);
            var instance2 = (SingletonWithLock)formatter.Deserialize(stream);
            stream.Close();
            Assert.AreNotSame(instance1, instance2);
        }
    }
}
