namespace DesignPattern.StructuralPatterns.ProxyPattern
{
    public class PrinterProxy : IPrintable
    {
        private string _name;
        private Printer _real;

        public PrinterProxy(string name)
        {
            _name = name;
        }

        public void SetPrinterName(string name)
        {
            _real?.SetPrinterName(name);
            _name = name;
        }

        public string GetPrinterName()
        {
            return _name;
        }

        public void Print(string content)
        {
            if (_real == null)
            {
                _real = new Printer(_name);
            }
            _real.Print(content);
        }
    }
}
