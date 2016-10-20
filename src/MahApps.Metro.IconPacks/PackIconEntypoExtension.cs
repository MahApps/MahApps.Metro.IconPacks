using System.Windows.Markup;

namespace MahApps.Metro.IconPacks
{
    [MarkupExtensionReturnType(typeof(PackIconEntypo))]
    public class PackIconEntypoExtension : PackIconExtension<PackIconEntypo, PackIconEntypoKind>
    {
        public PackIconEntypoExtension()
        {
        }

        public PackIconEntypoExtension(PackIconEntypoKind kind) : base(kind)
        {
        }
    }
}