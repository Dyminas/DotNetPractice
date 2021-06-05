namespace DesignPattern.BehavioralPatterns.VisitorPattern
{
    public interface IVisitor
    {
        void Visit(File file);
        void Visit(Directory directory);
    }
}
