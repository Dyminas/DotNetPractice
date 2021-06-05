using NUnit.Framework;
using System;

namespace DesignPattern.StructuralPatterns.ProxyPattern
{
    [TestFixture]
    public class PrinterTestFixture
    {
        [SetUp]
        public void SetUp()
        {
        }

        [Test]
        public void TestPrinter()
        {
            IPrintable proxy = new PrinterProxy("Alice");
            Console.WriteLine("Current Name: " + proxy.GetPrinterName());
            proxy.SetPrinterName("Bob");
            Console.WriteLine("Current Name: " + proxy.GetPrinterName());
            proxy.Print("Hello World!");
        }
    }
}
