using Windows.UI.Xaml.Media;

namespace MahApps.Metro.IconPacks
{
    /// <summary>
    /// Icons from the Modern UI Icons project, <see><cref>http://modernuiicons.com</cref></see>.
    /// </summary>
    public class PathIconModern : PathIconControl<PackIconModernKind>
    {
        public PathIconModern() : base(PackIconModernDataFactory.Create)
        {
        }
    }
}