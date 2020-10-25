using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace MahApps.Metro.IconPacks
{
    /// <summary>
    /// Bootstrap Icons are licensed under the [MIT license](<see><cref>https://github.com/twbs/icons/blob/main/LICENSE.md</cref></see>).
    /// Contributions, corrections and requests can be made on GitHub <see><cref>https://github.com/twbs/icons</cref></see>.
    /// </summary>
    [MetaData("Bootstrap Icons", "https://icons.getbootstrap.com/", "https://github.com/twbs/icons/blob/main/LICENSE.md")]
    public class PathIconBootstrapIcons : PathIconControlBase
    {
        public static readonly DependencyProperty KindProperty
            = DependencyProperty.Register(nameof(Kind), typeof(PackIconBootstrapIconsKind), typeof(PathIconBootstrapIcons), new PropertyMetadata(default(PackIconBootstrapIconsKind), KindPropertyChangedCallback));

        private static void KindPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != e.OldValue)
            {
                ((PathIconBootstrapIcons)dependencyObject).UpdateData();
            }
        }

        /// <summary>
        /// Gets or sets the icon to display.
        /// </summary>
        public PackIconBootstrapIconsKind Kind
        {
            get { return (PackIconBootstrapIconsKind)GetValue(KindProperty); }
            set { SetValue(KindProperty, value); }
        }

        public PathIconBootstrapIcons()
        {
            var transformGroup = this.RenderTransform as TransformGroup ?? new TransformGroup();
            var scaleTransform = new ScaleTransform() {ScaleY = -1};
            transformGroup.Children.Insert(0, scaleTransform);
        }

        protected override void UpdateData()
        {
            string data = null;
            PackIconBootstrapIconsDataFactory.DataIndex.Value?.TryGetValue(Kind, out data);
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