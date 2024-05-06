using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace MahApps.Metro.IconPacks
{
    /// <summary>
    /// All icons sourced from Game Icons <see><cref>https://github.com/game-icons/icons</cref></see>
    /// In accordance of <see><cref>https://github.com/game-icons/icons?tab=License-1-ov-file#readme</cref></see>.
    /// </summary>
    [MetaData("Game Icons", "https://github.com/game-icons/icons", "https://github.com/game-icons/icons?tab=License-1-ov-file#readme")]
    public class PathIconGameIcons : PathIconControlBase
    {
        public static readonly DependencyProperty KindProperty
            = DependencyProperty.Register(nameof(Kind), typeof(PackIconGameIconsKind), typeof(PathIconGameIcons), new PropertyMetadata(default(PackIconGameIconsKind), KindPropertyChangedCallback));

        private static void KindPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != e.OldValue)
            {
                ((PathIconGameIcons)dependencyObject).UpdateData();
            }
        }

        /// <summary>
        /// Gets or sets the icon to display.
        /// </summary>
        public PackIconGameIconsKind Kind
        {
            get { return (PackIconGameIconsKind)GetValue(KindProperty); }
            set { SetValue(KindProperty, value); }
        }

        public PathIconGameIcons()
        {
        }

        protected override void UpdateData()
        {
            string data = null;
            PackIconGameIconsDataFactory.DataIndex.Value?.TryGetValue(Kind, out data);
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