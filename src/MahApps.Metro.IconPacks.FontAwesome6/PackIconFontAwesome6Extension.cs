#if (NETFX_CORE || WINDOWS_UWP)
using Windows.UI.Xaml.Markup;
#else
using System;
using System.Windows.Markup;
#endif

namespace MahApps.Metro.IconPacks
{
#if (NETFX_CORE || WINDOWS_UWP)
    [MarkupExtensionReturnType(ReturnType = typeof(PackIconFontAwesome6))]
#else
    [MarkupExtensionReturnType(typeof(PackIconFontAwesome6))]
#endif
    public class FontAwesome6Extension : BasePackIconExtension
    {
        public FontAwesome6Extension()
        {
        }

#if !(NETFX_CORE || WINDOWS_UWP)
        public FontAwesome6Extension(PackIconFontAwesome6Kind kind)
        {
            this.Kind = kind;
        }

        [ConstructorArgument("kind")]
#endif
        public PackIconFontAwesome6Kind Kind { get; set; }

#if (NETFX_CORE || WINDOWS_UWP)
        protected override object ProvideValue()
#else
        public override object ProvideValue(IServiceProvider serviceProvider)
#endif
        {
            return this.GetPackIcon<PackIconFontAwesome6, PackIconFontAwesome6Kind>(this.Kind);
        }
    }
}