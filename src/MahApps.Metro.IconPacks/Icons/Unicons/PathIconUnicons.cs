﻿using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace MahApps.Metro.IconPacks
{
    /// <summary>
    /// Unicons are Open Source icons and licensed under [IconScout Simple License](<see><cref>https://github.com/Iconscout/unicons?tab=readme-ov-file#license</cref></see>).
    /// Contributions, corrections and requests can be made on GitHub <see><cref>https://github.com/Iconscout/unicons</cref></see>.
    /// </summary>
    [MetaData("Unicons", "https://github.com/Iconscout/unicons", "https://github.com/Iconscout/unicons?tab=readme-ov-file#license")]
    public class PathIconUnicons : PathIconControlBase
    {
        public static readonly DependencyProperty KindProperty
            = DependencyProperty.Register(nameof(Kind), typeof(PackIconUniconsKind), typeof(PathIconUnicons), new PropertyMetadata(default(PackIconUniconsKind), KindPropertyChangedCallback));

        private static void KindPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != e.OldValue)
            {
                ((PathIconUnicons)dependencyObject).UpdateData();
            }
        }

        /// <summary>
        /// Gets or sets the icon to display.
        /// </summary>
        public PackIconUniconsKind Kind
        {
            get { return (PackIconUniconsKind)GetValue(KindProperty); }
            set { SetValue(KindProperty, value); }
        }

        public PathIconUnicons()
        {
        }

        protected override void UpdateData()
        {
            string data = null;
            PackIconUniconsDataFactory.DataIndex.Value?.TryGetValue(Kind, out data);
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