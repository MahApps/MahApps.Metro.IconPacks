using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.IconPacks.Browser.Model;
using MahApps.Metro.IconPacks.Browser.Properties;
using MahApps.Metro.IconPacks.Browser.ViewModels;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using io = System.IO;
using System.Windows.Markup;
using System.Windows;

namespace MahApps.Metro.IconPacks.Browser.Controls
{
    /// <summary>
    /// Interaction logic for SideBar.xaml
    /// </summary>
    public partial class SideBar : UserControl
    {
        public SideBar()
        {
            InitializeComponent();
        }

        private void IconPreview_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta != 0 && Keyboard.Modifiers == ModifierKeys.Control)
            {
                Settings.Default.IconPreviewSize += Math.Sign(e.Delta) * 4;
                e.Handled = true;
            }
            else if (Keyboard.Modifiers == ModifierKeys.Shift)
            {
                this.PreviewScrollViewer.ScrollToHorizontalOffset(this.PreviewScrollViewer.HorizontalOffset - e.Delta / 3);
                e.Handled = true;
            }
        }


        
    }
}
