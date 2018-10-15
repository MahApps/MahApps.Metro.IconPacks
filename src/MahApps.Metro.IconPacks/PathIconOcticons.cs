using Windows.UI.Xaml.Media;

namespace MahApps.Metro.IconPacks
{
    /// <summary>
    /// Icons from GitHub Octicons - <see><cref>https://octicons.github.com</cref></see>
    /// </summary>
    public class PathIconOcticons : PathIconControl<PackIconOcticonsKind>
    {
        public PathIconOcticons() : base(PackIconOcticonsDataFactory.Create)
        {
        }
    }
}