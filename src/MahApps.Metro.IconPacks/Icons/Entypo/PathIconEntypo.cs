using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace MahApps.Metro.IconPacks
{
    /// <summary>
    /// All icons sourced from Entypo+ Icons Font <see><cref>http://www.entypo.com</cref></see>
    /// Licensed under [CC BY 4.0](<see><cref>http://creativecommons.org/licenses/by-sa/4.0/</cref></see>).
    /// </summary>
    [MetaData("Entypo+", "http://www.entypo.com/", "https://creativecommons.org/licenses/by-sa/4.0/")]
    public class PathIconEntypo : PathIconControlBase
    {
        public static readonly DependencyProperty KindProperty
            = DependencyProperty.Register(nameof(Kind), typeof(PackIconEntypoKind), typeof(PathIconEntypo), new PropertyMetadata(default(PackIconEntypoKind), KindPropertyChangedCallback));

        private static void KindPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != e.OldValue)
            {
                ((PathIconEntypo)dependencyObject).UpdateData();
            }
        }

        /// <summary>
        /// Gets or sets the icon to display.
        /// </summary>
        public PackIconEntypoKind Kind
        {
            get { return (PackIconEntypoKind)GetValue(KindProperty); }
            set { SetValue(KindProperty, value); }
        }

        public PathIconEntypo()
        {
        }

        protected override void UpdateData()
        {
            string data = null;
            PackIconEntypoDataFactory.DataIndex.Value?.TryGetValue(Kind, out data);
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