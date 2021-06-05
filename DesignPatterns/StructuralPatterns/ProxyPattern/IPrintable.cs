namespace DesignPattern.StructuralPatterns.ProxyPattern
{
    public interface IPrintable
    {
        void SetPrinterName(string name);
        string GetPrinterName();
        void Print(string content);
    }
}
