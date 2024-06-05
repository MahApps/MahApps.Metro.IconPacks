#if (NETFX_CORE || WINDOWS_UWP)
using Windows.UI.Xaml.Markup;
#else
using System;
using System.Windows.Markup;
#endif

namespace MahApps.Metro.IconPacks
{
#if (NETFX_CORE || WINDOWS_UWP)
    [MarkupExtensionReturnType(ReturnType = typeof(PackIconBoxIcons))]
#else
    [MarkupExtensionReturnType(typeof(PackIconBoxIcons))]
#endif
    public class BoxIconsExtension : BasePackIconExtension
    {
        public BoxIconsExtension()
        {
        }

#if !(NETFX_CORE || WINDOWS_UWP)
        public BoxIconsExtension(PackIconBoxIconsKind kind)
        {
            this.Kind = kind;
        }

        [ConstructorArgument("kind")]
#endif
        public PackIconBoxIconsKind Kind { get; set; }

#if (NETFX_CORE || WINDOWS_UWP)
        protected override object ProvideValue()
#else
        public override object ProvideValue(IServiceProvider serviceProvider)
#endif
        {
            return this.GetPackIcon<PackIconBoxIcons, PackIconBoxIconsKind>(this.Kind);
        }
    }
}