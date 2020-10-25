using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace MahApps.Metro.IconPacks
{
    /// <summary>
    /// RemixIcon released under the [Apache License Version 2.0](<see><cref>https://github.com/Remix-Design/RemixIcon/blob/master/License</cref></see>)
    /// Contributions, corrections and requests can be made on GitHub <see><cref>https://github.com/Remix-Design/RemixIcon</cref></see>.
    /// </summary>
    [MetaData("Remix Icon", "https://remixicon.com/", "https://github.com/Remix-Design/RemixIcon/blob/master/License")]
    public class PathIconRemixIcon : PathIconControlBase
    {
        public static readonly DependencyProperty KindProperty
            = DependencyProperty.Register(nameof(Kind), typeof(PackIconRemixIconKind), typeof(PathIconRemixIcon), new PropertyMetadata(default(PackIconRemixIconKind), KindPropertyChangedCallback));

        private static void KindPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != e.OldValue)
            {
                ((PathIconRemixIcon)dependencyObject).UpdateData();
            }
        }

        /// <summary>
        /// Gets or sets the icon to display.
        /// </summary>
        public PackIconRemixIconKind Kind
        {
            get { return (PackIconRemixIconKind)GetValue(KindProperty); }
            set { SetValue(KindProperty, value); }
        }

        public PathIconRemixIcon()
        {
            var transformGroup = this.RenderTransform as TransformGroup ?? new TransformGroup();
            var scaleTransform = new ScaleTransform() {ScaleY = -1};
            transformGroup.Children.Insert(0, scaleTransform);
        }

        protected override void UpdateData()
        {
            string data = null;
            PackIconRemixIconDataFactory.DataIndex.Value?.TryGetValue(Kind, out data);
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