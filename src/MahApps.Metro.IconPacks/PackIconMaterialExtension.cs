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
    [MarkupExtensionReturnType(ReturnType = typeof(PackIconMaterial))]
#else
    [MarkupExtensionReturnType(typeof(PackIconMaterial))]
#endif
    public class MaterialExtension : BasePackIconExtension
    {
        public MaterialExtension()
        {
        }

#if !(NETFX_CORE || WINDOWS_UWP)
        public MaterialExtension(PackIconMaterialKind kind)
        {
            this.Kind = kind;
        }

        [ConstructorArgument("kind")]
#endif
        public PackIconMaterialKind Kind { get; set; }

#if (NETFX_CORE || WINDOWS_UWP)
        protected override object ProvideValue()
#else
        public override object ProvideValue(IServiceProvider serviceProvider)
#endif
        {
            return this.GetPackIcon<PackIconMaterial, PackIconMaterialKind>(this.Kind);
        }
    }
}