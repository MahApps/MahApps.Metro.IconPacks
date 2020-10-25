using System;
using System.Collections.Generic;
#if (NETFX_CORE || WINDOWS_UWP)
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
#else
using System.Windows;
#endif

namespace MahApps.Metro.IconPacks
{
    /// <summary>
    /// Weather Icons are licensed under [SIL OFL 1.1](<see><cref>http://scripts.sil.org/OFL</cref></see>)
    /// Contributions, corrections and requests can be made on GitHub <see><cref>https://github.com/erikflowers/weather-icons</cref></see>.
    /// </summary>
    [MetaData("Weather Icons", "https://github.com/erikflowers/weather-icons", "https://github.com/erikflowers/weather-icons#licensing")]
    public class PackIconWeatherIcons : PackIconControlBase
    {
        public static readonly DependencyProperty KindProperty
            = DependencyProperty.Register(nameof(Kind), typeof(PackIconWeatherIconsKind), typeof(PackIconWeatherIcons), new PropertyMetadata(default(PackIconWeatherIconsKind), KindPropertyChangedCallback));

        private static void KindPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != e.OldValue)
            {
                ((PackIconWeatherIcons)dependencyObject).UpdateData();
            }
        }

        /// <summary>
        /// Gets or sets the icon to display.
        /// </summary>
        public PackIconWeatherIconsKind Kind
        {
            get { return (PackIconWeatherIconsKind)GetValue(KindProperty); }
            set { SetValue(KindProperty, value); }
        }

#if !(NETFX_CORE || WINDOWS_UWP)
        static PackIconWeatherIcons()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PackIconWeatherIcons), new FrameworkPropertyMetadata(typeof(PackIconWeatherIcons)));
        }
#endif

        public PackIconWeatherIcons()
        {
#if NETFX_CORE || WINDOWS_UWP
            this.DefaultStyleKey = typeof(PackIconWeatherIcons);
#endif
        }

        protected override void SetKind<TKind>(TKind iconKind)
        {
#if NETFX_CORE || WINDOWS_UWP
            BindingOperations.SetBinding(this, PackIconWeatherIcons.KindProperty, new Binding() { Source = iconKind, Mode = BindingMode.OneTime });
#else
            this.SetCurrentValue(KindProperty, iconKind);
#endif
        }

        protected override void UpdateData()
        {
            if (Kind != default(PackIconWeatherIconsKind))
            {
                string data = null;
                PackIconWeatherIconsDataFactory.DataIndex.Value?.TryGetValue(Kind, out data);
                this.Data = data;
            }
            else
            {
                this.Data = null;
            }
        }
    }
}