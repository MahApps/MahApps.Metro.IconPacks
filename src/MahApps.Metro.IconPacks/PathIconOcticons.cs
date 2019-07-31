using Windows.UI.Xaml.Media;

namespace MahApps.Metro.IconPacks
{
    /// <summary>
    /// All icons sourced from GitHub Octicons - <see><cref>https://octicons.github.com</cref></see> - 
    /// <see><cref>https://github.com/primer/octicons/blob/master/LICENSE</cref></see>.
    /// </summary>
    public class PathIconOcticons : PathIconControl<PackIconOcticonsKind>
    {
        public PathIconOcticons() : base(PackIconOcticonsDataFactory.Create)
        {
        }
    }
}