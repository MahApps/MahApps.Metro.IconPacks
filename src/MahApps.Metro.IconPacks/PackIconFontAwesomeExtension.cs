#if (NETFX_CORE || WINDOWS_UWP)
using Windows.UI.Xaml.Markup;
#else
using System;
using System.Windows.Markup;
#endif

namespace MahApps.Metro.IconPacks
{
#if (NETFX_CORE || WINDOWS_UWP)
    [MarkupExtensionReturnType(ReturnType = typeof(PackIconFontAwesome))]
#else
    [MarkupExtensionReturnType(typeof(PackIconFontAwesome))]
#endif
    public class FontAwesomeExtension : BasePackIconExtension
    {
        public FontAwesomeExtension()
        {
        }

#if !(NETFX_CORE || WINDOWS_UWP)
        public FontAwesomeExtension(PackIconFontAwesomeKind kind)
        {
            this.Kind = kind;
        }

        [ConstructorArgument("kind")]
#endif
        public PackIconFontAwesomeKind Kind { get; set; }

#if (NETFX_CORE || WINDOWS_UWP)
        protected override object ProvideValue()
#else
        public override object ProvideValue(IServiceProvider serviceProvider)
#endif
        {
            return this.GetPackIcon<PackIconFontAwesome, PackIconFontAwesomeKind>(this.Kind);
        }
    }
}