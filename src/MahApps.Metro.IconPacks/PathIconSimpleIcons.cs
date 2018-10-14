using Windows.UI.Xaml.Media;

namespace MahApps.Metro.IconPacks
{
    /// <summary>
    /// All SVG icons for popular brands, maintained by Dan Leech <see><cref>https://twitter.com/bathtype</cref></see>.
    /// Contributions, corrections and requests can be made on GitHub <see><cref>https://github.com/danleech/simple-icons</cref></see>.
    /// </summary>
    public class PathIconSimpleIcons : PathIconControl<PackIconSimpleIconsKind>
    {
        public PathIconSimpleIcons() : base(PackIconSimpleIconsDataFactory.Create)
        {
        }
    }
}