#if (NETFX_CORE || WINDOWS_UWP)
using Windows.UI.Xaml.Markup;
#else
using System;
using System.Windows.Markup;
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