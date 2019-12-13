#if (NETFX_CORE || WINDOWS_UWP)
using Windows.UI.Xaml.Markup;
#else
using System;
using System.Windows.Markup;
#endif

namespace MahApps.Metro.IconPacks
{
#if (NETFX_CORE || WINDOWS_UWP)
    [MarkupExtensionReturnType(ReturnType = typeof(PackIconOcticons))]
#else
    [MarkupExtensionReturnType(typeof(PackIconOcticons))]
#endif
    public class OcticonsExtension : BasePackIconExtension
    {
        public OcticonsExtension()
        {
        }

#if !(NETFX_CORE || WINDOWS_UWP)
        public OcticonsExtension(PackIconOcticonsKind kind)
        {
            this.Kind = kind;
        }

        [ConstructorArgument("kind")]
#endif
        public PackIconOcticonsKind Kind { get; set; }

#if (NETFX_CORE || WINDOWS_UWP)
        protected override object ProvideValue()
#else
        public override object ProvideValue(IServiceProvider serviceProvider)
#endif
        {
            return this.GetPackIcon<PackIconOcticons, PackIconOcticonsKind>(this.Kind);
        }
    }
}