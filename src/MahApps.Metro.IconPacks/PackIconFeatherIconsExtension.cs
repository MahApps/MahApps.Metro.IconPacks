#if (NETFX_CORE || WINDOWS_UWP)
using Windows.UI.Xaml.Markup;
#else
using System;
using System.Windows.Markup;
#endif

namespace MahApps.Metro.IconPacks
{
#if (NETFX_CORE || WINDOWS_UWP)
    [MarkupExtensionReturnType(ReturnType = typeof(PackIconFeatherIcons))]
#else
    [MarkupExtensionReturnType(typeof(PackIconFeatherIcons))]
#endif
    public class FeatherIconsExtension : BasePackIconExtension
    {
        public FeatherIconsExtension()
        {
        }

#if !(NETFX_CORE || WINDOWS_UWP)
        public FeatherIconsExtension(PackIconFeatherIconsKind kind)
        {
            this.Kind = kind;
        }

        [ConstructorArgument("kind")]
#endif
        public PackIconFeatherIconsKind Kind { get; set; }

#if (NETFX_CORE || WINDOWS_UWP)
        protected override object ProvideValue()
#else
        public override object ProvideValue(IServiceProvider serviceProvider)
#endif
        {
            return this.GetPackIcon<PackIconFeatherIcons, PackIconFeatherIconsKind>(this.Kind);
        }
    }
}