using NUnit.Framework;

namespace DesignPattern.BehavioralPatterns.VisitorPattern
{
    [TestFixture]
    public class EntryVisitorTestFixture
    {
        [SetUp]
        public void SetUp()
        {
        }

        [Test]
        public void TestVisitorPattern()
        {
            var rootDir = new Directory("root");
            var binDir = new Directory("bin");
            var tmpDir = new Directory("tmp");
            var usrDir = new Directory("usr");

            rootDir.Add(binDir);
            rootDir.Add(tmpDir);
            rootDir.Add(usrDir);

            binDir.Add(new File("vim", 1000));
            binDir.Add(new File("emacs", 2000));

            var dotNetDir = new Directory("dotNet");
            var javaDir = new Directory("java");
            var pythonDir = new Directory("python");

            usrDir.Add(dotNetDir);
            usrDir.Add(javaDir);
            usrDir.Add(pythonDir);

            dotNetDir.Add(new File("CompositePattern.cs", 3000));
            dotNetDir.Add(new File("ProxyPattern.cs", 4000));
            javaDir.Add(new File("SingletonPattern.java", 4000));

            rootDir.Accept(new EntryVisitor());
        }
    }
}
