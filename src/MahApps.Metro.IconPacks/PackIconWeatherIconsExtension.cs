using System.Windows.Markup;

namespace MahApps.Metro.IconPacks
{
    [MarkupExtensionReturnType(typeof(PackIconWeatherIcons))]
    public class WeatherIconsExtension : PackIconExtension<PackIconWeatherIcons, PackIconWeatherIconsKind>
    {
        public WeatherIconsExtension()
        {
        }

        public WeatherIconsExtension(PackIconWeatherIconsKind kind) : base(kind)
        {
        }
    }
}