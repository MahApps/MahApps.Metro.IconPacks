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
    [MarkupExtensionReturnType(ReturnType = typeof(PackIconFeatherIcons))]
#else
    [MarkupExtensionReturnType(typeof(PackIconFeatherIcons))]
#endif
    public class FeatherIconsExtension : PackIconExtension<PackIconFeatherIcons, PackIconFeatherIconsKind>
    {
        public FeatherIconsExtension()
        {
        }

        public FeatherIconsExtension(PackIconFeatherIconsKind kind) : base(kind)
        {
        }
    }
}