using System.Runtime.InteropServices;
#if !(NETFX_CORE || WINDOWS_UWP)
using System.Windows;
using System.Windows.Markup;

[assembly: ThemeInfo(ResourceDictionaryLocation.None, ResourceDictionaryLocation.SourceAssembly)]
[assembly: XmlnsPrefix(@"http://metro.mahapps.com/winfx/xaml/iconpacks", "iconPacks")]
[assembly: XmlnsDefinition(@"http://metro.mahapps.com/winfx/xaml/iconpacks", "MahApps.Metro.IconPacks")]
[assembly: XmlnsDefinition(@"http://metro.mahapps.com/winfx/xaml/iconpacks", "MahApps.Metro.IconPacks.Converter")]
#endif

[assembly: ComVisible(false)]
