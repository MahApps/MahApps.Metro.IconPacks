using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace MahApps.Metro.IconPacks
{
    /// <summary>
    /// All icons sourced from Google Material Design icon font - <see><cref>https://github.com/google/material-design-icons</cref></see>
    /// Google Material Design are licensed under the Apache License 2.0 <see><cref>https://github.com/google/material-design-icons?tab=Apache-2.0-1-ov-file#readme</cref></see>
    /// </summary>
    [MetaData("Material Icons (Google)", "https://github.com/google/material-design-icons", "https://github.com/google/material-design-icons?tab=Apache-2.0-1-ov-file#readme")]
    public class PathIconMaterialDesign : PathIconControlBase
    {
        public static readonly DependencyProperty KindProperty
            = DependencyProperty.Register(nameof(Kind), typeof(PackIconMaterialDesignKind), typeof(PathIconMaterialDesign), new PropertyMetadata(default(PackIconMaterialDesignKind), KindPropertyChangedCallback));

        private static void KindPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != e.OldValue)
            {
                ((PathIconMaterialDesign)dependencyObject).UpdateData();
            }
        }

        /// <summary>
        /// Gets or sets the icon to display.
        /// </summary>
        public PackIconMaterialDesignKind Kind
        {
            get { return (PackIconMaterialDesignKind)GetValue(KindProperty); }
            set { SetValue(KindProperty, value); }
        }

        public PathIconMaterialDesign()
        {
        }

        protected override void UpdateData()
        {
            string data = null;
            PackIconMaterialDesignDataFactory.DataIndex.Value?.TryGetValue(Kind, out data);
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