using MahApps.Metro.IconPacks.Browser.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MahApps.Metro.IconPacks.Browser.Model
{
    class NavigationItemTemplateSelector : DataTemplateSelector
    {
        public DataTemplate IconPackTempalte { get; set; }
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is IconPackViewModel)
            {
                return IconPackTempalte;
            }
            else
            {
                return base.SelectTemplate(item, container);
            }
        }
    }
}
