using System.Windows.Markup;

namespace MahApps.Metro.IconPacks
{
    [MarkupExtensionReturnType(typeof(PackIconFeatherIcons))]
    public class FeatherIconsExtension : PackIconExtension<PackIconFeatherIcons, PackIconFeatherIconsKind>
    {
        public FeatherIconsExtension()
        {
        }

        public FeatherIconsExtension(PackIconFeatherIconsKind kind) : base(kind)
        {
        }
    }
}