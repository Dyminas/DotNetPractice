using System;
using System.Threading;

namespace WpfApp
{
    public static class Global
    {
        public static readonly string WorkingDirectory = Environment.CurrentDirectory;
        public static readonly Mutex Mutex = new(false, @"Global\WpfApp");
        public static readonly string MainWindowTitle = "WpfApp";
    }
}
