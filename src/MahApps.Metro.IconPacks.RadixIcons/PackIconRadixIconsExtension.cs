#if (NETFX_CORE || WINDOWS_UWP)
using Windows.UI.Xaml.Markup;
#else
using System;
using System.Windows.Markup;
#endif

namespace MahApps.Metro.IconPacks
{
#if (NETFX_CORE || WINDOWS_UWP)
    [MarkupExtensionReturnType(ReturnType = typeof(PackIconRadixIcons))]
#else
    [MarkupExtensionReturnType(typeof(PackIconRadixIcons))]
#endif
    public class RadixIconsExtension : BasePackIconExtension
    {
        public RadixIconsExtension()
        {
        }

#if !(NETFX_CORE || WINDOWS_UWP)
        public RadixIconsExtension(PackIconRadixIconsKind kind)
        {
            this.Kind = kind;
        }

        [ConstructorArgument("kind")]
#endif
        public PackIconRadixIconsKind Kind { get; set; }

#if (NETFX_CORE || WINDOWS_UWP)
        protected override object ProvideValue()
#else
        public override object ProvideValue(IServiceProvider serviceProvider)
#endif
        {
            return this.GetPackIcon<PackIconRadixIcons, PackIconRadixIconsKind>(this.Kind);
        }
    }
}