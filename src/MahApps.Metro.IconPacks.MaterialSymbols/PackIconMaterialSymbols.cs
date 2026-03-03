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
    /// All icons sourced from GitHub <see><cref>https://github.com/marella/material-symbols</cref></see>
    /// In accordance of <see><cref>https://github.com/marella/material-symbols?tab=Apache-2.0-1-ov-file</cref></see>
    /// </summary>
    [MetaData("Material Symbols (Google)", "https://github.com/marella/material-symbols", "https://github.com/marella/material-symbols?tab=Apache-2.0-1-ov-file")]
    public class PackIconMaterialSymbols : PackIconControlBase
    {
        public static readonly DependencyProperty KindProperty
            = DependencyProperty.Register(nameof(Kind), typeof(PackIconMaterialSymbolsKind), typeof(PackIconMaterialSymbols), new PropertyMetadata(default(PackIconMaterialSymbolsKind), KindPropertyChangedCallback));

        private static void KindPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != e.OldValue)
            {
                ((PackIconMaterialSymbols)dependencyObject).UpdateData();
            }
        }

        /// <summary>
        /// Gets or sets the icon to display.
        /// </summary>
        public PackIconMaterialSymbolsKind Kind
        {
            get { return (PackIconMaterialSymbolsKind)GetValue(KindProperty); }
            set { SetValue(KindProperty, value); }
        }

#if !(NETFX_CORE || WINDOWS_UWP)
        static PackIconMaterialSymbols()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PackIconMaterialSymbols), new FrameworkPropertyMetadata(typeof(PackIconMaterialSymbols)));
        }
#endif

        public PackIconMaterialSymbols()
        {
#if NETFX_CORE || WINDOWS_UWP
            this.DefaultStyleKey = typeof(PackIconMaterialSymbols);
#endif
        }

        protected override void SetKind<TKind>(TKind iconKind)
        {
#if NETFX_CORE || WINDOWS_UWP
            BindingOperations.SetBinding(this, PackIconMaterialSymbols.KindProperty, new Binding() { Source = iconKind, Mode = BindingMode.OneTime });
#else
            this.SetCurrentValue(KindProperty, iconKind);
#endif
        }

        protected override void UpdateData()
        {
            if (Kind != default(PackIconMaterialSymbolsKind))
            {
                string data = null;
                PackIconDataFactory<PackIconMaterialSymbolsKind>.DataIndex.Value?.TryGetValue(Kind, out data);
                this.Data = data;
            }
            else
            {
                this.Data = null;
            }
        }
    }
}