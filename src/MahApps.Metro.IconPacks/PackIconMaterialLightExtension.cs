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
    [MarkupExtensionReturnType(ReturnType = typeof(PackIconMaterialLight))]
#else
    [MarkupExtensionReturnType(typeof(PackIconMaterialLight))]
#endif
    public class MaterialLightExtension : BasePackIconExtension
    {
        public MaterialLightExtension()
        {
        }

#if !(NETFX_CORE || WINDOWS_UWP)
        public MaterialLightExtension(PackIconMaterialLightKind kind)
        {
            this.Kind = kind;
        }

        [ConstructorArgument("kind")]
#endif
        public PackIconMaterialLightKind Kind { get; set; }

#if (NETFX_CORE || WINDOWS_UWP)
        protected override object ProvideValue()
#else
        public override object ProvideValue(IServiceProvider serviceProvider)
#endif
        {
            return this.GetPackIcon<PackIconMaterialLight, PackIconMaterialLightKind>(this.Kind);
        }
    }
}