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
    [MarkupExtensionReturnType(ReturnType = typeof(PackIconUnicons))]
#else
    [MarkupExtensionReturnType(typeof(PackIconUnicons))]
#endif
    public class UniconsExtension : PackIconExtension<PackIconUnicons, PackIconUniconsKind>
    {
        public UniconsExtension()
        {
        }

        public UniconsExtension(PackIconUniconsKind kind) : base(kind)
        {
        }
    }
}