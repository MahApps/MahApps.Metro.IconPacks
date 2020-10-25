using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace MahApps.Metro.IconPacks
{
    /// <summary>
    /// The Microns icons are licensed under [CC BY-SA](<see><cref>https://creativecommons.org/licenses/by-sa/3.0/</cref></see>) license.
    /// Contributions, corrections and requests can be made on GitHub <see><cref>https://github.com/stephenhutchings/microns</cref></see>.
    /// </summary>
    [MetaData("Microns", "https://www.s-ings.com/projects/microns-icon-font/", "https://github.com/stephenhutchings/microns/blob/master/LICENCE.md")]
    public class PathIconMicrons : PathIconControlBase
    {
        public static readonly DependencyProperty KindProperty
            = DependencyProperty.Register(nameof(Kind), typeof(PackIconMicronsKind), typeof(PathIconMicrons), new PropertyMetadata(default(PackIconMicronsKind), KindPropertyChangedCallback));

        private static void KindPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != e.OldValue)
            {
                ((PathIconMicrons)dependencyObject).UpdateData();
            }
        }

        /// <summary>
        /// Gets or sets the icon to display.
        /// </summary>
        public PackIconMicronsKind Kind
        {
            get { return (PackIconMicronsKind)GetValue(KindProperty); }
            set { SetValue(KindProperty, value); }
        }

        public PathIconMicrons()
        {
        }

        protected override void UpdateData()
        {
            string data = null;
            PackIconMicronsDataFactory.DataIndex.Value?.TryGetValue(Kind, out data);
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