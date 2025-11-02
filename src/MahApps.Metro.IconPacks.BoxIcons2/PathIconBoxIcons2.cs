using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace MahApps.Metro.IconPacks
{
    /// <summary>
    /// BoxIcons licensed under [MIT](<see><cref>https://v2.boxicons.com/usage#license</cref></see>)
    /// Contributions, corrections and requests can be made on GitHub <see><cref>https://github.com/atisawd/boxicons</cref></see>.
    /// </summary>
    [MetaData("Boxicons v2", "https://v2.boxicons.com/", "https://v2.boxicons.com/usage#license")]
    public class PathIconBoxIcons2 : PathIconControlBase
    {
        public static readonly DependencyProperty KindProperty
            = DependencyProperty.Register(nameof(Kind), typeof(PackIconBoxIcons2Kind), typeof(PathIconBoxIcons2), new PropertyMetadata(default(PackIconBoxIcons2Kind), KindPropertyChangedCallback));

        private static void KindPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != e.OldValue)
            {
                ((PathIconBoxIcons2)dependencyObject).UpdateData();
            }
        }

        /// <summary>
        /// Gets or sets the icon to display.
        /// </summary>
        public PackIconBoxIcons2Kind Kind
        {
            get { return (PackIconBoxIcons2Kind)GetValue(KindProperty); }
            set { SetValue(KindProperty, value); }
        }

        public PathIconBoxIcons2()
        {
        }

        protected override void UpdateData()
        {
            string data = null;
            PackIconDataFactory<PackIconBoxIcons2Kind>.DataIndex.Value?.TryGetValue(Kind, out data);
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