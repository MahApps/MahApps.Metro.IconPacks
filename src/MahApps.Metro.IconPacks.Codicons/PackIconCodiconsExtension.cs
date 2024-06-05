#if (NETFX_CORE || WINDOWS_UWP)
using Windows.UI.Xaml.Markup;
#else
using System;
using System.Windows.Markup;
#endif

namespace MahApps.Metro.IconPacks
{
#if (NETFX_CORE || WINDOWS_UWP)
    [MarkupExtensionReturnType(ReturnType = typeof(PackIconCodicons))]
#else
    [MarkupExtensionReturnType(typeof(PackIconCodicons))]
#endif
    public class CodiconsExtension : BasePackIconExtension
    {
        public CodiconsExtension()
        {
        }

#if !(NETFX_CORE || WINDOWS_UWP)
        public CodiconsExtension(PackIconCodiconsKind kind)
        {
            this.Kind = kind;
        }

        [ConstructorArgument("kind")]
#endif
        public PackIconCodiconsKind Kind { get; set; }

#if (NETFX_CORE || WINDOWS_UWP)
        protected override object ProvideValue()
#else
        public override object ProvideValue(IServiceProvider serviceProvider)
#endif
        {
            return this.GetPackIcon<PackIconCodicons, PackIconCodiconsKind>(this.Kind);
        }
    }
}