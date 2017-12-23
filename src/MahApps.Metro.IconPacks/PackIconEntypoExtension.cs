using System.Windows.Markup;

namespace MahApps.Metro.IconPacks
{
    [MarkupExtensionReturnType(typeof(PackIconEntypo))]
    public class EntypoExtension : PackIconExtension<PackIconEntypo, PackIconEntypoKind>
    {
        public EntypoExtension()
        {
        }

        public EntypoExtension(PackIconEntypoKind kind) : base(kind)
        {
        }
    }
}