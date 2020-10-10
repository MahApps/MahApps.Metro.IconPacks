#if (NETFX_CORE || WINDOWS_UWP)
using Windows.UI.Xaml.Markup;
#else
using System;
using System.Windows.Markup;
#endif

namespace MahApps.Metro.IconPacks
{
#if (NETFX_CORE || WINDOWS_UWP)
    [MarkupExtensionReturnType(ReturnType = typeof(PackIconRemixIcon))]
#else
    [MarkupExtensionReturnType(typeof(PackIconRemixIcon))]
#endif
    public class RemixIconExtension : BasePackIconExtension
    {
        public RemixIconExtension()
        {
        }

#if !(NETFX_CORE || WINDOWS_UWP)
        public RemixIconExtension(PackIconRemixIconKind kind)
        {
            this.Kind = kind;
        }

        [ConstructorArgument("kind")]
#endif
        public PackIconRemixIconKind Kind { get; set; }

#if (NETFX_CORE || WINDOWS_UWP)
        protected override object ProvideValue()
#else
        public override object ProvideValue(IServiceProvider serviceProvider)
#endif
        {
            return this.GetPackIcon<PackIconRemixIcon, PackIconRemixIconKind>(this.Kind);
        }
    }
}