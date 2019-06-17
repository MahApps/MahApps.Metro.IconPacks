using Windows.UI.Xaml.Media;

namespace MahApps.Metro.IconPacks
{
    /// <summary>
    /// Zondicons is licensed under the [CC BY 4.0](<see><cref>https://creativecommons.org/licenses/by/4.0/</cref></see>).
    /// Zondicons are availabe at <see><cref>https://www.zondicons.com/</cref></see>.
    /// </summary>
    public class PathIconZondicons : PathIconControl<PackIconZondiconsKind>
    {
        public PathIconZondicons() : base(PackIconZondiconsDataFactory.Create)
        {
        }
    }
}