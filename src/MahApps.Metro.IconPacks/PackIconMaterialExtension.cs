using System.Windows.Markup;

namespace MahApps.Metro.IconPacks
{
    [MarkupExtensionReturnType(typeof(PackIconMaterial))]
    public class MaterialExtension : PackIconExtension<PackIconMaterial, PackIconMaterialKind>
    {
        public MaterialExtension()
        {
        }

        public MaterialExtension(PackIconMaterialKind kind) : base(kind)
        {
        }
    }
}