using MahApps.Metro.Controls;
using MahApps.Metro.IconPacks.Browser.ViewModels;
using System.Windows.Input;
using System.Windows.Media;

namespace MahApps.Metro.IconPacks.Browser
{
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            var mainViewModel = new MainViewModel(this.Dispatcher);
            var resource = TryFindResource("AccentColorBrush");

            if (resource is Brush brush)
                mainViewModel.SelectedBrush = brush;

            this.DataContext = mainViewModel;


            ToggleImageGeneratorFlyoutCommand = new RelayCommand(() => ToggleFlyout(0));

            InitializeComponent();
        }

        private void ToggleFlyout(int index)
        {
            var flyout = Flyouts.Items[index] as Flyout;

            flyout.IsOpen = !flyout.IsOpen;
        }

        public ICommand ToggleImageGeneratorFlyoutCommand { get; private set; }
    }
}
