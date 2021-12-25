using System.Windows;
using WpfApp.Utils;

namespace WpfApp
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            if (!Global.Mutex.WaitOne(0, false))
            {
                MessageBox.Show($"Application is already running: {Global.WorkingDirectory}");
                Shutdown(1);
            }

            Logger.Info($"Application started with arguments: '{string.Join(' ', e.Args)}'");
            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            Logger.Info($"Application stopped");
        }
    }
}
