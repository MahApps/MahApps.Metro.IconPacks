#if (NETFX_CORE || WINDOWS_UWP)
using Windows.UI.Xaml.Markup;
#else
using System;
using System.Windows.Markup;
#endif

namespace MahApps.Metro.IconPacks
{
#if (NETFX_CORE || WINDOWS_UWP)
    [MarkupExtensionReturnType(ReturnType = typeof(PackIconPhosphorIcons))]
#else
    [MarkupExtensionReturnType(typeof(PackIconPhosphorIcons))]
#endif
    public class PhosphorIconsExtension : BasePackIconExtension
    {
        public PhosphorIconsExtension()
        {
        }

#if !(NETFX_CORE || WINDOWS_UWP)
        public PhosphorIconsExtension(PackIconPhosphorIconsKind kind)
        {
            this.Kind = kind;
        }

        [ConstructorArgument("kind")]
#endif
        public PackIconPhosphorIconsKind Kind { get; set; }

#if (NETFX_CORE || WINDOWS_UWP)
        protected override object ProvideValue()
#else
        public override object ProvideValue(IServiceProvider serviceProvider)
#endif
        {
            return this.GetPackIcon<PackIconPhosphorIcons, PackIconPhosphorIconsKind>(this.Kind);
        }
    }
}