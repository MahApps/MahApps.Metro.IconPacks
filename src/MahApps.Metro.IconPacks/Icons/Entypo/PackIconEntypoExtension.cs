#if (NETFX_CORE || WINDOWS_UWP)
using Windows.UI.Xaml.Markup;
#else
using System;
using System.Windows.Markup;
#endif

namespace MahApps.Metro.IconPacks
{
#if (NETFX_CORE || WINDOWS_UWP)
    [MarkupExtensionReturnType(ReturnType = typeof(PackIconEntypo))]
#else
    [MarkupExtensionReturnType(typeof(PackIconEntypo))]
#endif
    public class EntypoExtension : BasePackIconExtension
    {
        public EntypoExtension()
        {
        }

#if !(NETFX_CORE || WINDOWS_UWP)
        public EntypoExtension(PackIconEntypoKind kind)
        {
            this.Kind = kind;
        }

        [ConstructorArgument("kind")]
#endif
        public PackIconEntypoKind Kind { get; set; }

#if (NETFX_CORE || WINDOWS_UWP)
        protected override object ProvideValue()
#else
        public override object ProvideValue(IServiceProvider serviceProvider)
#endif
        {
            return this.GetPackIcon<PackIconEntypo, PackIconEntypoKind>(this.Kind);
        }
    }
}