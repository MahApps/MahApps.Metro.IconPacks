using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace MahApps.Metro.IconPacks
{
    /// <summary>
    /// All icons sourced from GitHub Octicons - <see><cref>https://octicons.github.com</cref></see> - 
    /// <see><cref>https://github.com/primer/octicons/blob/master/LICENSE</cref></see>.
    /// </summary>
    public class PathIconOcticons : PathIconControlBase
    {
        private static Lazy<IDictionary<PackIconOcticonsKind, string>> _dataIndex;

        public static readonly DependencyProperty KindProperty
            = DependencyProperty.Register(nameof(Kind), typeof(PackIconOcticonsKind), typeof(PathIconOcticons), new PropertyMetadata(default(PackIconOcticonsKind), KindPropertyChangedCallback));

        private static void KindPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != e.OldValue)
            {
                ((PathIconOcticons)dependencyObject).UpdateData();
            }
        }

        /// <summary>
        /// Gets or sets the icon to display.
        /// </summary>
        public PackIconOcticonsKind Kind
        {
            get { return (PackIconOcticonsKind)GetValue(KindProperty); }
            set { SetValue(KindProperty, value); }
        }

        public PathIconOcticons()
        {
            if (_dataIndex == null)
            {
                _dataIndex = new Lazy<IDictionary<PackIconOcticonsKind, string>>(PackIconOcticonsDataFactory.Create);
            }
        }

        protected override void UpdateData()
        {
            string data = null;
            _dataIndex.Value?.TryGetValue(Kind, out data);
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