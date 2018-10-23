using System.Windows.Markup;

namespace MahApps.Metro.IconPacks
{
    [MarkupExtensionReturnType(typeof(PackIconIonicons))]
    public class IoniconsExtension : PackIconExtension<PackIconIonicons, PackIconIoniconsKind>
    {
        public IoniconsExtension()
        {
        }

        public IoniconsExtension(PackIconIoniconsKind kind) : base(kind)
        {
        }
    }
}