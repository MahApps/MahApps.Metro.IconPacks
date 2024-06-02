using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace MahApps.Metro.IconPacks
{
    /// <summary>
    /// Fontaudio by @fefanto <see><cref>https://github.com/fefanto</cref></see>. License: Icons: CC BY 4.0, Fonts: SIL OFL 1.1, Code: MIT License. <see><cref>https://github.com/fefanto/fontaudio#license</cref></see>
    /// Contributions, corrections and requests can be made on GitHub <see><cref>https://github.com/fefanto/fontaudio</cref></see>.
    /// </summary>
    [MetaData("Fontaudio", "https://github.com/fefanto/fontaudio", "https://github.com/fefanto/fontaudio#license")]
    public class PathIconFontaudio : PathIconControlBase
    {
        public static readonly DependencyProperty KindProperty
            = DependencyProperty.Register(nameof(Kind), typeof(PackIconFontaudioKind), typeof(PathIconFontaudio), new PropertyMetadata(default(PackIconFontaudioKind), KindPropertyChangedCallback));

        private static void KindPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != e.OldValue)
            {
                ((PathIconFontaudio)dependencyObject).UpdateData();
            }
        }

        /// <summary>
        /// Gets or sets the icon to display.
        /// </summary>
        public PackIconFontaudioKind Kind
        {
            get { return (PackIconFontaudioKind)GetValue(KindProperty); }
            set { SetValue(KindProperty, value); }
        }

        public PathIconFontaudio()
        {
            var transformGroup = this.RenderTransform as TransformGroup ?? new TransformGroup();
            var scaleTransform = new ScaleTransform() {ScaleY = -1};
            transformGroup.Children.Insert(0, scaleTransform);
        }

        protected override void UpdateData()
        {
            string data = null;
            PackIconFontaudioDataFactory.DataIndex.Value?.TryGetValue(Kind, out data);
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