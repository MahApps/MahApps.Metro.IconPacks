using System.Windows.Markup;

namespace MahApps.Metro.IconPacks
{
    [MarkupExtensionReturnType(typeof(PackIconMaterialLight))]
    public class MaterialLightExtension : PackIconExtension<PackIconMaterialLight, PackIconMaterialLightKind>
    {
        public MaterialLightExtension()
        {
        }

        public MaterialLightExtension(PackIconMaterialLightKind kind) : base(kind)
        {
        }
    }
}