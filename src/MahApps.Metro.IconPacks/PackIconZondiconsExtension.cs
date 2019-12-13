#if (NETFX_CORE || WINDOWS_UWP)
using Windows.UI.Xaml.Markup;
#else
using System;
using System.Windows.Markup;
#endif

namespace MahApps.Metro.IconPacks
{
#if (NETFX_CORE || WINDOWS_UWP)
    [MarkupExtensionReturnType(ReturnType = typeof(PackIconZondicons))]
#else
    [MarkupExtensionReturnType(typeof(PackIconZondicons))]
#endif
    public class ZondiconsExtension : BasePackIconExtension
    {
        public ZondiconsExtension()
        {
        }

#if !(NETFX_CORE || WINDOWS_UWP)
        public ZondiconsExtension(PackIconZondiconsKind kind)
        {
            this.Kind = kind;
        }

        [ConstructorArgument("kind")]
#endif
        public PackIconZondiconsKind Kind { get; set; }

#if (NETFX_CORE || WINDOWS_UWP)
        protected override object ProvideValue()
#else
        public override object ProvideValue(IServiceProvider serviceProvider)
#endif
        {
            return this.GetPackIcon<PackIconZondicons, PackIconZondiconsKind>(this.Kind);
        }
    }
}