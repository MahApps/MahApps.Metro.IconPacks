#if (NETFX_CORE || WINDOWS_UWP)
using Windows.UI.Xaml.Markup;
#else
using System;
using System.Windows.Markup;
#endif

namespace MahApps.Metro.IconPacks
{
#if (NETFX_CORE || WINDOWS_UWP)
    [MarkupExtensionReturnType(ReturnType = typeof(PackIconMemoryIcons))]
#else
    [MarkupExtensionReturnType(typeof(PackIconMemoryIcons))]
#endif
    public class MemoryIconsExtension : BasePackIconExtension
    {
        public MemoryIconsExtension()
        {
        }

#if !(NETFX_CORE || WINDOWS_UWP)
        public MemoryIconsExtension(PackIconMemoryIconsKind kind)
        {
            this.Kind = kind;
        }

        [ConstructorArgument("kind")]
#endif
        public PackIconMemoryIconsKind Kind { get; set; }

#if (NETFX_CORE || WINDOWS_UWP)
        protected override object ProvideValue()
#else
        public override object ProvideValue(IServiceProvider serviceProvider)
#endif
        {
            return this.GetPackIcon<PackIconMemoryIcons, PackIconMemoryIconsKind>(this.Kind);
        }
    }
}