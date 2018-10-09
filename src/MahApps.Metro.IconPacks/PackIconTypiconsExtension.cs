using System.Windows.Markup;

namespace MahApps.Metro.IconPacks
{
    [MarkupExtensionReturnType(typeof(PackIconTypicons))]
    public class TypiconsExtension : PackIconExtension<PackIconTypicons, PackIconTypiconsKind>
    {
        public TypiconsExtension()
        {
        }

        public TypiconsExtension(PackIconTypiconsKind kind) : base(kind)
        {
        }
    }
}