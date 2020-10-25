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
    /// GitHub <see><cref>https://github.com/FortAwesome/Font-Awesome</cref></see>
    /// </summary>
    [MetaData("Font Awesome Free", "https://fontawesome.com/", "https://fontawesome.com/license/free")]
    public class PathIconFontAwesome : PathIconControlBase
    {
        public static readonly DependencyProperty KindProperty
            = DependencyProperty.Register(nameof(Kind), typeof(PackIconFontAwesomeKind), typeof(PathIconFontAwesome), new PropertyMetadata(default(PackIconFontAwesomeKind), KindPropertyChangedCallback));

        private static void KindPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != e.OldValue)
            {
                ((PathIconFontAwesome)dependencyObject).UpdateData();
            }
        }

        /// <summary>
        /// Gets or sets the icon to display.
        /// </summary>
        public PackIconFontAwesomeKind Kind
        {
            get { return (PackIconFontAwesomeKind)GetValue(KindProperty); }
            set { SetValue(KindProperty, value); }
        }

        public PathIconFontAwesome()
        {
        }

        protected override void UpdateData()
        {
            string data = null;
            PackIconFontAwesomeDataFactory.DataIndex.Value?.TryGetValue(Kind, out data);
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