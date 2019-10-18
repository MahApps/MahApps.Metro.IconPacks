using System;
#if (NETFX_CORE || WINDOWS_UWP)
using Windows.UI.Xaml;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
#else
using System.Windows.Markup;
using System.Windows.Media.Animation;
#endif

namespace MahApps.Metro.IconPacks
{
#if (NETFX_CORE || WINDOWS_UWP)
    [MarkupExtensionReturnType(ReturnType = typeof(PackIconWeatherIcons))]
#else
    [MarkupExtensionReturnType(typeof(PackIconWeatherIcons))]
#endif
    public class WeatherIconsExtension : BasePackIconExtension
    {
        public WeatherIconsExtension()
        {
        }

#if !(NETFX_CORE || WINDOWS_UWP)
        public WeatherIconsExtension(PackIconWeatherIconsKind kind)
        {
            this.Kind = kind;
        }

        [ConstructorArgument("kind")]
#endif
        public PackIconWeatherIconsKind Kind { get; set; }

#if (NETFX_CORE || WINDOWS_UWP)
        protected override object ProvideValue()
#else
        public override object ProvideValue(IServiceProvider serviceProvider)
#endif
        {
            return this.GetPackIcon<PackIconWeatherIcons, PackIconWeatherIconsKind>(this.Kind);
        }
    }
}