using System;
#if (NETFX_CORE || WINDOWS_UWP)
using Windows.UI.Xaml;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
#else
using System.Windows.Markup;
using System.Windows.Media.Animation;
#endif

namespace MahApps.Metro.IconPacks
{
#if (NETFX_CORE || WINDOWS_UWP)
    [MarkupExtensionReturnType(ReturnType = typeof(PackIconIonicons))]
#else
    [MarkupExtensionReturnType(typeof(PackIconIonicons))]
#endif
    public class IoniconsExtension : BasePackIconExtension
    {
        public IoniconsExtension()
        {
        }

#if !(NETFX_CORE || WINDOWS_UWP)
        public IoniconsExtension(PackIconIoniconsKind kind)
        {
            this.Kind = kind;
        }

        [ConstructorArgument("kind")]
#endif
        public PackIconIoniconsKind Kind { get; set; }

#if (NETFX_CORE || WINDOWS_UWP)
        protected override object ProvideValue()
#else
        public override object ProvideValue(IServiceProvider serviceProvider)
#endif
        {
            return this.GetPackIcon<PackIconIonicons, PackIconIoniconsKind>(this.Kind);
        }
    }
}