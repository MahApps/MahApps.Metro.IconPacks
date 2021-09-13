using MahApps.Metro.Controls;
using MahApps.Metro.IconPacks.Browser.Properties;
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

            // Let's check if the previewsize is valid
            Settings.Default.PropertyChanged += Settings_PropertyChanged;

            InitializeComponent();
        }

        private void Settings_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(Settings.Default.IconPreviewSize):
                    if (Settings.Default.IconPreviewSize < 4) Settings.Default.IconPreviewSize = 4; 
                    break;
            }
        }

        private void Find_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            FilterTextBox.Focus();
            Keyboard.Focus(FilterTextBox);
        }
    }
}