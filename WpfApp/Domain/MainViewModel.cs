using System.Collections.Generic;
using System.Collections.ObjectModel;
using WpfApp.Views;

namespace WpfApp.Domain
{
    public class MainViewModel : ViewModelBase
    {
        public ObservableCollection<MainViewItem> ViewItems { get; private set; }

        public MainViewModel()
        {
            ViewItems = new ObservableCollection<MainViewItem>()
            {
                new MainViewItem("Home", typeof(HomeView))
            };

            foreach (var item in GenerateViewItems())
            {
                ViewItems.Add(item);
            }
        }

        private static IEnumerable<MainViewItem> GenerateViewItems()
        {
            yield return new MainViewItem("External Program Caller", typeof(ExternalProgramCaller));
            yield return new MainViewItem("To Do 1", typeof(string));
            yield return new MainViewItem("To Do 2", typeof(string));
            yield return new MainViewItem("To Do 3", typeof(string));
        }
    }
}
