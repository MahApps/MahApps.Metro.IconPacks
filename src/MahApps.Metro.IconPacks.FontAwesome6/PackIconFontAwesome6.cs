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
    /// All icons sourced from Font Awesome Free <see><cref>https://fontawesome.com/</cref></see> - License <see><cref>https://fontawesome.com/license/free</cref></see>
    /// GitHub <see><cref>https://fontawesome.com/</cref></see>
    /// </summary>
    [MetaData("Font Awesome Free v6", "https://fontawesome.com/", "https://fontawesome.com/license/free")]
    public class PackIconFontAwesome6 : PackIconControlBase
    {
        public static readonly DependencyProperty KindProperty
            = DependencyProperty.Register(nameof(Kind), typeof(PackIconFontAwesome6Kind), typeof(PackIconFontAwesome6), new PropertyMetadata(default(PackIconFontAwesome6Kind), KindPropertyChangedCallback));

        private static void KindPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != e.OldValue)
            {
                ((PackIconFontAwesome6)dependencyObject).UpdateData();
            }
        }

        /// <summary>
        /// Gets or sets the icon to display.
        /// </summary>
        public PackIconFontAwesome6Kind Kind
        {
            get { return (PackIconFontAwesome6Kind)GetValue(KindProperty); }
            set { SetValue(KindProperty, value); }
        }

#if !(NETFX_CORE || WINDOWS_UWP)
        static PackIconFontAwesome6()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PackIconFontAwesome6), new FrameworkPropertyMetadata(typeof(PackIconFontAwesome6)));
        }
#endif

        public PackIconFontAwesome6()
        {
#if NETFX_CORE || WINDOWS_UWP
            this.DefaultStyleKey = typeof(PackIconFontAwesome6);
#endif
        }

        protected override void SetKind<TKind>(TKind iconKind)
        {
#if NETFX_CORE || WINDOWS_UWP
            BindingOperations.SetBinding(this, PackIconFontAwesome6.KindProperty, new Binding() { Source = iconKind, Mode = BindingMode.OneTime });
#else
            this.SetCurrentValue(KindProperty, iconKind);
#endif
        }

        protected override void UpdateData()
        {
            if (Kind != default(PackIconFontAwesome6Kind))
            {
                string data = null;
                PackIconDataFactory<PackIconFontAwesome6Kind>.DataIndex.Value?.TryGetValue(Kind, out data);
                this.Data = data;
            }
            else
            {
                this.Data = null;
            }
        }
    }
}