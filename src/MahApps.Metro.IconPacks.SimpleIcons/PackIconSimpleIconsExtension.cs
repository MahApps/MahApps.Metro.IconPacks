#if (NETFX_CORE || WINDOWS_UWP)
using Windows.UI.Xaml.Markup;
#else
using System;
using System.Windows.Markup;
#endif

namespace MahApps.Metro.IconPacks
{
#if (NETFX_CORE || WINDOWS_UWP)
    [MarkupExtensionReturnType(ReturnType = typeof(PackIconSimpleIcons))]
#else
    [MarkupExtensionReturnType(typeof(PackIconSimpleIcons))]
#endif
    public class SimpleIconsExtension : BasePackIconExtension
    {
        public SimpleIconsExtension()
        {
        }

#if !(NETFX_CORE || WINDOWS_UWP)
        public SimpleIconsExtension(PackIconSimpleIconsKind kind)
        {
            this.Kind = kind;
        }

        [ConstructorArgument("kind")]
#endif
        public PackIconSimpleIconsKind Kind { get; set; }

#if (NETFX_CORE || WINDOWS_UWP)
        protected override object ProvideValue()
#else
        public override object ProvideValue(IServiceProvider serviceProvider)
#endif
        {
            return this.GetPackIcon<PackIconSimpleIcons, PackIconSimpleIconsKind>(this.Kind);
        }
    }
}