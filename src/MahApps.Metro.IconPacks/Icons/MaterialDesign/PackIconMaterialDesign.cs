using System;
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
    /// All icons sourced from Google Material Design icon font - <see><cref>http://google.github.io/material-design-icons/</cref></see>
    /// Google Material Design are licensed under the Apache License 2.0 <see><cref>https://github.com/google/material-design-icons/blob/master/LICENSE</cref></see>
    /// </summary>
    [MetaData("Material design icons (Google)", "http://google.github.io/material-design-icons/", "http://google.github.io/material-design-icons/#licensing")]
    public class PackIconMaterialDesign : PackIconControlBase
    {
        public static readonly DependencyProperty KindProperty
            = DependencyProperty.Register(nameof(Kind), typeof(PackIconMaterialDesignKind), typeof(PackIconMaterialDesign), new PropertyMetadata(default(PackIconMaterialDesignKind), KindPropertyChangedCallback));

        private static void KindPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != e.OldValue)
            {
                ((PackIconMaterialDesign)dependencyObject).UpdateData();
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

#if !(NETFX_CORE || WINDOWS_UWP)
        static PackIconMaterialDesign()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PackIconMaterialDesign), new FrameworkPropertyMetadata(typeof(PackIconMaterialDesign)));
        }
#endif

        public PackIconMaterialDesign()
        {
#if NETFX_CORE || WINDOWS_UWP
            this.DefaultStyleKey = typeof(PackIconMaterialDesign);
#endif
        }

        protected override void SetKind<TKind>(TKind iconKind)
        {
#if NETFX_CORE || WINDOWS_UWP
            BindingOperations.SetBinding(this, PackIconMaterialDesign.KindProperty, new Binding() { Source = iconKind, Mode = BindingMode.OneTime });
#else
            this.SetCurrentValue(KindProperty, iconKind);
#endif
        }

        protected override void UpdateData()
        {
            if (Kind != default(PackIconMaterialDesignKind))
            {
                string data = null;
                PackIconMaterialDesignDataFactory.DataIndex.Value?.TryGetValue(Kind, out data);
                this.Data = data;
            }
            else
            {
                this.Data = null;
            }
        }
    }
}