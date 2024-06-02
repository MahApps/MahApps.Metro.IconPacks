﻿using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace MahApps.Metro.IconPacks
{
    /// <summary>
    /// All icons sourced from Material Design Icons Font <see><cref>https://pictogrammers.com/library/mdi/</cref></see>
    /// In accordance of <see><cref>https://github.com/Templarian/MaterialDesign?tab=License-1-ov-file#readme</cref></see>.
    /// </summary>
    [MetaData("Material Design Icons", "https://pictogrammers.com/library/mdi/", "https://github.com/Templarian/MaterialDesign?tab=License-1-ov-file#readme")]
    public class PathIconMaterial : PathIconControlBase
    {
        public static readonly DependencyProperty KindProperty
            = DependencyProperty.Register(nameof(Kind), typeof(PackIconMaterialKind), typeof(PathIconMaterial), new PropertyMetadata(default(PackIconMaterialKind), KindPropertyChangedCallback));

        private static void KindPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != e.OldValue)
            {
                ((PathIconMaterial)dependencyObject).UpdateData();
            }
        }

        /// <summary>
        /// Gets or sets the icon to display.
        /// </summary>
        public PackIconMaterialKind Kind
        {
            get { return (PackIconMaterialKind)GetValue(KindProperty); }
            set { SetValue(KindProperty, value); }
        }

        public PathIconMaterial()
        {
        }

        protected override void UpdateData()
        {
            string data = null;
            PackIconMaterialDataFactory.DataIndex.Value?.TryGetValue(Kind, out data);
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