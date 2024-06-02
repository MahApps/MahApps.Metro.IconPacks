#if (NETFX_CORE || WINDOWS_UWP)
using Windows.UI.Xaml.Markup;
#else
using System;
using System.Windows.Markup;
#endif

namespace MahApps.Metro.IconPacks
{
#if (NETFX_CORE || WINDOWS_UWP)
    [MarkupExtensionReturnType(ReturnType = typeof(PackIconVaadinIcons))]
#else
    [MarkupExtensionReturnType(typeof(PackIconVaadinIcons))]
#endif
    public class VaadinIconsExtension : BasePackIconExtension
    {
        public VaadinIconsExtension()
        {
        }

#if !(NETFX_CORE || WINDOWS_UWP)
        public VaadinIconsExtension(PackIconVaadinIconsKind kind)
        {
            this.Kind = kind;
        }

        [ConstructorArgument("kind")]
#endif
        public PackIconVaadinIconsKind Kind { get; set; }

#if (NETFX_CORE || WINDOWS_UWP)
        protected override object ProvideValue()
#else
        public override object ProvideValue(IServiceProvider serviceProvider)
#endif
        {
            return this.GetPackIcon<PackIconVaadinIcons, PackIconVaadinIconsKind>(this.Kind);
        }
    }
}