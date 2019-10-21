using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace MahApps.Metro.IconPacks
{
    /// <summary>
    /// Unicons are Open Source icons and licensed under [Apache 2.0](<see><cref>https://www.apache.org/licenses/LICENSE-2.0.txt</cref></see>). You're free to use these icons in your personal and commercial project. We would love to see the attribution in your app's **about** screen, but it's not mandatory.
    /// Contributions, corrections and requests can be made on GitHub <see><cref>https://github.com/Iconscout/unicons</cref></see>.
    /// </summary>
    public class PathIconUnicons : PathIconControlBase
    {
        private static Lazy<IDictionary<PackIconUniconsKind, string>> _dataIndex;

        public static readonly DependencyProperty KindProperty
            = DependencyProperty.Register(nameof(Kind), typeof(PackIconUniconsKind), typeof(PathIconUnicons), new PropertyMetadata(default(PackIconUniconsKind), KindPropertyChangedCallback));

        private static void KindPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != e.OldValue)
            {
                ((PathIconUnicons)dependencyObject).UpdateData();
            }
        }

        /// <summary>
        /// Gets or sets the icon to display.
        /// </summary>
        public PackIconUniconsKind Kind
        {
            get { return (PackIconUniconsKind)GetValue(KindProperty); }
            set { SetValue(KindProperty, value); }
        }

        public PathIconUnicons()
        {
            if (_dataIndex == null)
            {
                _dataIndex = new Lazy<IDictionary<PackIconUniconsKind, string>>(PackIconUniconsDataFactory.Create);
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