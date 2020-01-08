using System;
#if (NETFX_CORE || WINDOWS_UWP)
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Markup;
#else
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
#endif

namespace MahApps.Metro.IconPacks.Converter
{
    /// <summary>
    /// MarkupConverter is a MarkupExtension which can be used for IValueConverter.
    /// </summary>
#if (NETFX_CORE || WINDOWS_UWP)
    [MarkupExtensionReturnType(ReturnType = typeof(IValueConverter))]
    public abstract class MarkupConverter : MarkupExtension, IValueConverter
    {
        /// <inheritdoc />
        protected override object ProvideValue()
        {
            return this;
        }

        /// <summary>
        /// Converts a value.
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="language">The culture language to use in the converter.</param>
        /// <returns>A converted value. If the method returns null, the valid null value is used.</returns>
        protected abstract object Convert(object value, Type targetType, object parameter, string language);

        /// <summary>
        /// Converts a value.
        /// </summary>
        /// <param name="value">The value that is produced by the binding target.</param>
        /// <param name="targetType">The type to convert to.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="language">The culture language to use in the converter.</param>
        /// <returns>A converted value. If the method returns null, the valid null value is used.</returns>
        protected abstract object ConvertBack(object value, Type targetType, object parameter, string language);

        /// <inheritdoc />
        object IValueConverter.Convert(object value, Type targetType, object parameter, string language)
        {
            try
            {
                return Convert(value, targetType, parameter, language);
            }
            catch
            {
                return DependencyProperty.UnsetValue;
            }
        }

        /// <inheritdoc />
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, string language)
        {
            try
            {
                return ConvertBack(value, targetType, parameter, language);
            }
            catch
            {
                return DependencyProperty.UnsetValue;
            }
        }
    }
#else
    [MarkupExtensionReturnType(typeof(IValueConverter))]
    public abstract class MarkupConverter : MarkupExtension, IValueConverter
    {
        /// <inheritdoc />
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }

        /// <summary>
        /// Converts a value.
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>A converted value. If the method returns null, the valid null value is used.</returns>
        protected abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);

        /// <summary>
        /// Converts a value.
        /// </summary>
        /// <param name="value">The value that is produced by the binding target.</param>
        /// <param name="targetType">The type to convert to.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>A converted value. If the method returns null, the valid null value is used.</returns>
        protected abstract object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture);

        /// <inheritdoc />
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                return Convert(value, targetType, parameter, culture);
            }
            catch
            {
                return DependencyProperty.UnsetValue;
            }
        }

        /// <inheritdoc />
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                return ConvertBack(value, targetType, parameter, culture);
            }
            catch
            {
                return DependencyProperty.UnsetValue;
            }
        }
    }
#endif
}