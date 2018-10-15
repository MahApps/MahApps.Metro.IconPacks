using Windows.UI.Xaml.Media;

namespace MahApps.Metro.IconPacks
{
    /// <summary>
    /// Weather Icons licensed under [SIL OFL 1.1](<see><cref>http://scripts.sil.org/OFL</cref></see>)
    /// Contributions, corrections and requests can be made on GitHub <see><cref>https://github.com/erikflowers/weather-icons</cref></see>.
    /// </summary>
    public class PathIconWeatherIcons : PathIconControl<PackIconWeatherIconsKind>
    {
        public PathIconWeatherIcons() : base(PackIconWeatherIconsDataFactory.Create)
        {
        }
    }
}