#if (NETFX_CORE || WINDOWS_UWP)
using Windows.UI.Xaml;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
#else
using System.Windows.Markup;
using System.Windows.Media.Animation;
#endif

namespace MahApps.Metro.IconPacks
{
#if (NETFX_CORE || WINDOWS_UWP)
    [MarkupExtensionReturnType(ReturnType = typeof(PackIconSimpleIcons))]
#else
    [MarkupExtensionReturnType(typeof(PackIconSimpleIcons))]
#endif
    public class SimpleIconsExtension : PackIconExtension<PackIconSimpleIcons, PackIconSimpleIconsKind>
    {
        public SimpleIconsExtension()
        {
        }

        public SimpleIconsExtension(PackIconSimpleIconsKind kind) : base(kind)
        {
        }
    }
}