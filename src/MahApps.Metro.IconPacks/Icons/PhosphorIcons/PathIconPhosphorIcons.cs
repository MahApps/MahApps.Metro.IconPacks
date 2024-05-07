using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace MahApps.Metro.IconPacks
{
    /// <summary>
    /// All icons sourced from Phosphor Icons - <see><cref>https://phosphoricons.com/</cref></see>
    /// Phosphor Icons are licensed under the MIT License <see><cref>https://github.com/phosphor-icons/core?tab=MIT-1-ov-file#readme</cref></see>
    /// </summary>
    [MetaData("Phosphor Icons", "https://phosphoricons.com/", "https://github.com/phosphor-icons/core?tab=MIT-1-ov-file#readme")]
    public class PathIconPhosphorIcons : PathIconControlBase
    {
        public static readonly DependencyProperty KindProperty
            = DependencyProperty.Register(nameof(Kind), typeof(PackIconPhosphorIconsKind), typeof(PathIconPhosphorIcons), new PropertyMetadata(default(PackIconPhosphorIconsKind), KindPropertyChangedCallback));

        private static void KindPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != e.OldValue)
            {
                ((PathIconPhosphorIcons)dependencyObject).UpdateData();
            }
        }

        /// <summary>
        /// Gets or sets the icon to display.
        /// </summary>
        public PackIconPhosphorIconsKind Kind
        {
            get { return (PackIconPhosphorIconsKind)GetValue(KindProperty); }
            set { SetValue(KindProperty, value); }
        }

        public PathIconPhosphorIcons()
        {
        }

        protected override void UpdateData()
        {
            string data = null;
            PackIconPhosphorIconsDataFactory.DataIndex.Value?.TryGetValue(Kind, out data);
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