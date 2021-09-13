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
    /// Coolicons icons are licensed under [CC 4.0](<see><cref>https://github.com/krystonschwarze/coolicons#license</cref></see>).
    /// Contributions, corrections and requests can be made on GitHub <see><cref>https://github.com/krystonschwarze/coolicons</cref></see>.
    /// </summary>
    [MetaData("Coolicons", "https://github.com/krystonschwarze/coolicons", "https://github.com/krystonschwarze/coolicons#license")]
    public class PackIconCoolicons : PackIconControlBase
    {
        public static readonly DependencyProperty KindProperty
            = DependencyProperty.Register(nameof(Kind), typeof(PackIconCooliconsKind), typeof(PackIconCoolicons), new PropertyMetadata(default(PackIconCooliconsKind), KindPropertyChangedCallback));

        private static void KindPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != e.OldValue)
            {
                ((PackIconCoolicons)dependencyObject).UpdateData();
            }
        }

        /// <summary>
        /// Gets or sets the icon to display.
        /// </summary>
        public PackIconCooliconsKind Kind
        {
            get { return (PackIconCooliconsKind)GetValue(KindProperty); }
            set { SetValue(KindProperty, value); }
        }

#if !(NETFX_CORE || WINDOWS_UWP)
        static PackIconCoolicons()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PackIconCoolicons), new FrameworkPropertyMetadata(typeof(PackIconCoolicons)));
        }
#endif

        public PackIconCoolicons()
        {
#if NETFX_CORE || WINDOWS_UWP
            this.DefaultStyleKey = typeof(PackIconCoolicons);
#endif
        }

        protected override void SetKind<TKind>(TKind iconKind)
        {
#if NETFX_CORE || WINDOWS_UWP
            BindingOperations.SetBinding(this, PackIconCoolicons.KindProperty, new Binding() { Source = iconKind, Mode = BindingMode.OneTime });
#else
            this.SetCurrentValue(KindProperty, iconKind);
#endif
        }

        protected override void UpdateData()
        {
            if (Kind != default(PackIconCooliconsKind))
            {
                string data = null;
                PackIconCooliconsDataFactory.DataIndex.Value?.TryGetValue(Kind, out data);
                this.Data = data;
            }
            else
            {
                this.Data = null;
            }
        }
    }
}