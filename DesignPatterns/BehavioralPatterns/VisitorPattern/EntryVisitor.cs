using System;

namespace DesignPattern.BehavioralPatterns.VisitorPattern
{
    public class EntryVisitor : IVisitor
    {
        private string _currentDir = "";

        public void Visit(File file)
        {
            Console.WriteLine("Visiting file: " + _currentDir + '/' + file.Name);
        }

        public void Visit(Directory directory)
        {
            var savedDir = _currentDir;
            _currentDir = _currentDir + '/' + directory.Name;
            Console.WriteLine("Started visiting directory: " + _currentDir);
            foreach (var entry in directory.GetEntries())
            {
                entry.Accept(this);
            }
            Console.WriteLine("Finished visiting directory: " + _currentDir);
            _currentDir = savedDir;
        }
    }
}
