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
    [MarkupExtensionReturnType(ReturnType = typeof(PackIconFontAwesome))]
#else
    [MarkupExtensionReturnType(typeof(PackIconFontAwesome))]
#endif
    public class FontAwesomeExtension : PackIconExtension<PackIconFontAwesome, PackIconFontAwesomeKind>
    {
        public FontAwesomeExtension()
        {
        }

        public FontAwesomeExtension(PackIconFontAwesomeKind kind) : base(kind)
        {
        }
    }
}