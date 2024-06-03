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
    /// All icons sourced from Memory Icons <see><cref>https://pictogrammers.com/library/memory/</cref></see>
    /// In accordance of <see><cref>https://github.com/Pictogrammers/Memory?tab=License-1-ov-file#readme</cref></see>.
    /// </summary>
    [MetaData("Memory Icons", "https://pictogrammers.com/library/memory/", "https://github.com/Pictogrammers/Memory?tab=License-1-ov-file#readme")]
    public class PackIconMemoryIcons : PackIconControlBase
    {
        public static readonly DependencyProperty KindProperty
            = DependencyProperty.Register(nameof(Kind), typeof(PackIconMemoryIconsKind), typeof(PackIconMemoryIcons), new PropertyMetadata(default(PackIconMemoryIconsKind), KindPropertyChangedCallback));

        private static void KindPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != e.OldValue)
            {
                ((PackIconMemoryIcons)dependencyObject).UpdateData();
            }
        }

        /// <summary>
        /// Gets or sets the icon to display.
        /// </summary>
        public PackIconMemoryIconsKind Kind
        {
            get { return (PackIconMemoryIconsKind)GetValue(KindProperty); }
            set { SetValue(KindProperty, value); }
        }

#if !(NETFX_CORE || WINDOWS_UWP)
        static PackIconMemoryIcons()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PackIconMemoryIcons), new FrameworkPropertyMetadata(typeof(PackIconMemoryIcons)));
        }
#endif

        public PackIconMemoryIcons()
        {
#if NETFX_CORE || WINDOWS_UWP
            this.DefaultStyleKey = typeof(PackIconMemoryIcons);
#endif
        }

        protected override void SetKind<TKind>(TKind iconKind)
        {
#if NETFX_CORE || WINDOWS_UWP
            BindingOperations.SetBinding(this, PackIconMemoryIcons.KindProperty, new Binding() { Source = iconKind, Mode = BindingMode.OneTime });
#else
            this.SetCurrentValue(KindProperty, iconKind);
#endif
        }

        protected override void UpdateData()
        {
            if (Kind != default(PackIconMemoryIconsKind))
            {
                string data = null;
                PackIconDataFactory<PackIconMemoryIconsKind>.DataIndex.Value?.TryGetValue(Kind, out data);
                this.Data = data;
            }
            else
            {
                this.Data = null;
            }
        }
    }
}