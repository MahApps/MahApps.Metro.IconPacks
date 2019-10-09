using System;
#if (NETFX_CORE || WINDOWS_UWP)
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
#else
using System.Globalization;
using System.Windows;
#endif

namespace MahApps.Metro.IconPacks.Converter
{
#if (NETFX_CORE || WINDOWS_UWP)
    public class DataTypeValueConverter : MarkupConverter
    {
        private static DataTypeValueConverter _instance;

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static DataTypeValueConverter()
        {
        }

        public static IValueConverter Instance { get; } = _instance ?? (_instance = new DataTypeValueConverter());

        protected override object Convert(object value, Type targetType, object parameter, string language)
        {
            return value?.GetType();
        }

        protected override object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return DependencyProperty.UnsetValue;
        }
    }
#else
    public class DataTypeValueConverter : MarkupConverter
    {
        private static DataTypeValueConverter _instance;

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static DataTypeValueConverter()
        {
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return _instance ?? (_instance = new DataTypeValueConverter());
        }

        protected override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value?.GetType();
        }

        protected override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
#endif
}