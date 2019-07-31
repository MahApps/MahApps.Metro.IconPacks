using Windows.UI.Xaml.Media;

namespace MahApps.Metro.IconPacks
{
    /// <summary>
    /// All icons sourced from Modern UI Icons Font - <see><cref>http://modernuiicons.com</cref></see> - in accordance of
    /// <see><cref>https://github.com/Templarian/WindowsIcons/blob/master/WindowsPhone/license.txt</cref></see>.
    /// </summary>
    public class PathIconModern : PathIconControl<PackIconModernKind>
    {
        public PathIconModern() : base(PackIconModernDataFactory.Create)
        {
        }
    }
}