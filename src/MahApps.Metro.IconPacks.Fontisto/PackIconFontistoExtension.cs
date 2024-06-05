#if (NETFX_CORE || WINDOWS_UWP)
using Windows.UI.Xaml.Markup;
#else
using System;
using System.Windows.Markup;
#endif

namespace MahApps.Metro.IconPacks
{
#if (NETFX_CORE || WINDOWS_UWP)
    [MarkupExtensionReturnType(ReturnType = typeof(PackIconFontisto))]
#else
    [MarkupExtensionReturnType(typeof(PackIconFontisto))]
#endif
    public class FontistoExtension : BasePackIconExtension
    {
        public FontistoExtension()
        {
        }

#if !(NETFX_CORE || WINDOWS_UWP)
        public FontistoExtension(PackIconFontistoKind kind)
        {
            this.Kind = kind;
        }

        [ConstructorArgument("kind")]
#endif
        public PackIconFontistoKind Kind { get; set; }

#if (NETFX_CORE || WINDOWS_UWP)
        protected override object ProvideValue()
#else
        public override object ProvideValue(IServiceProvider serviceProvider)
#endif
        {
            return this.GetPackIcon<PackIconFontisto, PackIconFontistoKind>(this.Kind);
        }
    }
}