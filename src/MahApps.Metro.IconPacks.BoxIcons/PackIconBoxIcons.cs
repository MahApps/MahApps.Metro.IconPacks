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
    /// BoxIcons v3 licensed under [CC 4.0 License](<see><cref>https://docs.boxicons.com/license/free</cref></see>)
    /// Project web site <see><cref>https://boxicons.com/</cref></see>.
    /// </summary>
    [MetaData("Boxicons v3", "https://boxicons.com/", "https://docs.boxicons.com/license/free")]
    public class PackIconBoxIcons : PackIconControlBase
    {
        public static readonly DependencyProperty KindProperty
            = DependencyProperty.Register(nameof(Kind), typeof(PackIconBoxIconsKind), typeof(PackIconBoxIcons), new PropertyMetadata(default(PackIconBoxIconsKind), KindPropertyChangedCallback));

        private static void KindPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != e.OldValue)
            {
                ((PackIconBoxIcons)dependencyObject).UpdateData();
            }
        }

        /// <summary>
        /// Gets or sets the icon to display.
        /// </summary>
        public PackIconBoxIconsKind Kind
        {
            get { return (PackIconBoxIconsKind)GetValue(KindProperty); }
            set { SetValue(KindProperty, value); }
        }

#if !(NETFX_CORE || WINDOWS_UWP)
        static PackIconBoxIcons()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PackIconBoxIcons), new FrameworkPropertyMetadata(typeof(PackIconBoxIcons)));
        }
#endif

        public PackIconBoxIcons()
        {
#if NETFX_CORE || WINDOWS_UWP
            this.DefaultStyleKey = typeof(PackIconBoxIcons);
#endif
        }

        protected override void SetKind<TKind>(TKind iconKind)
        {
#if NETFX_CORE || WINDOWS_UWP
            BindingOperations.SetBinding(this, PackIconBoxIcons.KindProperty, new Binding() { Source = iconKind, Mode = BindingMode.OneTime });
#else
            this.SetCurrentValue(KindProperty, iconKind);
#endif
        }

        protected override void UpdateData()
        {
            if (Kind != default(PackIconBoxIconsKind))
            {
                string data = null;
                PackIconDataFactory<PackIconBoxIconsKind>.DataIndex.Value?.TryGetValue(Kind, out data);
                this.Data = data;
            }
            else
            {
                this.Data = null;
            }
        }
    }
}