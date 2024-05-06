#if (NETFX_CORE || WINDOWS_UWP)
using Windows.UI.Xaml.Markup;
#else
using System;
using System.Windows.Markup;
#endif

namespace MahApps.Metro.IconPacks
{
#if (NETFX_CORE || WINDOWS_UWP)
    [MarkupExtensionReturnType(ReturnType = typeof(PackIconGameIcons))]
#else
    [MarkupExtensionReturnType(typeof(PackIconGameIcons))]
#endif
    public class GameIconsExtension : BasePackIconExtension
    {
        public GameIconsExtension()
        {
        }

#if !(NETFX_CORE || WINDOWS_UWP)
        public GameIconsExtension(PackIconGameIconsKind kind)
        {
            this.Kind = kind;
        }

        [ConstructorArgument("kind")]
#endif
        public PackIconGameIconsKind Kind { get; set; }

#if (NETFX_CORE || WINDOWS_UWP)
        protected override object ProvideValue()
#else
        public override object ProvideValue(IServiceProvider serviceProvider)
#endif
        {
            return this.GetPackIcon<PackIconGameIcons, PackIconGameIconsKind>(this.Kind);
        }
    }
}