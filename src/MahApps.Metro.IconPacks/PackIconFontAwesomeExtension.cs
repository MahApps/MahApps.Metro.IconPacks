using System.Windows.Markup;

namespace MahApps.Metro.IconPacks
{
    [MarkupExtensionReturnType(typeof(PackIconFontAwesome))]
    public class PackIconFontAwesomeExtension : PackIconExtension<PackIconFontAwesome, PackIconFontAwesomeKind>
    {
        public PackIconFontAwesomeExtension()
        {
        }

        public PackIconFontAwesomeExtension(PackIconFontAwesomeKind kind) : base(kind)
        {
        }
    }
}