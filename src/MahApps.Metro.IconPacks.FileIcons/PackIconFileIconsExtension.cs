#if (NETFX_CORE || WINDOWS_UWP)
using Windows.UI.Xaml.Markup;
#else
using System;
using System.Windows.Markup;
#endif

namespace MahApps.Metro.IconPacks
{
#if (NETFX_CORE || WINDOWS_UWP)
    [MarkupExtensionReturnType(ReturnType = typeof(PackIconFileIcons))]
#else
    [MarkupExtensionReturnType(typeof(PackIconFileIcons))]
#endif
    public class FileIconsExtension : BasePackIconExtension
    {
        public FileIconsExtension()
        {
        }

#if !(NETFX_CORE || WINDOWS_UWP)
        public FileIconsExtension(PackIconFileIconsKind kind)
        {
            this.Kind = kind;
        }

        [ConstructorArgument("kind")]
#endif
        public PackIconFileIconsKind Kind { get; set; }

#if (NETFX_CORE || WINDOWS_UWP)
        protected override object ProvideValue()
#else
        public override object ProvideValue(IServiceProvider serviceProvider)
#endif
        {
            return this.GetPackIcon<PackIconFileIcons, PackIconFileIconsKind>(this.Kind);
        }
    }
}