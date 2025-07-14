#if (NETFX_CORE || WINDOWS_UWP)
using Windows.UI.Xaml.Markup;
#else
using System;
using System.Windows.Markup;
#endif

namespace MahApps.Metro.IconPacks
{
#if (NETFX_CORE || WINDOWS_UWP)
    [MarkupExtensionReturnType(ReturnType = typeof(PackIconMynaUIIcons))]
#else
    [MarkupExtensionReturnType(typeof(PackIconMynaUIIcons))]
#endif
    public class MynaUIIconsExtension : BasePackIconExtension
    {
        public MynaUIIconsExtension()
        {
        }

#if !(NETFX_CORE || WINDOWS_UWP)
        public MynaUIIconsExtension(PackIconMynaUIIconsKind kind)
        {
            this.Kind = kind;
        }

        [ConstructorArgument("kind")]
#endif
        public PackIconMynaUIIconsKind Kind { get; set; }

#if (NETFX_CORE || WINDOWS_UWP)
        protected override object ProvideValue()
#else
        public override object ProvideValue(IServiceProvider serviceProvider)
#endif
        {
            return this.GetPackIcon<PackIconMynaUIIcons, PackIconMynaUIIconsKind>(this.Kind);
        }
    }
}