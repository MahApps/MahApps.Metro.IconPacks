using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace MahApps.Metro.IconPacks
{
    /// <summary>
    /// All icons sourced from GitHub <see><cref>https://github.com/andrewgioia/keyrune</cref></see>
    /// In accordance of <see><cref>https://github.com/andrewgioia/keyrune?tab=License-1-ov-file</cref></see>.
    /// </summary>
    [MetaData("Keyrune Icons", "https://github.com/andrewgioia/keyrune", "https://github.com/andrewgioia/keyrune?tab=License-1-ov-file")]
    public class PathIconKeyruneIcons : PathIconControlBase
    {
        public static readonly DependencyProperty KindProperty
            = DependencyProperty.Register(nameof(Kind), typeof(PackIconKeyruneIconsKind), typeof(PathIconKeyruneIcons), new PropertyMetadata(default(PackIconKeyruneIconsKind), KindPropertyChangedCallback));

        private static void KindPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != e.OldValue)
            {
                ((PathIconKeyruneIcons)dependencyObject).UpdateData();
            }
        }

        /// <summary>
        /// Gets or sets the icon to display.
        /// </summary>
        public PackIconKeyruneIconsKind Kind
        {
            get { return (PackIconKeyruneIconsKind)GetValue(KindProperty); }
            set { SetValue(KindProperty, value); }
        }

        public PathIconKeyruneIcons()
        {
        }

        protected override void UpdateData()
        {
            string data = null;
            PackIconDataFactory<PackIconKeyruneIconsKind>.DataIndex.Value?.TryGetValue(Kind, out data);
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