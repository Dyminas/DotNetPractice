using System;
using System.Collections.Generic;

namespace DesignPattern.StructuralPatterns.CompositePattern
{
    public abstract class Entry
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
    }
}
