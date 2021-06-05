using System.Collections.Generic;
using System.Linq;

namespace DesignPattern.StructuralPatterns.CompositePattern
{
    public class Directory : Entry
    {
        private readonly ICollection<Entry> _entries = new List<Entry>();

        public Directory(string name) : base(name)
        {
        }

        public override int GetSize()
        {
            return _entries.Sum(e => e.GetSize());
        }

        public override void Add(Entry entry)
        {
            _entries.Add(entry);
        }

        public override ICollection<Entry> GetEntries()
        {
            return _entries;
        }
    }
}
