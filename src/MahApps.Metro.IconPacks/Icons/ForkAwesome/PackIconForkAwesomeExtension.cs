#if (NETFX_CORE || WINDOWS_UWP)
using Windows.UI.Xaml.Markup;
#else
using System;
using System.Windows.Markup;
#endif

namespace MahApps.Metro.IconPacks
{
#if (NETFX_CORE || WINDOWS_UWP)
    [MarkupExtensionReturnType(ReturnType = typeof(PackIconForkAwesome))]
#else
    [MarkupExtensionReturnType(typeof(PackIconForkAwesome))]
#endif
    public class ForkAwesomeExtension : BasePackIconExtension
    {
        public ForkAwesomeExtension()
        {
        }

#if !(NETFX_CORE || WINDOWS_UWP)
        public ForkAwesomeExtension(PackIconForkAwesomeKind kind)
        {
            this.Kind = kind;
        }

        [ConstructorArgument("kind")]
#endif
        public PackIconForkAwesomeKind Kind { get; set; }

#if (NETFX_CORE || WINDOWS_UWP)
        protected override object ProvideValue()
#else
        public override object ProvideValue(IServiceProvider serviceProvider)
#endif
        {
            return this.GetPackIcon<PackIconForkAwesome, PackIconForkAwesomeKind>(this.Kind);
        }
    }
}