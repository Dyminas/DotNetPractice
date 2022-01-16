using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using WpfApp.Views;

namespace WpfApp.Domain
{
    public class MainViewModel : ViewModelBase
    {
        public ObservableCollection<MainViewItem> ViewItems { get; private set; }

        private MainViewItem _selectedItem;
        public MainViewItem SelectedItem
        {
            get => _selectedItem;
            set => SetProperty(ref _selectedItem, value);
        }

        public MainViewModel()
        {
            ViewItems = new ObservableCollection<MainViewItem>()
            {
                new MainViewItem("Home", typeof(HomeView))
            };
            SelectedItem = ViewItems.First();

            foreach (var item in GenerateViewItems())
            {
                ViewItems.Add(item);
            }
        }

        private static IEnumerable<MainViewItem> GenerateViewItems()
        {
            yield return new MainViewItem("External Caller", typeof(ExternalCaller));
            yield return new MainViewItem("To Do 1", typeof(UserControl));
            yield return new MainViewItem("To Do 2", typeof(UserControl));
            yield return new MainViewItem("To Do 3", typeof(UserControl));
        }
    }
}
