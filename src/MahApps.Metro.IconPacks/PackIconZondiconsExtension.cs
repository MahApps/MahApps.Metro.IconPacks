using System.Windows.Markup;

namespace MahApps.Metro.IconPacks
{
    [MarkupExtensionReturnType(typeof(PackIconZondicons))]
    public class ZondiconsExtension : PackIconExtension<PackIconZondicons, PackIconZondiconsKind>
    {
        public ZondiconsExtension()
        {
        }

        public ZondiconsExtension(PackIconZondiconsKind kind) : base(kind)
        {
        }
    }
}