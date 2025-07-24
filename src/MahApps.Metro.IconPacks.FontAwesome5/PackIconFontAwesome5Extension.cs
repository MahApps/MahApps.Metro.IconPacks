#if (NETFX_CORE || WINDOWS_UWP)
using Windows.UI.Xaml.Markup;
#else
using System;
using System.Windows.Markup;
#endif

namespace MahApps.Metro.IconPacks
{
#if (NETFX_CORE || WINDOWS_UWP)
    [MarkupExtensionReturnType(ReturnType = typeof(PackIconFontAwesome5))]
#else
    [MarkupExtensionReturnType(typeof(PackIconFontAwesome5))]
#endif
    public class FontAwesome5Extension : BasePackIconExtension
    {
        public FontAwesome5Extension()
        {
        }

#if !(NETFX_CORE || WINDOWS_UWP)
        public FontAwesome5Extension(PackIconFontAwesome5Kind kind)
        {
            this.Kind = kind;
        }

        [ConstructorArgument("kind")]
#endif
        public PackIconFontAwesome5Kind Kind { get; set; }

#if (NETFX_CORE || WINDOWS_UWP)
        protected override object ProvideValue()
#else
        public override object ProvideValue(IServiceProvider serviceProvider)
#endif
        {
            return this.GetPackIcon<PackIconFontAwesome5, PackIconFontAwesome5Kind>(this.Kind);
        }
    }
}