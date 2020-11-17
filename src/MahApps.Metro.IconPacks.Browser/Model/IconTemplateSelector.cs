using MahApps.Metro.IconPacks.Browser.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Markup;

namespace MahApps.Metro.IconPacks.Browser.Model
{
    public class IconTemplateSelector : DataTemplateSelector
    {
        const string DataTemplate_Template = @"
<DataTemplate xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation""
              xmlns:x=""http://schemas.microsoft.com/winfx/2006/xaml"" 
              xmlns:iconPacks=""http://metro.mahapps.com/winfx/xaml/iconpacks""
              xmlns:converter=""clr-namespace:MahApps.Metro.IconPacks.Converter;assembly=MahApps.Metro.IconPacks.Core"">
    <iconPacks:[[IconPackType]] Width = ""Auto""
                                Height = ""Auto""
                                HorizontalAlignment = ""Center""
                                VerticalAlignment = ""Center""
                                Kind = ""{Binding Value, Mode=OneWay, Converter={converter:NullToUnsetValueConverter}}""
                                SnapsToDevicePixels = ""True"" />
</DataTemplate>";

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is IconViewModel iconViewModel)
            {
                var template = XamlReader.Parse(DataTemplate_Template.Replace("[[IconPackType]]", iconViewModel.IconPackType.Name)) as DataTemplate;
                return template;
            }
            return null;
        }
    }
}
