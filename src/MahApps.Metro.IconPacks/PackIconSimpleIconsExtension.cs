using System.Windows.Markup;

namespace MahApps.Metro.IconPacks
{
    [MarkupExtensionReturnType(typeof(PackIconSimpleIcons))]
    public class PackIconSimpleIconsExtension : PackIconExtension<PackIconSimpleIcons, PackIconSimpleIconsKind>
    {
        public PackIconSimpleIconsExtension()
        {
        }

        public PackIconSimpleIconsExtension(PackIconSimpleIconsKind kind) : base(kind)
        {
        }
    }
}