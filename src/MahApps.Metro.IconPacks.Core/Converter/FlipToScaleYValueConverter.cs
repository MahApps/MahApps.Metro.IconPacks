using System;
#if (NETFX_CORE || WINDOWS_UWP)
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Markup;
#else
using System.Globalization;
using System.Windows;
using System.Windows.Markup;
#endif

namespace MahApps.Metro.IconPacks.Converter
{
    /// <summary>
    /// ValueConverter which converts the PackIconFlipOrientation enumeration value to ScaleY value of a ScaleTransformation.
    /// </summary>
#if (NETFX_CORE || WINDOWS_UWP)
    [MarkupExtensionReturnType(ReturnType = typeof(FlipToScaleYValueConverter))]
#else
    [MarkupExtensionReturnType(typeof(FlipToScaleYValueConverter))]
#endif
    public class FlipToScaleYValueConverter : MarkupConverter
    {
        private static FlipToScaleYValueConverter _instance;

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static FlipToScaleYValueConverter()
        {
        }

        public static FlipToScaleYValueConverter Instance { get; } = _instance ?? (_instance = new FlipToScaleYValueConverter());

#if (NETFX_CORE || WINDOWS_UWP)
        protected override object ProvideValue()
#else
        public override object ProvideValue(IServiceProvider serviceProvider)
#endif
        {
            return Instance;
        }

#if (NETFX_CORE || WINDOWS_UWP)
        protected override object Convert(object value, Type targetType, object parameter, string language)
#else
        protected override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
#endif
        {
            if (value is PackIconFlipOrientation flip)
            {
                var scaleY = flip == PackIconFlipOrientation.Vertical || flip == PackIconFlipOrientation.Both ? -1 : 1;
                return scaleY;
            }

            return 1;
        }

#if (NETFX_CORE || WINDOWS_UWP)
        protected override object ConvertBack(object value, Type targetType, object parameter, string language)
#else
        protected override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
#endif
        {
            return DependencyProperty.UnsetValue;
        }
    }
}