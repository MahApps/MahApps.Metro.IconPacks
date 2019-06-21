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
    [MarkupExtensionReturnType(ReturnType = typeof(PackIconJamIcons))]
#else
    [MarkupExtensionReturnType(typeof(PackIconJamIcons))]
#endif
    public class JamIconsExtension : PackIconExtension<PackIconJamIcons, PackIconJamIconsKind>
    {
        public JamIconsExtension()
        {
        }

        public JamIconsExtension(PackIconJamIconsKind kind) : base(kind)
        {
        }
    }
}