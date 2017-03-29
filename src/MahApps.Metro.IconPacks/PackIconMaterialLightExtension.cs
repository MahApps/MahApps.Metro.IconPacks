using System.Windows.Markup;

namespace MahApps.Metro.IconPacks.MarkupExtensions
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