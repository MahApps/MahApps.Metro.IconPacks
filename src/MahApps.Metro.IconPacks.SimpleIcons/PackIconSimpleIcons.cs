﻿using System;
using System.Collections.Generic;
#if (NETFX_CORE || WINDOWS_UWP)
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
#else
using System.Windows;
#endif

namespace MahApps.Metro.IconPacks
{
    /// <summary>
    /// Simple Icons licensed under <see><cref>https://github.com/simple-icons/simple-icons?tab=CC0-1.0-1-ov-file#readme</cref></see>. Please read the legal disclaimer <see><cref>https://github.com/simple-icons/simple-icons/blob/master/DISCLAIMER.md</cref></see>.
    /// Contributions, corrections and requests can be made on GitHub <see><cref>https://github.com/simple-icons/simple-icons</cref></see>.
    /// </summary>
    [MetaData("Simple Icons", "https://github.com/simple-icons/simple-icons", "https://github.com/simple-icons/simple-icons?tab=CC0-1.0-1-ov-file#readme")]
    public class PackIconSimpleIcons : PackIconControlBase
    {
        public static readonly DependencyProperty KindProperty
            = DependencyProperty.Register(nameof(Kind), typeof(PackIconSimpleIconsKind), typeof(PackIconSimpleIcons), new PropertyMetadata(default(PackIconSimpleIconsKind), KindPropertyChangedCallback));

        private static void KindPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != e.OldValue)
            {
                ((PackIconSimpleIcons)dependencyObject).UpdateData();
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

#if !(NETFX_CORE || WINDOWS_UWP)
        static PackIconSimpleIcons()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PackIconSimpleIcons), new FrameworkPropertyMetadata(typeof(PackIconSimpleIcons)));
        }
#endif

        public PackIconSimpleIcons()
        {
#if NETFX_CORE || WINDOWS_UWP
            this.DefaultStyleKey = typeof(PackIconSimpleIcons);
#endif
        }

        protected override void SetKind<TKind>(TKind iconKind)
        {
#if NETFX_CORE || WINDOWS_UWP
            BindingOperations.SetBinding(this, PackIconSimpleIcons.KindProperty, new Binding() { Source = iconKind, Mode = BindingMode.OneTime });
#else
            this.SetCurrentValue(KindProperty, iconKind);
#endif
        }

        protected override void UpdateData()
        {
            if (Kind != default(PackIconSimpleIconsKind))
            {
                string data = null;
                PackIconSimpleIconsDataFactory.DataIndex.Value?.TryGetValue(Kind, out data);
                this.Data = data;
            }
            else
            {
                this.Data = null;
            }
        }
    }
}