namespace DesignPattern.BehavioralPatterns.VisitorPattern
{
    public interface IVisitable
    {
        void Accept(IVisitor visitor);
    }
}
