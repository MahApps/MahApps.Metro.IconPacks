using System.Windows.Markup;

namespace MahApps.Metro.IconPacks
{
    [MarkupExtensionReturnType(typeof(PackIconOcticons))]
    public class PackIconOcticonsExtension : PackIconExtension<PackIconOcticons, PackIconOcticonsKind>
    {
        public PackIconOcticonsExtension()
        {
        }

        public PackIconOcticonsExtension(PackIconOcticonsKind kind) : base(kind)
        {
        }
    }
}