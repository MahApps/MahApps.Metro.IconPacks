using System;
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
    public class MaterialDesignExtension : BasePackIconExtension
    {
        public MaterialDesignExtension()
        {
        }

#if !(NETFX_CORE || WINDOWS_UWP)
        public MaterialDesignExtension(PackIconMaterialDesignKind kind)
        {
            this.Kind = kind;
        }

        [ConstructorArgument("kind")]
#endif
        public PackIconMaterialDesignKind Kind { get; set; }

#if (NETFX_CORE || WINDOWS_UWP)
        protected override object ProvideValue()
#else
        public override object ProvideValue(IServiceProvider serviceProvider)
#endif
        {
            return this.GetPackIcon<PackIconMaterialDesign, PackIconMaterialDesignKind>(this.Kind);
        }
    }
}