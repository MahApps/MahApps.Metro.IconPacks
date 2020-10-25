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
    /// PixelartIcons are licensed under the [MIT license](<see><cref>https://github.com/halfmage/pixelarticons/blob/master/LICENSE</cref></see>).
    /// Contributions, corrections and requests can be made on GitHub <see><cref>https://github.com/halfmage/pixelarticons</cref></see>.
    /// </summary>
    [MetaData("Pixelarticons", "https://pixelarticons.com/", "https://github.com/halfmage/pixelarticons/blob/master/LICENSE")]
    public class PackIconPixelartIcons : PackIconControlBase
    {
        public static readonly DependencyProperty KindProperty
            = DependencyProperty.Register(nameof(Kind), typeof(PackIconPixelartIconsKind), typeof(PackIconPixelartIcons), new PropertyMetadata(default(PackIconPixelartIconsKind), KindPropertyChangedCallback));

        private static void KindPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != e.OldValue)
            {
                ((PackIconPixelartIcons)dependencyObject).UpdateData();
            }
        }

        /// <summary>
        /// Gets or sets the icon to display.
        /// </summary>
        public PackIconPixelartIconsKind Kind
        {
            get { return (PackIconPixelartIconsKind)GetValue(KindProperty); }
            set { SetValue(KindProperty, value); }
        }

#if !(NETFX_CORE || WINDOWS_UWP)
        static PackIconPixelartIcons()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PackIconPixelartIcons), new FrameworkPropertyMetadata(typeof(PackIconPixelartIcons)));
        }
#endif

        public PackIconPixelartIcons()
        {
#if NETFX_CORE || WINDOWS_UWP
            this.DefaultStyleKey = typeof(PackIconPixelartIcons);
#endif
        }

        protected override void SetKind<TKind>(TKind iconKind)
        {
#if NETFX_CORE || WINDOWS_UWP
            BindingOperations.SetBinding(this, PackIconPixelartIcons.KindProperty, new Binding() { Source = iconKind, Mode = BindingMode.OneTime });
#else
            this.SetCurrentValue(KindProperty, iconKind);
#endif
        }

        protected override void UpdateData()
        {
            if (Kind != default(PackIconPixelartIconsKind))
            {
                string data = null;
                PackIconPixelartIconsDataFactory.DataIndex.Value?.TryGetValue(Kind, out data);
                this.Data = data;
            }
            else
            {
                this.Data = null;
            }
        }
    }
}