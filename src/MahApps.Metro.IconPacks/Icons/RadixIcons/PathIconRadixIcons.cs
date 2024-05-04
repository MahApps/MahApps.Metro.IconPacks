﻿using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace MahApps.Metro.IconPacks
{
    /// <summary>
    /// RadixIcons licensed under the MIT License <see><cref>https://github.com/radix-ui/icons?tab=MIT-1-ov-file#readme</cref></see>
    /// Contributions, corrections and requests can be made on GitHub <see><cref>https://github.com/radix-ui/icons</cref></see>.
    /// </summary>
    [MetaData("Radix Icons", "https://github.com/radix-ui/icons", "https://github.com/radix-ui/icons?tab=MIT-1-ov-file#readme")]
    public class PathIconRadixIcons : PathIconControlBase
    {
        public static readonly DependencyProperty KindProperty
            = DependencyProperty.Register(nameof(Kind), typeof(PackIconRadixIconsKind), typeof(PathIconRadixIcons), new PropertyMetadata(default(PackIconRadixIconsKind), KindPropertyChangedCallback));

        private static void KindPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != e.OldValue)
            {
                ((PathIconRadixIcons)dependencyObject).UpdateData();
            }
        }

        /// <summary>
        /// Gets or sets the icon to display.
        /// </summary>
        public PackIconRadixIconsKind Kind
        {
            get { return (PackIconRadixIconsKind)GetValue(KindProperty); }
            set { SetValue(KindProperty, value); }
        }

        public PathIconRadixIcons()
        {
        }

        protected override void UpdateData()
        {
            string data = null;
            PackIconRadixIconsDataFactory.DataIndex.Value?.TryGetValue(Kind, out data);
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