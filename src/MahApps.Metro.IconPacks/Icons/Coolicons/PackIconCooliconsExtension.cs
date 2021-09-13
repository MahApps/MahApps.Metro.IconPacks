#if (NETFX_CORE || WINDOWS_UWP)
using Windows.UI.Xaml.Markup;
#else
using System;
using System.Windows.Markup;
#endif

namespace MahApps.Metro.IconPacks
{
#if (NETFX_CORE || WINDOWS_UWP)
    [MarkupExtensionReturnType(ReturnType = typeof(PackIconCoolicons))]
#else
    [MarkupExtensionReturnType(typeof(PackIconCoolicons))]
#endif
    public class CooliconsExtension : BasePackIconExtension
    {
        public CooliconsExtension()
        {
        }

#if !(NETFX_CORE || WINDOWS_UWP)
        public CooliconsExtension(PackIconCooliconsKind kind)
        {
            this.Kind = kind;
        }

        [ConstructorArgument("kind")]
#endif
        public PackIconCooliconsKind Kind { get; set; }

#if (NETFX_CORE || WINDOWS_UWP)
        protected override object ProvideValue()
#else
        public override object ProvideValue(IServiceProvider serviceProvider)
#endif
        {
            return this.GetPackIcon<PackIconCoolicons, PackIconCooliconsKind>(this.Kind);
        }
    }
}