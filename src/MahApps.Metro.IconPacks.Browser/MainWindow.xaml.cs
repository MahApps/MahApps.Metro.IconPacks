using MahApps.Metro.Controls;
using MahApps.Metro.IconPacks.Browser.ViewModels;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

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

        private void Find_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            FilterTextBox.Focus();
            Keyboard.Focus(FilterTextBox);
        }
    }
}