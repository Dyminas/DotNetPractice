using System;
using System.Threading;

namespace DesignPattern.StructuralPatterns.ProxyPattern
{
    public class Printer : IPrintable
    {
        private string _name;

        public Printer(string name)
        {
            _name = name;
            DoSomething($"Creating Printer ({name})...");
        }

        private void DoSomething(string message)
        {
            Console.WriteLine(message);
            Thread.Sleep(3000);
        }

        public void SetPrinterName(string name)
        {
            _name = name;
        }

        public string GetPrinterName()
        {
            return _name;
        }

        public void Print(string content)
        {
            Console.WriteLine($"===== {_name} =====");
            Console.WriteLine(content);
        }
    }
}
