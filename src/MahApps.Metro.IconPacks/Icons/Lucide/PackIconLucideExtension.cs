#if (NETFX_CORE || WINDOWS_UWP)
using Windows.UI.Xaml.Markup;
#else
using System;
using System.Windows.Markup;
#endif

namespace MahApps.Metro.IconPacks
{
#if (NETFX_CORE || WINDOWS_UWP)
    [MarkupExtensionReturnType(ReturnType = typeof(PackIconLucide))]
#else
    [MarkupExtensionReturnType(typeof(PackIconLucide))]
#endif
    public class LucideExtension : BasePackIconExtension
    {
        public LucideExtension()
        {
        }

#if !(NETFX_CORE || WINDOWS_UWP)
        public LucideExtension(PackIconLucideKind kind)
        {
            this.Kind = kind;
        }

        [ConstructorArgument("kind")]
#endif
        public PackIconLucideKind Kind { get; set; }

#if (NETFX_CORE || WINDOWS_UWP)
        protected override object ProvideValue()
#else
        public override object ProvideValue(IServiceProvider serviceProvider)
#endif
        {
            return this.GetPackIcon<PackIconLucide, PackIconLucideKind>(this.Kind);
        }
    }
}