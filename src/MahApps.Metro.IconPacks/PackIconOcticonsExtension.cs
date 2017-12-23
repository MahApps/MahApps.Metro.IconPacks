using System.Windows.Markup;

namespace MahApps.Metro.IconPacks
{
    [MarkupExtensionReturnType(typeof(PackIconOcticons))]
    public class OcticonsExtension : PackIconExtension<PackIconOcticons, PackIconOcticonsKind>
    {
        public OcticonsExtension()
        {
        }

        public OcticonsExtension(PackIconOcticonsKind kind) : base(kind)
        {
        }
    }
}