#if (NETFX_CORE || WINDOWS_UWP)
using Windows.UI.Xaml.Markup;
#else
using System;
using System.Windows.Markup;
#endif

namespace MahApps.Metro.IconPacks
{
#if (NETFX_CORE || WINDOWS_UWP)
    [MarkupExtensionReturnType(ReturnType = typeof(PackIconBootstrapIcons))]
#else
    [MarkupExtensionReturnType(typeof(PackIconBootstrapIcons))]
#endif
    public class BootstrapIconsExtension : BasePackIconExtension
    {
        public BootstrapIconsExtension()
        {
        }

#if !(NETFX_CORE || WINDOWS_UWP)
        public BootstrapIconsExtension(PackIconBootstrapIconsKind kind)
        {
            this.Kind = kind;
        }

        [ConstructorArgument("kind")]
#endif
        public PackIconBootstrapIconsKind Kind { get; set; }

#if (NETFX_CORE || WINDOWS_UWP)
        protected override object ProvideValue()
#else
        public override object ProvideValue(IServiceProvider serviceProvider)
#endif
        {
            return this.GetPackIcon<PackIconBootstrapIcons, PackIconBootstrapIconsKind>(this.Kind);
        }
    }
}