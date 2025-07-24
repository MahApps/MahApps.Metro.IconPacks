using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace MahApps.Metro.IconPacks
{
    /// <summary>
    /// All icons sourced from Font Awesome Free <see><cref>https://fontawesome.com/</cref></see> - License <see><cref>https://fontawesome.com/license/free</cref></see>
    /// GitHub <see><cref>https://fontawesome.com/</cref></see>
    /// </summary>
    [MetaData("Font Awesome Free v5", "https://fontawesome.com/", "https://fontawesome.com/license/free")]
    public class PathIconFontAwesome5 : PathIconControlBase
    {
        public static readonly DependencyProperty KindProperty
            = DependencyProperty.Register(nameof(Kind), typeof(PackIconFontAwesome5Kind), typeof(PathIconFontAwesome5), new PropertyMetadata(default(PackIconFontAwesome5Kind), KindPropertyChangedCallback));

        private static void KindPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != e.OldValue)
            {
                ((PathIconFontAwesome5)dependencyObject).UpdateData();
            }
        }

        /// <summary>
        /// Gets or sets the icon to display.
        /// </summary>
        public PackIconFontAwesome5Kind Kind
        {
            get { return (PackIconFontAwesome5Kind)GetValue(KindProperty); }
            set { SetValue(KindProperty, value); }
        }

        public PathIconFontAwesome5()
        {
        }

        protected override void UpdateData()
        {
            string data = null;
            PackIconDataFactory<PackIconFontAwesome5Kind>.DataIndex.Value?.TryGetValue(Kind, out data);
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