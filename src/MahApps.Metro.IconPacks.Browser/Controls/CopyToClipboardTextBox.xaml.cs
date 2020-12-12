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
    /// Interaction logic for CopyToClipboardTextBox.xaml
    /// </summary>
    public partial class CopyToClipboardTextBox : UserControl
    {
        public CopyToClipboardTextBox()
        {
            InitializeComponent();
        }


        public string TextToCopy
        {
            get { return (string)GetValue(TextToCopyProperty); }
            set { SetValue(TextToCopyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TextToCopy.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextToCopyProperty =
            DependencyProperty.Register("TextToCopy", typeof(string), typeof(CopyToClipboardTextBox), new PropertyMetadata(null));


    }
}
