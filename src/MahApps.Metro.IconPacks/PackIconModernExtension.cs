using System.Windows.Markup;

namespace MahApps.Metro.IconPacks
{
    [MarkupExtensionReturnType(typeof(PackIconModern))]
    public class PackIconModernExtension : PackIconExtension<PackIconModern, PackIconModernKind>
    {
        public PackIconModernExtension()
        {
        }

        public PackIconModernExtension(PackIconModernKind kind) : base(kind)
        {
        }
    }
}