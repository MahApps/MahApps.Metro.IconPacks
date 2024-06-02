using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace MahApps.Metro.IconPacks
{
    /// <summary>
    /// All icons sourced from GitHub Octicons <see><cref>https://github.com/Klarr-Agency/Circum-Icons</cref></see>
    /// In accordance of <see><cref>https://github.com/Klarr-Agency/Circum-Icons?tab=MPL-2.0-1-ov-file</cref></see>.
    /// </summary>
    [MetaData("Circum Icons Free", "https://github.com/Klarr-Agency/Circum-Icons", "https://github.com/Klarr-Agency/Circum-Icons?tab=MPL-2.0-1-ov-file")]
    public class PathIconCircumIcons : PathIconControlBase
    {
        public static readonly DependencyProperty KindProperty
            = DependencyProperty.Register(nameof(Kind), typeof(PackIconCircumIconsKind), typeof(PathIconCircumIcons), new PropertyMetadata(default(PackIconCircumIconsKind), KindPropertyChangedCallback));

        private static void KindPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != e.OldValue)
            {
                ((PathIconCircumIcons)dependencyObject).UpdateData();
            }
        }

        /// <summary>
        /// Gets or sets the icon to display.
        /// </summary>
        public PackIconCircumIconsKind Kind
        {
            get { return (PackIconCircumIconsKind)GetValue(KindProperty); }
            set { SetValue(KindProperty, value); }
        }

        public PathIconCircumIcons()
        {
        }

        protected override void UpdateData()
        {
            string data = null;
            PackIconCircumIconsDataFactory.DataIndex.Value?.TryGetValue(Kind, out data);
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