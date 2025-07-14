using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace MahApps.Metro.IconPacks
{
    /// <summary>
    /// MynaUI Icons are licensed under [MIT license](<see><cref>https://github.com/praveenjuge/mynaui-icons?tab=MIT-1-ov-file</cref></see>).
    /// Contributions, corrections and requests can be made on GitHub <see><cref>https://github.com/praveenjuge/mynaui-icons</cref></see>.
    /// </summary>
    [MetaData("MynaUI Icons", "https://github.com/praveenjuge/mynaui-icons", "https://github.com/praveenjuge/mynaui-icons?tab=MIT-1-ov-file")]
    public class PathIconMynaUIIcons : PathIconControlBase
    {
        public static readonly DependencyProperty KindProperty
            = DependencyProperty.Register(nameof(Kind), typeof(PackIconMynaUIIconsKind), typeof(PathIconMynaUIIcons), new PropertyMetadata(default(PackIconMynaUIIconsKind), KindPropertyChangedCallback));

        private static void KindPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != e.OldValue)
            {
                ((PathIconMynaUIIcons)dependencyObject).UpdateData();
            }
        }

        /// <summary>
        /// Gets or sets the icon to display.
        /// </summary>
        public PackIconMynaUIIconsKind Kind
        {
            get { return (PackIconMynaUIIconsKind)GetValue(KindProperty); }
            set { SetValue(KindProperty, value); }
        }

        public PathIconMynaUIIcons()
        {
            var transformGroup = this.RenderTransform as TransformGroup ?? new TransformGroup();
            var scaleTransform = new ScaleTransform() {ScaleY = -1};
            transformGroup.Children.Insert(0, scaleTransform);
        }

        protected override void UpdateData()
        {
            string data = null;
            PackIconDataFactory<PackIconMynaUIIconsKind>.DataIndex.Value?.TryGetValue(Kind, out data);
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