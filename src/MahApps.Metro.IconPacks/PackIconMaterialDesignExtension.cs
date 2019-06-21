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
    [MarkupExtensionReturnType(ReturnType = typeof(PackIconMaterialDesign))]
#else
    [MarkupExtensionReturnType(typeof(PackIconMaterialDesign))]
#endif
    public class MaterialDesignExtension : PackIconExtension<PackIconMaterialDesign, PackIconMaterialDesignKind>
    {
        public MaterialDesignExtension()
        {
        }

        public MaterialDesignExtension(PackIconMaterialDesignKind kind) : base(kind)
        {
        }
    }
}