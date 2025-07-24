using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace MahApps.Metro.IconPacks
{
    /// <summary>
    /// MingCute Icon are licensed under [Apache-2.0 license](<see><cref>https://github.com/Richard9394/MingCute?tab=Apache-2.0-1-ov-file</cref></see>).
    /// Contributions, corrections and requests can be made on GitHub <see><cref>https://github.com/Richard9394/MingCute</cref></see>.
    /// </summary>
    [MetaData("MingCute Icon", "https://github.com/Richard9394/MingCute", "https://github.com/Richard9394/MingCute?tab=Apache-2.0-1-ov-file")]
    public class PathIconMingCuteIcons : PathIconControlBase
    {
        public static readonly DependencyProperty KindProperty
            = DependencyProperty.Register(nameof(Kind), typeof(PackIconMingCuteIconsKind), typeof(PathIconMingCuteIcons), new PropertyMetadata(default(PackIconMingCuteIconsKind), KindPropertyChangedCallback));

        private static void KindPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != e.OldValue)
            {
                ((PathIconMingCuteIcons)dependencyObject).UpdateData();
            }
        }

        /// <summary>
        /// Gets or sets the icon to display.
        /// </summary>
        public PackIconMingCuteIconsKind Kind
        {
            get { return (PackIconMingCuteIconsKind)GetValue(KindProperty); }
            set { SetValue(KindProperty, value); }
        }

        public PathIconMingCuteIcons()
        {
            var transformGroup = this.RenderTransform as TransformGroup ?? new TransformGroup();
            var scaleTransform = new ScaleTransform() {ScaleY = -1};
            transformGroup.Children.Insert(0, scaleTransform);
        }

        protected override void UpdateData()
        {
            string data = null;
            PackIconDataFactory<PackIconMingCuteIconsKind>.DataIndex.Value?.TryGetValue(Kind, out data);
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