
#if !NETFX_CORE
using System.Windows;
#endif

namespace MahApps.Metro.IconPacks
{
    /// <summary>
    /// Weather Icons licensed under [SIL OFL 1.1](<see><cref>http://scripts.sil.org/OFL</cref></see>)
    /// Contributions, corrections and requests can be made on GitHub <see><cref>https://github.com/erikflowers/weather-icons</cref></see>.
    /// </summary>
    public class PackIconWeatherIcons : PackIconControl<PackIconWeatherIconsKind>
    {
#if !NETFX_CORE
        static PackIconWeatherIcons()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PackIconWeatherIcons), new FrameworkPropertyMetadata(typeof(PackIconWeatherIcons)));
        }
#endif

        public PackIconWeatherIcons() : base(PackIconWeatherIconsDataFactory.Create)
        {
#if NETFX_CORE
            this.DefaultStyleKey = typeof(PackIconWeatherIcons);
#endif
        }
    }
}