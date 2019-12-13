#if (NETFX_CORE || WINDOWS_UWP)
using Windows.UI.Xaml.Markup;
#else
using System;
using System.Windows.Markup;
#endif

namespace MahApps.Metro.IconPacks
{
#if (NETFX_CORE || WINDOWS_UWP)
    [MarkupExtensionReturnType(ReturnType = typeof(PackIconMicrons))]
#else
    [MarkupExtensionReturnType(typeof(PackIconMicrons))]
#endif
    public class MicronsExtension : BasePackIconExtension
    {
        public MicronsExtension()
        {
        }

#if !(NETFX_CORE || WINDOWS_UWP)
        public MicronsExtension(PackIconMicronsKind kind)
        {
            this.Kind = kind;
        }

        [ConstructorArgument("kind")]
#endif
        public PackIconMicronsKind Kind { get; set; }

#if (NETFX_CORE || WINDOWS_UWP)
        protected override object ProvideValue()
#else
        public override object ProvideValue(IServiceProvider serviceProvider)
#endif
        {
            return this.GetPackIcon<PackIconMicrons, PackIconMicronsKind>(this.Kind);
        }
    }
}