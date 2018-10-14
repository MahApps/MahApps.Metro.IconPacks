using Windows.UI.Xaml.Media;

namespace MahApps.Metro.IconPacks
{
    /// <summary>
    /// Icons from the FontAwesome Icons project, <see><cref>http://fontawesome.io</cref></see>.
    /// </summary>
    public class PathIconFontAwesome : PathIconControl<PackIconFontAwesomeKind>
    {
        public PathIconFontAwesome() : base(PackIconFontAwesomeDataFactory.Create)
        {
        }
    }
}