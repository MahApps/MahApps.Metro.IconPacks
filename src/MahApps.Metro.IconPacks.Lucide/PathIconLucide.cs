using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace MahApps.Metro.IconPacks
{
    /// <summary>
    /// Lucide Icons are licensed under [ISC License](<see><cref>https://github.com/lucide-icons/lucide?tab=ISC-1-ov-file#readme</cref></see>).
    /// Contributions, corrections and requests can be made on GitHub <see><cref>https://github.com/lucide-icons/lucide</cref></see>.
    /// </summary>
    [MetaData("Lucide Icons", "https://github.com/lucide-icons/lucide", "https://github.com/lucide-icons/lucide?tab=ISC-1-ov-file#readme")]
    public class PathIconLucide : PathIconControlBase
    {
        public static readonly DependencyProperty KindProperty
            = DependencyProperty.Register(nameof(Kind), typeof(PackIconLucideKind), typeof(PathIconLucide), new PropertyMetadata(default(PackIconLucideKind), KindPropertyChangedCallback));

        private static void KindPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != e.OldValue)
            {
                ((PathIconLucide)dependencyObject).UpdateData();
            }
        }

        /// <summary>
        /// Gets or sets the icon to display.
        /// </summary>
        public PackIconLucideKind Kind
        {
            get { return (PackIconLucideKind)GetValue(KindProperty); }
            set { SetValue(KindProperty, value); }
        }

        public PathIconLucide()
        {
            var transformGroup = this.RenderTransform as TransformGroup ?? new TransformGroup();
            var scaleTransform = new ScaleTransform() {ScaleY = -1};
            transformGroup.Children.Insert(0, scaleTransform);
        }

        protected override void UpdateData()
        {
            string data = null;
            PackIconLucideDataFactory.DataIndex.Value?.TryGetValue(Kind, out data);
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