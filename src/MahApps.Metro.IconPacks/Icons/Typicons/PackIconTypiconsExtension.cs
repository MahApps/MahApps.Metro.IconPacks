#if (NETFX_CORE || WINDOWS_UWP)
using Windows.UI.Xaml.Markup;
#else
using System;
using System.Windows.Markup;
#endif

namespace MahApps.Metro.IconPacks
{
#if (NETFX_CORE || WINDOWS_UWP)
    [MarkupExtensionReturnType(ReturnType = typeof(PackIconTypicons))]
#else
    [MarkupExtensionReturnType(typeof(PackIconTypicons))]
#endif
    public class TypiconsExtension : BasePackIconExtension
    {
        public TypiconsExtension()
        {
        }

#if !(NETFX_CORE || WINDOWS_UWP)
        public TypiconsExtension(PackIconTypiconsKind kind)
        {
            this.Kind = kind;
        }

        [ConstructorArgument("kind")]
#endif
        public PackIconTypiconsKind Kind { get; set; }

#if (NETFX_CORE || WINDOWS_UWP)
        protected override object ProvideValue()
#else
        public override object ProvideValue(IServiceProvider serviceProvider)
#endif
        {
            return this.GetPackIcon<PackIconTypicons, PackIconTypiconsKind>(this.Kind);
        }
    }
}