#if (NETFX_CORE || WINDOWS_UWP)
using Windows.UI.Xaml.Markup;
#else
using System;
using System.Windows.Markup;
#endif

namespace MahApps.Metro.IconPacks
{
#if (NETFX_CORE || WINDOWS_UWP)
    [MarkupExtensionReturnType(ReturnType = typeof(PackIconFontaudio))]
#else
    [MarkupExtensionReturnType(typeof(PackIconFontaudio))]
#endif
    public class FontaudioExtension : BasePackIconExtension
    {
        public FontaudioExtension()
        {
        }

#if !(NETFX_CORE || WINDOWS_UWP)
        public FontaudioExtension(PackIconFontaudioKind kind)
        {
            this.Kind = kind;
        }

        [ConstructorArgument("kind")]
#endif
        public PackIconFontaudioKind Kind { get; set; }

#if (NETFX_CORE || WINDOWS_UWP)
        protected override object ProvideValue()
#else
        public override object ProvideValue(IServiceProvider serviceProvider)
#endif
        {
            return this.GetPackIcon<PackIconFontaudio, PackIconFontaudioKind>(this.Kind);
        }
    }
}