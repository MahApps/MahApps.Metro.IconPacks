#if (NETFX_CORE || WINDOWS_UWP)
using Windows.UI.Xaml.Markup;
#else
using System;
using System.Windows.Markup;
#endif

namespace MahApps.Metro.IconPacks
{
#if (NETFX_CORE || WINDOWS_UWP)
    [MarkupExtensionReturnType(ReturnType = typeof(PackIconMingCuteIcons))]
#else
    [MarkupExtensionReturnType(typeof(PackIconMingCuteIcons))]
#endif
    public class MingCuteIconsExtension : BasePackIconExtension
    {
        public MingCuteIconsExtension()
        {
        }

#if !(NETFX_CORE || WINDOWS_UWP)
        public MingCuteIconsExtension(PackIconMingCuteIconsKind kind)
        {
            this.Kind = kind;
        }

        [ConstructorArgument("kind")]
#endif
        public PackIconMingCuteIconsKind Kind { get; set; }

#if (NETFX_CORE || WINDOWS_UWP)
        protected override object ProvideValue()
#else
        public override object ProvideValue(IServiceProvider serviceProvider)
#endif
        {
            return this.GetPackIcon<PackIconMingCuteIcons, PackIconMingCuteIconsKind>(this.Kind);
        }
    }
}