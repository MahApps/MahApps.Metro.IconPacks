using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace MahApps.Metro.IconPacks
{
    /// <summary>
    /// The PICOL icons are licensed under [Artistic License 2.0, Attribution 3.0 Unported (CC BY 3.0)](<see><cref>https://creativecommons.org/licenses/by/3.0/</cref></see>).
    /// Contributions, corrections and requests can be made on GitHub <see><cref>https://github.com/PicolSigns/Icons</cref></see>.
    /// </summary>
    [MetaData("PICOL Icons", "https://github.com/PicolSigns/Icons", "https://github.com/PicolSigns/Icons/blob/master/LICENSE")]
    public class PathIconPicolIcons : PathIconControlBase
    {
        public static readonly DependencyProperty KindProperty
            = DependencyProperty.Register(nameof(Kind), typeof(PackIconPicolIconsKind), typeof(PathIconPicolIcons), new PropertyMetadata(default(PackIconPicolIconsKind), KindPropertyChangedCallback));

        private static void KindPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != e.OldValue)
            {
                ((PathIconPicolIcons)dependencyObject).UpdateData();
            }
        }

        /// <summary>
        /// Gets or sets the icon to display.
        /// </summary>
        public PackIconPicolIconsKind Kind
        {
            get { return (PackIconPicolIconsKind)GetValue(KindProperty); }
            set { SetValue(KindProperty, value); }
        }

        public PathIconPicolIcons()
        {
        }

        protected override void UpdateData()
        {
            string data = null;
            PackIconPicolIconsDataFactory.DataIndex.Value?.TryGetValue(Kind, out data);
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