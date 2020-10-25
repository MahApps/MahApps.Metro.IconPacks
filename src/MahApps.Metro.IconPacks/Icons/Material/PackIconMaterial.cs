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
    /// All icons sourced from Material Design Icons Font <see><cref>https://materialdesignicons.com</cref></see>
    /// In accordance of <see><cref>https://github.com/Templarian/MaterialDesign/blob/master/license.txt</cref></see>.
    /// </summary>
    [MetaData("Material Design Icons", "https://materialdesignicons.com/", "https://github.com/Templarian/MaterialDesign/blob/master/LICENSE")]
    public class PackIconMaterial : PackIconControlBase
    {
        public static readonly DependencyProperty KindProperty
            = DependencyProperty.Register(nameof(Kind), typeof(PackIconMaterialKind), typeof(PackIconMaterial), new PropertyMetadata(default(PackIconMaterialKind), KindPropertyChangedCallback));

        private static void KindPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != e.OldValue)
            {
                ((PackIconMaterial)dependencyObject).UpdateData();
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

#if !(NETFX_CORE || WINDOWS_UWP)
        static PackIconMaterial()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PackIconMaterial), new FrameworkPropertyMetadata(typeof(PackIconMaterial)));
        }
#endif

        public PackIconMaterial()
        {
#if NETFX_CORE || WINDOWS_UWP
            this.DefaultStyleKey = typeof(PackIconMaterial);
#endif
        }

        protected override void SetKind<TKind>(TKind iconKind)
        {
#if NETFX_CORE || WINDOWS_UWP
            BindingOperations.SetBinding(this, PackIconMaterial.KindProperty, new Binding() { Source = iconKind, Mode = BindingMode.OneTime });
#else
            this.SetCurrentValue(KindProperty, iconKind);
#endif
        }

        protected override void UpdateData()
        {
            if (Kind != default(PackIconMaterialKind))
            {
                string data = null;
                PackIconMaterialDataFactory.DataIndex.Value?.TryGetValue(Kind, out data);
                this.Data = data;
            }
            else
            {
                this.Data = null;
            }
        }
    }
}