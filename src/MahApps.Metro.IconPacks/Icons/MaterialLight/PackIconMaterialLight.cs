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
    /// All icons sourced from Material Design Light Icons <see><cref>https://github.com/Templarian/MaterialDesignLight</cref></see>
    /// In accordance of <see><cref>https://github.com/Templarian/MaterialDesignLight/blob/master/LICENSE.md</cref></see>.
    /// </summary>
    [MetaData("Material Design Icons Light", "https://github.com/Templarian/MaterialDesignLight", "https://github.com/Templarian/MaterialDesignLight/blob/master/LICENSE.md")]
    public class PackIconMaterialLight : PackIconControlBase
    {
        public static readonly DependencyProperty KindProperty
            = DependencyProperty.Register(nameof(Kind), typeof(PackIconMaterialLightKind), typeof(PackIconMaterialLight), new PropertyMetadata(default(PackIconMaterialLightKind), KindPropertyChangedCallback));

        private static void KindPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != e.OldValue)
            {
                ((PackIconMaterialLight)dependencyObject).UpdateData();
            }
        }

        /// <summary>
        /// Gets or sets the icon to display.
        /// </summary>
        public PackIconMaterialLightKind Kind
        {
            get { return (PackIconMaterialLightKind)GetValue(KindProperty); }
            set { SetValue(KindProperty, value); }
        }

#if !(NETFX_CORE || WINDOWS_UWP)
        static PackIconMaterialLight()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PackIconMaterialLight), new FrameworkPropertyMetadata(typeof(PackIconMaterialLight)));
        }
#endif

        public PackIconMaterialLight()
        {
#if NETFX_CORE || WINDOWS_UWP
            this.DefaultStyleKey = typeof(PackIconMaterialLight);
#endif
        }

        protected override void SetKind<TKind>(TKind iconKind)
        {
#if NETFX_CORE || WINDOWS_UWP
            BindingOperations.SetBinding(this, PackIconMaterialLight.KindProperty, new Binding() { Source = iconKind, Mode = BindingMode.OneTime });
#else
            this.SetCurrentValue(KindProperty, iconKind);
#endif
        }

        protected override void UpdateData()
        {
            if (Kind != default(PackIconMaterialLightKind))
            {
                string data = null;
                PackIconMaterialLightDataFactory.DataIndex.Value?.TryGetValue(Kind, out data);
                this.Data = data;
            }
            else
            {
                this.Data = null;
            }
        }
    }
}