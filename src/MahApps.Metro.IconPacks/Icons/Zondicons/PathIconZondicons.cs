using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace MahApps.Metro.IconPacks
{
    /// <summary>
    /// Zondicons are licensed under the [CC BY 4.0](<see><cref>https://creativecommons.org/licenses/by/4.0/</cref></see>).
    /// Zondicons are availabe at <see><cref>https://www.zondicons.com/</cref></see>.
    /// </summary>
    [MetaData("Zondicons", "https://www.zondicons.com/", "https://creativecommons.org/licenses/by/4.0/")]
    public class PathIconZondicons : PathIconControlBase
    {
        public static readonly DependencyProperty KindProperty
            = DependencyProperty.Register(nameof(Kind), typeof(PackIconZondiconsKind), typeof(PathIconZondicons), new PropertyMetadata(default(PackIconZondiconsKind), KindPropertyChangedCallback));

        private static void KindPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != e.OldValue)
            {
                ((PathIconZondicons)dependencyObject).UpdateData();
            }
        }

        /// <summary>
        /// Gets or sets the icon to display.
        /// </summary>
        public PackIconZondiconsKind Kind
        {
            get { return (PackIconZondiconsKind)GetValue(KindProperty); }
            set { SetValue(KindProperty, value); }
        }

        public PathIconZondicons()
        {
        }

        protected override void UpdateData()
        {
            string data = null;
            PackIconZondiconsDataFactory.DataIndex.Value?.TryGetValue(Kind, out data);
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