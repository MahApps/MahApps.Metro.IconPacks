using Windows.UI.Xaml.Media;

namespace MahApps.Metro.IconPacks
{
    /// <summary>
    /// Ionicons is licensed under the [MIT license](<see><cref>https://github.com/ionic-team/ionicons/blob/master/LICENSE</cref></see>).
    /// Contributions, corrections and requests can be made on GitHub <see><cref>https://github.com/ionic-team/ionicons</cref></see>.
    /// </summary>
    public class PathIconIonicons : PathIconControl<PackIconIoniconsKind>
    {
        public PathIconIonicons() : base(PackIconIoniconsDataFactory.Create)
        {
        }
    }
}