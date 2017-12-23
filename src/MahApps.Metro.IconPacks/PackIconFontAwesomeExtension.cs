using System.Windows.Markup;

namespace MahApps.Metro.IconPacks
{
    [MarkupExtensionReturnType(typeof(PackIconFontAwesome))]
    public class FontAwesomeExtension : PackIconExtension<PackIconFontAwesome, PackIconFontAwesomeKind>
    {
        public FontAwesomeExtension()
        {
        }

        public FontAwesomeExtension(PackIconFontAwesomeKind kind) : base(kind)
        {
        }
    }
}