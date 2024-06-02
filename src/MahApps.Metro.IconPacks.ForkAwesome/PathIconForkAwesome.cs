using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace MahApps.Metro.IconPacks
{
    /// <summary>
    /// The Fork Awesome font is licensed under the SIL OFL 1.1 (<see><cref>http://scripts.sil.org/OFL</cref></see>). Fork Awesome is a fork based of off Font Awesome 4.7.0 by Dave Gandy.
    /// More info on licenses at <see><cref>https://forkawesome.github.io</cref></see>. Contributions, corrections and requests can be made on GitHub <see><cref>https://github.com/ForkAwesome/Fork-Awesome</cref></see>.
    /// </summary>
    [MetaData("Fork Awesome", "https://forkaweso.me/", "https://github.com/ForkAwesome/Fork-Awesome/blob/master/LICENSES")]
    public class PathIconForkAwesome : PathIconControlBase
    {
        public static readonly DependencyProperty KindProperty
            = DependencyProperty.Register(nameof(Kind), typeof(PackIconForkAwesomeKind), typeof(PathIconForkAwesome), new PropertyMetadata(default(PackIconForkAwesomeKind), KindPropertyChangedCallback));

        private static void KindPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != e.OldValue)
            {
                ((PathIconForkAwesome)dependencyObject).UpdateData();
            }
        }

        /// <summary>
        /// Gets or sets the icon to display.
        /// </summary>
        public PackIconForkAwesomeKind Kind
        {
            get { return (PackIconForkAwesomeKind)GetValue(KindProperty); }
            set { SetValue(KindProperty, value); }
        }

        public PathIconForkAwesome()
        {
            var transformGroup = this.RenderTransform as TransformGroup ?? new TransformGroup();
            var scaleTransform = new ScaleTransform() {ScaleY = -1};
            transformGroup.Children.Insert(0, scaleTransform);
        }

        protected override void UpdateData()
        {
            string data = null;
            PackIconForkAwesomeDataFactory.DataIndex.Value?.TryGetValue(Kind, out data);
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