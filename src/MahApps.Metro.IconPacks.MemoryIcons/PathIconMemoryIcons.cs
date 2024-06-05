using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace MahApps.Metro.IconPacks
{
    /// <summary>
    /// All icons sourced from Memory Icons <see><cref>https://pictogrammers.com/library/memory/</cref></see>
    /// In accordance of <see><cref>https://github.com/Pictogrammers/Memory?tab=License-1-ov-file#readme</cref></see>.
    /// </summary>
    [MetaData("Memory Icons", "https://pictogrammers.com/library/memory/", "https://github.com/Pictogrammers/Memory?tab=License-1-ov-file#readme")]
    public class PathIconMemoryIcons : PathIconControlBase
    {
        public static readonly DependencyProperty KindProperty
            = DependencyProperty.Register(nameof(Kind), typeof(PackIconMemoryIconsKind), typeof(PathIconMemoryIcons), new PropertyMetadata(default(PackIconMemoryIconsKind), KindPropertyChangedCallback));

        private static void KindPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != e.OldValue)
            {
                ((PathIconMemoryIcons)dependencyObject).UpdateData();
            }
        }

        /// <summary>
        /// Gets or sets the icon to display.
        /// </summary>
        public PackIconMemoryIconsKind Kind
        {
            get { return (PackIconMemoryIconsKind)GetValue(KindProperty); }
            set { SetValue(KindProperty, value); }
        }

        public PathIconMemoryIcons()
        {
        }

        protected override void UpdateData()
        {
            string data = null;
            PackIconDataFactory<PackIconMemoryIconsKind>.DataIndex.Value?.TryGetValue(Kind, out data);
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