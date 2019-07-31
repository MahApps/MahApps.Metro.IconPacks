using Windows.UI.Xaml.Media;

namespace MahApps.Metro.IconPacks
{
    /// <summary>
    /// All icons sourced from Font Awesome Free <see><cref>https://fontawesome.com/</cref></see> - License <see><cref>https://fontawesome.com/license/free</cref></see>
    /// GitHub <see><cref>https://github.com/FortAwesome/Font-Awesome</cref></see>
    /// </summary>
    public class PathIconFontAwesome : PathIconControl<PackIconFontAwesomeKind>
    {
        public PathIconFontAwesome() : base(PackIconFontAwesomeDataFactory.Create)
        {
        }
    }
}