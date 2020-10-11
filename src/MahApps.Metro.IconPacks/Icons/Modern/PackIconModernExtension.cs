#if (NETFX_CORE || WINDOWS_UWP)
using Windows.UI.Xaml.Markup;
#else
using System;
using System.Windows.Markup;
#endif

namespace MahApps.Metro.IconPacks
{
#if (NETFX_CORE || WINDOWS_UWP)
    [MarkupExtensionReturnType(ReturnType = typeof(PackIconModern))]
#else
    [MarkupExtensionReturnType(typeof(PackIconModern))]
#endif
    public class ModernExtension : BasePackIconExtension
    {
        public ModernExtension()
        {
        }

#if !(NETFX_CORE || WINDOWS_UWP)
        public ModernExtension(PackIconModernKind kind)
        {
            this.Kind = kind;
        }

        [ConstructorArgument("kind")]
#endif
        public PackIconModernKind Kind { get; set; }

#if (NETFX_CORE || WINDOWS_UWP)
        protected override object ProvideValue()
#else
        public override object ProvideValue(IServiceProvider serviceProvider)
#endif
        {
            return this.GetPackIcon<PackIconModern, PackIconModernKind>(this.Kind);
        }
    }
}