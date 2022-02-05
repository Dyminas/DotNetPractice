using System;
using System.Runtime.InteropServices;

namespace WpfApp
{
    internal class EntryPoint
    {
        [DllImport("user32", CharSet = CharSet.Unicode)]
        private static extern IntPtr FindWindow(string? cls, string win);

        [DllImport("user32")]
        private static extern IntPtr SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32")]
        private static extern bool IsIconic(IntPtr hWnd);

        [DllImport("user32")]
        private static extern bool OpenIcon(IntPtr hWnd);

        [STAThread]
        public static void Main()
        {
            if (!Global.Mutex.WaitOne(0, false))
            {
                var existing = FindWindow(null, Global.MainWindowTitle);
                if (IntPtr.Zero != existing)
                {
                    SetForegroundWindow(existing);
                    if (IsIconic(existing))
                    {
                        OpenIcon(existing);
                    }
                }
                Environment.Exit(1);
            }

            App app = new();
            app.InitializeComponent();
            app.Run();
        }
    }
}
