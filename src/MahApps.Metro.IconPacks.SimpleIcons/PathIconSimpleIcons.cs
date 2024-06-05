﻿using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace MahApps.Metro.IconPacks
{
    /// <summary>
    /// Simple Icons licensed under <see><cref>https://github.com/simple-icons/simple-icons?tab=CC0-1.0-1-ov-file#readme</cref></see>. Please read the legal disclaimer <see><cref>https://github.com/simple-icons/simple-icons/blob/master/DISCLAIMER.md</cref></see>.
    /// Contributions, corrections and requests can be made on GitHub <see><cref>https://github.com/simple-icons/simple-icons</cref></see>.
    /// </summary>
    [MetaData("Simple Icons", "https://github.com/simple-icons/simple-icons", "https://github.com/simple-icons/simple-icons?tab=CC0-1.0-1-ov-file#readme")]
    public class PathIconSimpleIcons : PathIconControlBase
    {
        public static readonly DependencyProperty KindProperty
            = DependencyProperty.Register(nameof(Kind), typeof(PackIconSimpleIconsKind), typeof(PathIconSimpleIcons), new PropertyMetadata(default(PackIconSimpleIconsKind), KindPropertyChangedCallback));

        private static void KindPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != e.OldValue)
            {
                ((PathIconSimpleIcons)dependencyObject).UpdateData();
            }
        }

        /// <summary>
        /// Gets or sets the icon to display.
        /// </summary>
        public PackIconSimpleIconsKind Kind
        {
            get { return (PackIconSimpleIconsKind)GetValue(KindProperty); }
            set { SetValue(KindProperty, value); }
        }

        public PathIconSimpleIcons()
        {
        }

        protected override void UpdateData()
        {
            string data = null;
            PackIconDataFactory<PackIconSimpleIconsKind>.DataIndex.Value?.TryGetValue(Kind, out data);
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