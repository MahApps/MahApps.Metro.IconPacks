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
    /// The Fontisto font is licensed under the [SIL OFL 1.1](<see><cref>https://github.com/kenangundogan/fontisto#license</cref></see>).
    /// Contributions, corrections and requests can be made on GitHub <see><cref>https://fontisto.com/</cref></see>.
    /// </summary>
    [MetaData("Fontisto", "https://fontisto.com/", "https://github.com/kenangundogan/fontisto#license")]
    public class PackIconFontisto : PackIconControlBase
    {
        public static readonly DependencyProperty KindProperty
            = DependencyProperty.Register(nameof(Kind), typeof(PackIconFontistoKind), typeof(PackIconFontisto), new PropertyMetadata(default(PackIconFontistoKind), KindPropertyChangedCallback));

        private static void KindPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != e.OldValue)
            {
                ((PackIconFontisto)dependencyObject).UpdateData();
            }
        }

        /// <summary>
        /// Gets or sets the icon to display.
        /// </summary>
        public PackIconFontistoKind Kind
        {
            get { return (PackIconFontistoKind)GetValue(KindProperty); }
            set { SetValue(KindProperty, value); }
        }

#if !(NETFX_CORE || WINDOWS_UWP)
        static PackIconFontisto()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PackIconFontisto), new FrameworkPropertyMetadata(typeof(PackIconFontisto)));
        }
#endif

        public PackIconFontisto()
        {
#if NETFX_CORE || WINDOWS_UWP
            this.DefaultStyleKey = typeof(PackIconFontisto);
#endif
        }

        protected override void SetKind<TKind>(TKind iconKind)
        {
#if NETFX_CORE || WINDOWS_UWP
            BindingOperations.SetBinding(this, PackIconFontisto.KindProperty, new Binding() { Source = iconKind, Mode = BindingMode.OneTime });
#else
            this.SetCurrentValue(KindProperty, iconKind);
#endif
        }

        protected override void UpdateData()
        {
            if (Kind != default(PackIconFontistoKind))
            {
                string data = null;
                PackIconFontistoDataFactory.DataIndex.Value?.TryGetValue(Kind, out data);
                this.Data = data;
            }
            else
            {
                this.Data = null;
            }
        }
    }
}