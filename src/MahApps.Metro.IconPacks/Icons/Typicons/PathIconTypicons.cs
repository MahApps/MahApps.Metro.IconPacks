using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace MahApps.Metro.IconPacks
{
    /// <summary>
    /// Typicons Icons can be found at <see><cref>https://www.s-ings.com/typicons/</cref></see>.
    /// Font files are available under the SIL Open Font Licence. Artwork available under the CC BY-SA Licence. [License](<see><cref>https://github.com/stephenhutchings/typicons.font/blob/master/LICENCE.md</cref></see>).
    /// </summary>
    [MetaData("Typicons", "https://www.s-ings.com/typicons/", "https://github.com/stephenhutchings/typicons.font/blob/master/LICENCE.md")]
    public class PathIconTypicons : PathIconControlBase
    {
        public static readonly DependencyProperty KindProperty
            = DependencyProperty.Register(nameof(Kind), typeof(PackIconTypiconsKind), typeof(PathIconTypicons), new PropertyMetadata(default(PackIconTypiconsKind), KindPropertyChangedCallback));

        private static void KindPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != e.OldValue)
            {
                ((PathIconTypicons)dependencyObject).UpdateData();
            }
        }

        /// <summary>
        /// Gets or sets the icon to display.
        /// </summary>
        public PackIconTypiconsKind Kind
        {
            get { return (PackIconTypiconsKind)GetValue(KindProperty); }
            set { SetValue(KindProperty, value); }
        }

        public PathIconTypicons()
        {
            var transformGroup = this.RenderTransform as TransformGroup ?? new TransformGroup();
            var scaleTransform = new ScaleTransform() {ScaleY = -1};
            transformGroup.Children.Insert(0, scaleTransform);
        }

        protected override void UpdateData()
        {
            string data = null;
            PackIconTypiconsDataFactory.DataIndex.Value?.TryGetValue(Kind, out data);
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