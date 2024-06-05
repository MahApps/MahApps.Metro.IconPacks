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
    /// All icons sourced from Phosphor Icons - <see><cref>https://phosphoricons.com/</cref></see>
    /// Phosphor Icons are licensed under the MIT License <see><cref>https://github.com/phosphor-icons/core?tab=MIT-1-ov-file#readme</cref></see>
    /// </summary>
    [MetaData("Phosphor Icons", "https://phosphoricons.com/", "https://github.com/phosphor-icons/core?tab=MIT-1-ov-file#readme")]
    public class PackIconPhosphorIcons : PackIconControlBase
    {
        public static readonly DependencyProperty KindProperty
            = DependencyProperty.Register(nameof(Kind), typeof(PackIconPhosphorIconsKind), typeof(PackIconPhosphorIcons), new PropertyMetadata(default(PackIconPhosphorIconsKind), KindPropertyChangedCallback));

        private static void KindPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != e.OldValue)
            {
                ((PackIconPhosphorIcons)dependencyObject).UpdateData();
            }
        }

        /// <summary>
        /// Gets or sets the icon to display.
        /// </summary>
        public PackIconPhosphorIconsKind Kind
        {
            get { return (PackIconPhosphorIconsKind)GetValue(KindProperty); }
            set { SetValue(KindProperty, value); }
        }

#if !(NETFX_CORE || WINDOWS_UWP)
        static PackIconPhosphorIcons()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PackIconPhosphorIcons), new FrameworkPropertyMetadata(typeof(PackIconPhosphorIcons)));
        }
#endif

        public PackIconPhosphorIcons()
        {
#if NETFX_CORE || WINDOWS_UWP
            this.DefaultStyleKey = typeof(PackIconPhosphorIcons);
#endif
        }

        protected override void SetKind<TKind>(TKind iconKind)
        {
#if NETFX_CORE || WINDOWS_UWP
            BindingOperations.SetBinding(this, PackIconPhosphorIcons.KindProperty, new Binding() { Source = iconKind, Mode = BindingMode.OneTime });
#else
            this.SetCurrentValue(KindProperty, iconKind);
#endif
        }

        protected override void UpdateData()
        {
            if (Kind != default(PackIconPhosphorIconsKind))
            {
                string data = null;
                PackIconDataFactory<PackIconPhosphorIconsKind>.DataIndex.Value?.TryGetValue(Kind, out data);
                this.Data = data;
            }
            else
            {
                this.Data = null;
            }
        }
    }
}