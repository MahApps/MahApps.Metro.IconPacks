using System.Windows.Markup;

namespace MahApps.Metro.IconPacks
{
    [MarkupExtensionReturnType(typeof(PackIconUnicons))]
    public class UniconsExtension : PackIconExtension<PackIconUnicons, PackIconUniconsKind>
    {
        public UniconsExtension()
        {
        }

        public UniconsExtension(PackIconUniconsKind kind) : base(kind)
        {
        }
    }
}