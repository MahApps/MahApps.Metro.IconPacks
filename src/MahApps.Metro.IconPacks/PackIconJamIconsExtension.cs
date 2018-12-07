using System.Windows.Markup;

namespace MahApps.Metro.IconPacks
{
    [MarkupExtensionReturnType(typeof(PackIconJamIcons))]
    public class JamIconsExtension : PackIconExtension<PackIconJamIcons, PackIconJamIconsKind>
    {
        public JamIconsExtension()
        {
        }

        public JamIconsExtension(PackIconJamIconsKind kind) : base(kind)
        {
        }
    }
}