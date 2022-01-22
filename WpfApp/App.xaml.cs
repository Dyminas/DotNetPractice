using System.Windows;
using WpfApp.Utils;

namespace WpfApp
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
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
