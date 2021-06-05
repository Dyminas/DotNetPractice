namespace DesignPattern.BehavioralPatterns.VisitorPattern
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

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
