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
    /// ValueConverter which converts the PackIconFlipOrientation enumeration value to ScaleX value of a ScaleTransformation.
    /// </summary>
#if (NETFX_CORE || WINDOWS_UWP)
    [MarkupExtensionReturnType(ReturnType = typeof(FlipToScaleXValueConverter))]
#else
    [MarkupExtensionReturnType(typeof(FlipToScaleXValueConverter))]
#endif
    public class FlipToScaleXValueConverter : MarkupConverter
    {
        private static FlipToScaleXValueConverter _instance;

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static FlipToScaleXValueConverter()
        {
        }

        public static FlipToScaleXValueConverter Instance { get; } = _instance ?? (_instance = new FlipToScaleXValueConverter());

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
                var scaleX = flip == PackIconFlipOrientation.Horizontal || flip == PackIconFlipOrientation.Both ? -1 : 1;
                return scaleX;
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