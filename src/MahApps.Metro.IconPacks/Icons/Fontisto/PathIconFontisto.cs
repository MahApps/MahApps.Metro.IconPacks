using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace MahApps.Metro.IconPacks
{
    /// <summary>
    /// The Fontisto font is licensed under the [SIL OFL 1.1](<see><cref>https://github.com/kenangundogan/fontisto#license</cref></see>).
    /// Contributions, corrections and requests can be made on GitHub <see><cref>https://fontisto.com/</cref></see>.
    /// </summary>
    [MetaData("Fontisto", "https://fontisto.com/", "https://github.com/kenangundogan/fontisto#license")]
    public class PathIconFontisto : PathIconControlBase
    {
        public static readonly DependencyProperty KindProperty
            = DependencyProperty.Register(nameof(Kind), typeof(PackIconFontistoKind), typeof(PathIconFontisto), new PropertyMetadata(default(PackIconFontistoKind), KindPropertyChangedCallback));

        private static void KindPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != e.OldValue)
            {
                ((PathIconFontisto)dependencyObject).UpdateData();
            }
        }

        /// <summary>
        /// Gets or sets the icon to display.
        /// </summary>
        public PackIconFontistoKind Kind
        {
            get { return (PackIconFontistoKind)GetValue(KindProperty); }
            set { SetValue(KindProperty, value); }
        }

        public PathIconFontisto()
        {
            var transformGroup = this.RenderTransform as TransformGroup ?? new TransformGroup();
            var scaleTransform = new ScaleTransform() {ScaleY = -1};
            transformGroup.Children.Insert(0, scaleTransform);
        }

        protected override void UpdateData()
        {
            string data = null;
            PackIconFontistoDataFactory.DataIndex.Value?.TryGetValue(Kind, out data);
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