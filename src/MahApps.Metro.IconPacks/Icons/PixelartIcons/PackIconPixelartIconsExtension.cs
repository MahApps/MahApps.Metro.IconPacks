#if (NETFX_CORE || WINDOWS_UWP)
using Windows.UI.Xaml.Markup;
#else
using System;
using System.Windows.Markup;
#endif

namespace MahApps.Metro.IconPacks
{
#if (NETFX_CORE || WINDOWS_UWP)
    [MarkupExtensionReturnType(ReturnType = typeof(PackIconPixelartIcons))]
#else
    [MarkupExtensionReturnType(typeof(PackIconPixelartIcons))]
#endif
    public class PixelartIconsExtension : BasePackIconExtension
    {
        public PixelartIconsExtension()
        {
        }

#if !(NETFX_CORE || WINDOWS_UWP)
        public PixelartIconsExtension(PackIconPixelartIconsKind kind)
        {
            this.Kind = kind;
        }

        [ConstructorArgument("kind")]
#endif
        public PackIconPixelartIconsKind Kind { get; set; }

#if (NETFX_CORE || WINDOWS_UWP)
        protected override object ProvideValue()
#else
        public override object ProvideValue(IServiceProvider serviceProvider)
#endif
        {
            return this.GetPackIcon<PackIconPixelartIcons, PackIconPixelartIconsKind>(this.Kind);
        }
    }
}