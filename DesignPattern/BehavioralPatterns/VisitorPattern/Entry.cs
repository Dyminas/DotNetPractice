using System;
using System.Collections.Generic;

namespace DesignPattern.BehavioralPatterns.VisitorPattern
{
    public abstract class Entry : IVisitable
    {
        public string Name { get; set; }

        public Entry(string name)
        {
            Name = name;
        }

        public abstract int GetSize();

        public virtual void Add(Entry entry)
        {
            throw new NotImplementedException();
        }

        public virtual ICollection<Entry> GetEntries()
        {
            throw new NotImplementedException();
        }

        public abstract void Accept(IVisitor visitor);
    }
}
