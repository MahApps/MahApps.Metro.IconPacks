using System.Reflection;
using System.Runtime.InteropServices;
#if !NETFX_CORE
using System.Windows;
using System.Windows.Markup;

[assembly: ThemeInfo(ResourceDictionaryLocation.None, ResourceDictionaryLocation.SourceAssembly)]
[assembly: XmlnsDefinition("http://metro.mahapps.com/winfx/xaml/iconpacks", "MahApps.Metro.IconPacks")]
#endif

[assembly: ComVisible(false)]
[assembly: AssemblyDescription("IconPacks for stylish awesome WPF and UWP applications.")]
[assembly: AssemblyCopyright("Copyright © MahApps.Metro 2018")]
[assembly: AssemblyCompany("MahApps")]

#if ENTYPO
[assembly: AssemblyProduct("MahApps.Metro.IconPacks.Entypo")]
[assembly: AssemblyTitle("MahApps.Metro.IconPacks.Entypo")]
#elif FONTAWESOME
[assembly: AssemblyProduct("MahApps.Metro.IconPacks.FontAwesome")]
[assembly: AssemblyTitle("MahApps.Metro.IconPacks.FontAwesome")]
#elif MATERIAL
[assembly: AssemblyProduct("MahApps.Metro.IconPacks.Material")]
[assembly: AssemblyTitle("MahApps.Metro.IconPacks.Material")]
#elif MATERIALLIGHT
[assembly: AssemblyProduct("MahApps.Metro.IconPacks.MaterialLight")]
[assembly: AssemblyTitle("MahApps.Metro.IconPacks.MaterialLight")]
#elif MODERN
[assembly: AssemblyProduct("MahApps.Metro.IconPacks.Modern")]
[assembly: AssemblyTitle("MahApps.Metro.IconPacks.Modern")]
#elif OCTICONS
[assembly: AssemblyProduct("MahApps.Metro.IconPacks.Octicons")]
[assembly: AssemblyTitle("MahApps.Metro.IconPacks.Octicons")]
#elif SIMPLEICONS
[assembly: AssemblyProduct("MahApps.Metro.IconPacks.SimpleIcons")]
[assembly: AssemblyTitle("MahApps.Metro.IconPacks.SimpleIcons")]
#else
[assembly: AssemblyProduct("MahApps.Metro.IconPacks")]
[assembly: AssemblyTitle("MahApps.Metro.IconPacks")]
#endif