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
    [MarkupExtensionReturnType(ReturnType = typeof(PackIconRPGAwesome))]
#else
    [MarkupExtensionReturnType(typeof(PackIconRPGAwesome))]
#endif
    public class RPGAwesomeExtension : BasePackIconExtension
    {
        public RPGAwesomeExtension()
        {
        }

#if !(NETFX_CORE || WINDOWS_UWP)
        public RPGAwesomeExtension(PackIconRPGAwesomeKind kind)
        {
            this.Kind = kind;
        }

        [ConstructorArgument("kind")]
#endif
        public PackIconRPGAwesomeKind Kind { get; set; }

#if (NETFX_CORE || WINDOWS_UWP)
        protected override object ProvideValue()
#else
        public override object ProvideValue(IServiceProvider serviceProvider)
#endif
        {
            return this.GetPackIcon<PackIconRPGAwesome, PackIconRPGAwesomeKind>(this.Kind);
        }
    }
}