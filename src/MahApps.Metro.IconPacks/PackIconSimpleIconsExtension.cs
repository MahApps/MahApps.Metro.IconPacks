using System.Windows.Markup;

namespace MahApps.Metro.IconPacks
{
    [MarkupExtensionReturnType(typeof(PackIconSimpleIcons))]
    public class SimpleIconsExtension : PackIconExtension<PackIconSimpleIcons, PackIconSimpleIconsKind>
    {
        public SimpleIconsExtension()
        {
        }

        public SimpleIconsExtension(PackIconSimpleIconsKind kind) : base(kind)
        {
        }
    }
}