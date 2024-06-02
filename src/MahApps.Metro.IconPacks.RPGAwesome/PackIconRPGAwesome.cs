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
    /// The RPG Awesome font is licensed under the [SIL OFL 1.1](<see><cref>http://scripts.sil.org/OFL</cref></see>)
    /// Contributions, corrections and requests can be made on GitHub <see><cref>https://github.com/nagoshiashumari/Rpg-Awesome</cref></see>.
    /// </summary>
    [MetaData("RPG Awesome", "http://nagoshiashumari.github.io/Rpg-Awesome/", "https://github.com/nagoshiashumari/Rpg-Awesome/blob/master/LICENSE.md")]
    public class PackIconRPGAwesome : PackIconControlBase
    {
        public static readonly DependencyProperty KindProperty
            = DependencyProperty.Register(nameof(Kind), typeof(PackIconRPGAwesomeKind), typeof(PackIconRPGAwesome), new PropertyMetadata(default(PackIconRPGAwesomeKind), KindPropertyChangedCallback));

        private static void KindPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != e.OldValue)
            {
                ((PackIconRPGAwesome)dependencyObject).UpdateData();
            }
        }

        /// <summary>
        /// Gets or sets the icon to display.
        /// </summary>
        public PackIconRPGAwesomeKind Kind
        {
            get { return (PackIconRPGAwesomeKind)GetValue(KindProperty); }
            set { SetValue(KindProperty, value); }
        }

#if !(NETFX_CORE || WINDOWS_UWP)
        static PackIconRPGAwesome()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PackIconRPGAwesome), new FrameworkPropertyMetadata(typeof(PackIconRPGAwesome)));
        }
#endif

        public PackIconRPGAwesome()
        {
#if NETFX_CORE || WINDOWS_UWP
            this.DefaultStyleKey = typeof(PackIconRPGAwesome);
#endif
        }

        protected override void SetKind<TKind>(TKind iconKind)
        {
#if NETFX_CORE || WINDOWS_UWP
            BindingOperations.SetBinding(this, PackIconRPGAwesome.KindProperty, new Binding() { Source = iconKind, Mode = BindingMode.OneTime });
#else
            this.SetCurrentValue(KindProperty, iconKind);
#endif
        }

        protected override void UpdateData()
        {
            if (Kind != default(PackIconRPGAwesomeKind))
            {
                string data = null;
                PackIconRPGAwesomeDataFactory.DataIndex.Value?.TryGetValue(Kind, out data);
                this.Data = data;
            }
            else
            {
                this.Data = null;
            }
        }
    }
}