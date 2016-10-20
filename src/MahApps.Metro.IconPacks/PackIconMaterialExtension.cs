using System.Windows.Markup;

namespace MahApps.Metro.IconPacks
{
    [MarkupExtensionReturnType(typeof(PackIconMaterial))]
    public class PackIconMaterialExtension : PackIconExtension<PackIconMaterial, PackIconMaterialKind>
    {
        public PackIconMaterialExtension()
        {
        }

        public PackIconMaterialExtension(PackIconMaterialKind kind) : base(kind)
        {
        }
    }
}