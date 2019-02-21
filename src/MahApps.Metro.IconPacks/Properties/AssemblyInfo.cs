using System.Reflection;
using System.Runtime.InteropServices;
#if !(NETFX_CORE || WINDOWS_UWP)
using System.Windows;
using System.Windows.Markup;

[assembly: ThemeInfo(ResourceDictionaryLocation.None, ResourceDictionaryLocation.SourceAssembly)]
[assembly: XmlnsDefinition("http://metro.mahapps.com/winfx/xaml/iconpacks", "MahApps.Metro.IconPacks")]
#endif

[assembly: ComVisible(false)]
