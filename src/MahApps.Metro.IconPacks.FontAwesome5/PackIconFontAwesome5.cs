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
    [MetaData("Font Awesome Free v5", "https://fontawesome.com/", "https://fontawesome.com/license/free")]
    public class PackIconFontAwesome5 : PackIconControlBase
    {
        public static readonly DependencyProperty KindProperty
            = DependencyProperty.Register(nameof(Kind), typeof(PackIconFontAwesome5Kind), typeof(PackIconFontAwesome5), new PropertyMetadata(default(PackIconFontAwesome5Kind), KindPropertyChangedCallback));

        private static void KindPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != e.OldValue)
            {
                ((PackIconFontAwesome5)dependencyObject).UpdateData();
            }
        }

        /// <summary>
        /// Gets or sets the icon to display.
        /// </summary>
        public PackIconFontAwesome5Kind Kind
        {
            get { return (PackIconFontAwesome5Kind)GetValue(KindProperty); }
            set { SetValue(KindProperty, value); }
        }

#if !(NETFX_CORE || WINDOWS_UWP)
        static PackIconFontAwesome5()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PackIconFontAwesome5), new FrameworkPropertyMetadata(typeof(PackIconFontAwesome5)));
        }
#endif

        public PackIconFontAwesome5()
        {
#if NETFX_CORE || WINDOWS_UWP
            this.DefaultStyleKey = typeof(PackIconFontAwesome5);
#endif
        }

        protected override void SetKind<TKind>(TKind iconKind)
        {
#if NETFX_CORE || WINDOWS_UWP
            BindingOperations.SetBinding(this, PackIconFontAwesome5.KindProperty, new Binding() { Source = iconKind, Mode = BindingMode.OneTime });
#else
            this.SetCurrentValue(KindProperty, iconKind);
#endif
        }

        protected override void UpdateData()
        {
            if (Kind != default(PackIconFontAwesome5Kind))
            {
                string data = null;
                PackIconDataFactory<PackIconFontAwesome5Kind>.DataIndex.Value?.TryGetValue(Kind, out data);
                this.Data = data;
            }
            else
            {
                this.Data = null;
            }
        }
    }
}