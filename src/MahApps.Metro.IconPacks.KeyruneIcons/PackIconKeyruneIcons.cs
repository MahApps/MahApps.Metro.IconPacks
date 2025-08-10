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
    /// All icons sourced from GitHub <see><cref>https://github.com/andrewgioia/keyrune</cref></see>
    /// In accordance of <see><cref>https://github.com/andrewgioia/keyrune?tab=License-1-ov-file</cref></see>.
    /// </summary>
    [MetaData("Keyrune Icons", "https://github.com/andrewgioia/keyrune", "https://github.com/andrewgioia/keyrune?tab=License-1-ov-file")]
    public class PackIconKeyruneIcons : PackIconControlBase
    {
        public static readonly DependencyProperty KindProperty
            = DependencyProperty.Register(nameof(Kind), typeof(PackIconKeyruneIconsKind), typeof(PackIconKeyruneIcons), new PropertyMetadata(default(PackIconKeyruneIconsKind), KindPropertyChangedCallback));

        private static void KindPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != e.OldValue)
            {
                ((PackIconKeyruneIcons)dependencyObject).UpdateData();
            }
        }

        /// <summary>
        /// Gets or sets the icon to display.
        /// </summary>
        public PackIconKeyruneIconsKind Kind
        {
            get { return (PackIconKeyruneIconsKind)GetValue(KindProperty); }
            set { SetValue(KindProperty, value); }
        }

#if !(NETFX_CORE || WINDOWS_UWP)
        static PackIconKeyruneIcons()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PackIconKeyruneIcons), new FrameworkPropertyMetadata(typeof(PackIconKeyruneIcons)));
        }
#endif

        public PackIconKeyruneIcons()
        {
#if NETFX_CORE || WINDOWS_UWP
            this.DefaultStyleKey = typeof(PackIconKeyruneIcons);
#endif
        }

        protected override void SetKind<TKind>(TKind iconKind)
        {
#if NETFX_CORE || WINDOWS_UWP
            BindingOperations.SetBinding(this, PackIconKeyruneIcons.KindProperty, new Binding() { Source = iconKind, Mode = BindingMode.OneTime });
#else
            this.SetCurrentValue(KindProperty, iconKind);
#endif
        }

        protected override void UpdateData()
        {
            if (Kind != default(PackIconKeyruneIconsKind))
            {
                string data = null;
                PackIconDataFactory<PackIconKeyruneIconsKind>.DataIndex.Value?.TryGetValue(Kind, out data);
                this.Data = data;
            }
            else
            {
                this.Data = null;
            }
        }
    }
}