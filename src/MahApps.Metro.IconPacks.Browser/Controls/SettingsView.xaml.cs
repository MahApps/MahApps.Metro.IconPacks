using MahApps.Metro.IconPacks.Browser.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MahApps.Metro.IconPacks.Browser.Controls
{
    /// <summary>
    /// Interaction logic for SettingsView.xaml
    /// </summary>
    public partial class SettingsView : UserControl
    {
        public SettingsView()
        {
            InitializeComponent();
        }

        private void AccentColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            SettingsViewModel.SetTheme();
        }

        private void AppThemeChanged(object sender, SelectionChangedEventArgs e)
        {
            SettingsViewModel.SetTheme();
        }
    }
}
