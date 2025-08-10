#if (NETFX_CORE || WINDOWS_UWP)
using Windows.UI.Xaml.Markup;
#else
using System;
using System.Windows.Markup;
#endif

namespace MahApps.Metro.IconPacks
{
#if (NETFX_CORE || WINDOWS_UWP)
    [MarkupExtensionReturnType(ReturnType = typeof(PackIconKeyruneIcons))]
#else
    [MarkupExtensionReturnType(typeof(PackIconKeyruneIcons))]
#endif
    public class KeyruneIconsExtension : BasePackIconExtension
    {
        public KeyruneIconsExtension()
        {
        }

#if !(NETFX_CORE || WINDOWS_UWP)
        public KeyruneIconsExtension(PackIconKeyruneIconsKind kind)
        {
            this.Kind = kind;
        }

        [ConstructorArgument("kind")]
#endif
        public PackIconKeyruneIconsKind Kind { get; set; }

#if (NETFX_CORE || WINDOWS_UWP)
        protected override object ProvideValue()
#else
        public override object ProvideValue(IServiceProvider serviceProvider)
#endif
        {
            return this.GetPackIcon<PackIconKeyruneIcons, PackIconKeyruneIconsKind>(this.Kind);
        }
    }
}