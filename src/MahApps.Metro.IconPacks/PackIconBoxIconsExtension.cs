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
    [MarkupExtensionReturnType(ReturnType = typeof(PackIconBoxIcons))]
#else
    [MarkupExtensionReturnType(typeof(PackIconBoxIcons))]
#endif
    public class BoxIconsExtension : PackIconExtension<PackIconBoxIcons, PackIconBoxIconsKind>
    {
        public BoxIconsExtension()
        {
        }

        public BoxIconsExtension(PackIconBoxIconsKind kind) : base(kind)
        {
        }
    }
}