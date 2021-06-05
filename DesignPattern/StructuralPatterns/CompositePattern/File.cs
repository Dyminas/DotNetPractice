namespace DesignPattern.StructuralPatterns.CompositePattern
{
    public class File : Entry
    {
        private int _size;

        public File(string name, int size) : base(name)
        {
            _size = size;
        }

        public override int GetSize()
        {
            return _size;
        }
    }
}
