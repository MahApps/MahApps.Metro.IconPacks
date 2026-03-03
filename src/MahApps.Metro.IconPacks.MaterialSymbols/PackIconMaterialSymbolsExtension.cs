#if (NETFX_CORE || WINDOWS_UWP)
using Windows.UI.Xaml.Markup;
#else
using System;
using System.Windows.Markup;
#endif

namespace MahApps.Metro.IconPacks
{
#if (NETFX_CORE || WINDOWS_UWP)
    [MarkupExtensionReturnType(ReturnType = typeof(PackIconMaterialSymbols))]
#else
    [MarkupExtensionReturnType(typeof(PackIconMaterialSymbols))]
#endif
    public class MaterialSymbolsExtension : BasePackIconExtension
    {
        public MaterialSymbolsExtension()
        {
        }

#if !(NETFX_CORE || WINDOWS_UWP)
        public MaterialSymbolsExtension(PackIconMaterialSymbolsKind kind)
        {
            this.Kind = kind;
        }

        [ConstructorArgument("kind")]
#endif
        public PackIconMaterialSymbolsKind Kind { get; set; }

#if (NETFX_CORE || WINDOWS_UWP)
        protected override object ProvideValue()
#else
        public override object ProvideValue(IServiceProvider serviceProvider)
#endif
        {
            return this.GetPackIcon<PackIconMaterialSymbols, PackIconMaterialSymbolsKind>(this.Kind);
        }
    }
}