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
    /// MingCute Icon are licensed under [Apache-2.0 license](<see><cref>https://github.com/Richard9394/MingCute?tab=Apache-2.0-1-ov-file</cref></see>).
    /// Contributions, corrections and requests can be made on GitHub <see><cref>https://github.com/Richard9394/MingCute</cref></see>.
    /// </summary>
    [MetaData("MingCute Icon", "https://github.com/Richard9394/MingCute", "https://github.com/Richard9394/MingCute?tab=Apache-2.0-1-ov-file")]
    public class PackIconMingCuteIcons : PackIconControlBase
    {
        public static readonly DependencyProperty KindProperty
            = DependencyProperty.Register(nameof(Kind), typeof(PackIconMingCuteIconsKind), typeof(PackIconMingCuteIcons), new PropertyMetadata(default(PackIconMingCuteIconsKind), KindPropertyChangedCallback));

        private static void KindPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != e.OldValue)
            {
                ((PackIconMingCuteIcons)dependencyObject).UpdateData();
            }
        }

        /// <summary>
        /// Gets or sets the icon to display.
        /// </summary>
        public PackIconMingCuteIconsKind Kind
        {
            get { return (PackIconMingCuteIconsKind)GetValue(KindProperty); }
            set { SetValue(KindProperty, value); }
        }

#if !(NETFX_CORE || WINDOWS_UWP)
        static PackIconMingCuteIcons()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PackIconMingCuteIcons), new FrameworkPropertyMetadata(typeof(PackIconMingCuteIcons)));
        }
#endif

        public PackIconMingCuteIcons()
        {
#if NETFX_CORE || WINDOWS_UWP
            this.DefaultStyleKey = typeof(PackIconMingCuteIcons);
#endif
        }

        protected override void SetKind<TKind>(TKind iconKind)
        {
#if NETFX_CORE || WINDOWS_UWP
            BindingOperations.SetBinding(this, PackIconMingCuteIcons.KindProperty, new Binding() { Source = iconKind, Mode = BindingMode.OneTime });
#else
            this.SetCurrentValue(KindProperty, iconKind);
#endif
        }

        protected override void UpdateData()
        {
            if (Kind != default(PackIconMingCuteIconsKind))
            {
                string data = null;
                PackIconDataFactory<PackIconMingCuteIconsKind>.DataIndex.Value?.TryGetValue(Kind, out data);
                this.Data = data;
            }
            else
            {
                this.Data = null;
            }
        }
    }
}