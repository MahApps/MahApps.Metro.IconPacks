using System.Windows.Markup;

namespace MahApps.Metro.IconPacks
{
    [MarkupExtensionReturnType(typeof(PackIconMaterialLight))]
    public class PackIconMaterialLightExtension : PackIconExtension<PackIconMaterialLight, PackIconMaterialLightKind>
    {
        public PackIconMaterialLightExtension()
        {
        }

        public PackIconMaterialLightExtension(PackIconMaterialLightKind kind) : base(kind)
        {
        }
    }
}