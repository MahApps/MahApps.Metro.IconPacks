#if (NETFX_CORE || WINDOWS_UWP)
using Windows.UI.Xaml.Markup;
#else
using System;
using System.Windows.Markup;
#endif

namespace MahApps.Metro.IconPacks
{
#if (NETFX_CORE || WINDOWS_UWP)
    [MarkupExtensionReturnType(ReturnType = typeof(PackIconBoxIcons2))]
#else
    [MarkupExtensionReturnType(typeof(PackIconBoxIcons2))]
#endif
    public class BoxIcons2Extension : BasePackIconExtension
    {
        public BoxIcons2Extension()
        {
        }

#if !(NETFX_CORE || WINDOWS_UWP)
        public BoxIcons2Extension(PackIconBoxIcons2Kind kind)
        {
            this.Kind = kind;
        }

        [ConstructorArgument("kind")]
#endif
        public PackIconBoxIcons2Kind Kind { get; set; }

#if (NETFX_CORE || WINDOWS_UWP)
        protected override object ProvideValue()
#else
        public override object ProvideValue(IServiceProvider serviceProvider)
#endif
        {
            return this.GetPackIcon<PackIconBoxIcons2, PackIconBoxIcons2Kind>(this.Kind);
        }
    }
}