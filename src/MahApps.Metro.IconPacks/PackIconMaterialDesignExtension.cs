using System.Windows.Markup;

namespace MahApps.Metro.IconPacks
{
    [MarkupExtensionReturnType(typeof(PackIconMaterialDesign))]
    public class MaterialDesignExtension : PackIconExtension<PackIconMaterialDesign, PackIconMaterialDesignKind>
    {
        public MaterialDesignExtension()
        {
        }

        public MaterialDesignExtension(PackIconMaterialDesignKind kind) : base(kind)
        {
        }
    }
}