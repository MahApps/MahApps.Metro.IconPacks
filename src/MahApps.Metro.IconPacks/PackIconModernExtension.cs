using System.Windows.Markup;

namespace MahApps.Metro.IconPacks
{
    [MarkupExtensionReturnType(typeof(PackIconModern))]
    public class ModernExtension : PackIconExtension<PackIconModern, PackIconModernKind>
    {
        public ModernExtension()
        {
        }

        public ModernExtension(PackIconModernKind kind) : base(kind)
        {
        }
    }
}