using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace MahApps.Metro.IconPacks
{
    /// <summary>
    /// All icons sourced from Material Design Icons Font v1.6.50 - <see><cref>https://materialdesignicons.com</cref></see> - in accordance of
    /// <see><cref>https://github.com/Templarian/MaterialDesign/blob/master/license.txt</cref></see>.
    /// </summary>
    public class PathIconMaterial : PathIconControlBase
    {
        private static Lazy<IDictionary<PackIconMaterialKind, string>> _dataIndex;

        public static readonly DependencyProperty KindProperty
            = DependencyProperty.Register(nameof(Kind), typeof(PackIconMaterialKind), typeof(PathIconMaterial), new PropertyMetadata(default(PackIconMaterialKind), KindPropertyChangedCallback));

        private static void KindPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != e.OldValue)
            {
                ((PathIconMaterial)dependencyObject).UpdateData();
            }
        }

        /// <summary>
        /// Gets or sets the icon to display.
        /// </summary>
        public PackIconMaterialKind Kind
        {
            get { return (PackIconMaterialKind)GetValue(KindProperty); }
            set { SetValue(KindProperty, value); }
        }

        public PathIconMaterial()
        {
            if (_dataIndex == null)
            {
                _dataIndex = new Lazy<IDictionary<PackIconMaterialKind, string>>(PackIconMaterialDataFactory.Create);
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