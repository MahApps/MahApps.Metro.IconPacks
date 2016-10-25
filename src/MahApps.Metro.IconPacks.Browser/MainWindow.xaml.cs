using MahApps.Metro.Controls;
using MahApps.Metro.IconPacks.Browser.ViewModels;

namespace MahApps.Metro.IconPacks.Browser
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            this.DataContext = new MainViewModel(this.Dispatcher);
            InitializeComponent();
        }
    }
}