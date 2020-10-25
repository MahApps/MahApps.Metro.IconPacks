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
    /// Ionicons are licensed under the [MIT license](<see><cref>https://github.com/ionic-team/ionicons/blob/master/LICENSE</cref></see>).
    /// Contributions, corrections and requests can be made on GitHub <see><cref>https://github.com/ionic-team/ionicons</cref></see>.
    /// </summary>
    [MetaData("Ionicons", "https://ionicons.com/", "https://github.com/ionic-team/ionicons/blob/master/LICENSE")]
    public class PackIconIonicons : PackIconControlBase
    {
        public static readonly DependencyProperty KindProperty
            = DependencyProperty.Register(nameof(Kind), typeof(PackIconIoniconsKind), typeof(PackIconIonicons), new PropertyMetadata(default(PackIconIoniconsKind), KindPropertyChangedCallback));

        private static void KindPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != e.OldValue)
            {
                ((PackIconIonicons)dependencyObject).UpdateData();
            }
        }

        /// <summary>
        /// Gets or sets the icon to display.
        /// </summary>
        public PackIconIoniconsKind Kind
        {
            get { return (PackIconIoniconsKind)GetValue(KindProperty); }
            set { SetValue(KindProperty, value); }
        }

#if !(NETFX_CORE || WINDOWS_UWP)
        static PackIconIonicons()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PackIconIonicons), new FrameworkPropertyMetadata(typeof(PackIconIonicons)));
        }
#endif

        public PackIconIonicons()
        {
#if NETFX_CORE || WINDOWS_UWP
            this.DefaultStyleKey = typeof(PackIconIonicons);
#endif
        }

        protected override void SetKind<TKind>(TKind iconKind)
        {
#if NETFX_CORE || WINDOWS_UWP
            BindingOperations.SetBinding(this, PackIconIonicons.KindProperty, new Binding() { Source = iconKind, Mode = BindingMode.OneTime });
#else
            this.SetCurrentValue(KindProperty, iconKind);
#endif
        }

        protected override void UpdateData()
        {
            if (Kind != default(PackIconIoniconsKind))
            {
                string data = null;
                PackIconIoniconsDataFactory.DataIndex.Value?.TryGetValue(Kind, out data);
                this.Data = data;
            }
            else
            {
                this.Data = null;
            }
        }
    }
}