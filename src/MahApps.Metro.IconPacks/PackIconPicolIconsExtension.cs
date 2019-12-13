#if (NETFX_CORE || WINDOWS_UWP)
using Windows.UI.Xaml.Markup;
#else
using System;
using System.Windows.Markup;
#endif

namespace MahApps.Metro.IconPacks
{
#if (NETFX_CORE || WINDOWS_UWP)
    [MarkupExtensionReturnType(ReturnType = typeof(PackIconPicolIcons))]
#else
    [MarkupExtensionReturnType(typeof(PackIconPicolIcons))]
#endif
    public class PicolIconsExtension : BasePackIconExtension
    {
        public PicolIconsExtension()
        {
        }

#if !(NETFX_CORE || WINDOWS_UWP)
        public PicolIconsExtension(PackIconPicolIconsKind kind)
        {
            this.Kind = kind;
        }

        [ConstructorArgument("kind")]
#endif
        public PackIconPicolIconsKind Kind { get; set; }

#if (NETFX_CORE || WINDOWS_UWP)
        protected override object ProvideValue()
#else
        public override object ProvideValue(IServiceProvider serviceProvider)
#endif
        {
            return this.GetPackIcon<PackIconPicolIcons, PackIconPicolIconsKind>(this.Kind);
        }
    }
}