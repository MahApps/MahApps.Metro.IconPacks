#if (NETFX_CORE || WINDOWS_UWP)
using Windows.UI.Xaml.Markup;
#else
using System;
using System.Windows.Markup;
#endif

namespace MahApps.Metro.IconPacks
{
#if (NETFX_CORE || WINDOWS_UWP)
    [MarkupExtensionReturnType(ReturnType = typeof(PackIconCircumIcons))]
#else
    [MarkupExtensionReturnType(typeof(PackIconCircumIcons))]
#endif
    public class CircumIconsExtension : BasePackIconExtension
    {
        public CircumIconsExtension()
        {
        }

#if !(NETFX_CORE || WINDOWS_UWP)
        public CircumIconsExtension(PackIconCircumIconsKind kind)
        {
            this.Kind = kind;
        }

        [ConstructorArgument("kind")]
#endif
        public PackIconCircumIconsKind Kind { get; set; }

#if (NETFX_CORE || WINDOWS_UWP)
        protected override object ProvideValue()
#else
        public override object ProvideValue(IServiceProvider serviceProvider)
#endif
        {
            return this.GetPackIcon<PackIconCircumIcons, PackIconCircumIconsKind>(this.Kind);
        }
    }
}