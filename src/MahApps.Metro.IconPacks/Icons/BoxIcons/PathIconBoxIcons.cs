using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace MahApps.Metro.IconPacks
{
    /// <summary>
    /// BoxIcons licensed under [SIL OFL 1.1](<see><cref>http://scripts.sil.org/OFL</cref></see>)
    /// Contributions, corrections and requests can be made on GitHub <see><cref>https://github.com/atisawd/boxicons</cref></see>.
    /// </summary>
    [MetaData("Boxicons", "https://boxicons.com/", "https://boxicons.com/usage/#license")]
    public class PathIconBoxIcons : PathIconControlBase
    {
        public static readonly DependencyProperty KindProperty
            = DependencyProperty.Register(nameof(Kind), typeof(PackIconBoxIconsKind), typeof(PathIconBoxIcons), new PropertyMetadata(default(PackIconBoxIconsKind), KindPropertyChangedCallback));

        private static void KindPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != e.OldValue)
            {
                ((PathIconBoxIcons)dependencyObject).UpdateData();
            }
        }

        /// <summary>
        /// Gets or sets the icon to display.
        /// </summary>
        public PackIconBoxIconsKind Kind
        {
            get { return (PackIconBoxIconsKind)GetValue(KindProperty); }
            set { SetValue(KindProperty, value); }
        }

        public PathIconBoxIcons()
        {
            var transformGroup = this.RenderTransform as TransformGroup ?? new TransformGroup();
            var scaleTransform = new ScaleTransform() {ScaleY = -1};
            transformGroup.Children.Insert(0, scaleTransform);
        }

        protected override void UpdateData()
        {
            string data = null;
            PackIconBoxIconsDataFactory.DataIndex.Value?.TryGetValue(Kind, out data);
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