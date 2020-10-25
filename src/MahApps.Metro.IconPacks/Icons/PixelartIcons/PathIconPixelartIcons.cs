using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace MahApps.Metro.IconPacks
{
    /// <summary>
    /// PixelartIcons are licensed under the [MIT license](<see><cref>https://github.com/halfmage/pixelarticons/blob/master/LICENSE</cref></see>).
    /// Contributions, corrections and requests can be made on GitHub <see><cref>https://github.com/halfmage/pixelarticons</cref></see>.
    /// </summary>
    [MetaData("Pixelarticons", "https://pixelarticons.com/", "https://github.com/halfmage/pixelarticons/blob/master/LICENSE")]
    public class PathIconPixelartIcons : PathIconControlBase
    {
        public static readonly DependencyProperty KindProperty
            = DependencyProperty.Register(nameof(Kind), typeof(PackIconPixelartIconsKind), typeof(PathIconPixelartIcons), new PropertyMetadata(default(PackIconPixelartIconsKind), KindPropertyChangedCallback));

        private static void KindPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != e.OldValue)
            {
                ((PathIconPixelartIcons)dependencyObject).UpdateData();
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

        public PathIconPixelartIcons()
        {
        }

        protected override void UpdateData()
        {
            string data = null;
            PackIconPixelartIconsDataFactory.DataIndex.Value?.TryGetValue(Kind, out data);
            if (string.IsNullOrEmpty(data))
            {
                this.Data = default(Geometry);
            }
            else
            {
                BindingOperations.SetBinding(this, PathIcon.DataProperty, new Binding() {Source = data, Mode = BindingMode.OneTime});
            }
        }
    }
}