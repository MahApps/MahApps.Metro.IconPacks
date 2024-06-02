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
    /// The Fork Awesome font is licensed under the SIL OFL 1.1 (<see><cref>http://scripts.sil.org/OFL</cref></see>). Fork Awesome is a fork based of off Font Awesome 4.7.0 by Dave Gandy.
    /// More info on licenses at <see><cref>https://forkawesome.github.io</cref></see>. Contributions, corrections and requests can be made on GitHub <see><cref>https://github.com/ForkAwesome/Fork-Awesome</cref></see>.
    /// </summary>
    [MetaData("Fork Awesome", "https://forkaweso.me/", "https://github.com/ForkAwesome/Fork-Awesome/blob/master/LICENSES")]
    public class PackIconForkAwesome : PackIconControlBase
    {
        public static readonly DependencyProperty KindProperty
            = DependencyProperty.Register(nameof(Kind), typeof(PackIconForkAwesomeKind), typeof(PackIconForkAwesome), new PropertyMetadata(default(PackIconForkAwesomeKind), KindPropertyChangedCallback));

        private static void KindPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != e.OldValue)
            {
                ((PackIconForkAwesome)dependencyObject).UpdateData();
            }
        }

        /// <summary>
        /// Gets or sets the icon to display.
        /// </summary>
        public PackIconForkAwesomeKind Kind
        {
            get { return (PackIconForkAwesomeKind)GetValue(KindProperty); }
            set { SetValue(KindProperty, value); }
        }

#if !(NETFX_CORE || WINDOWS_UWP)
        static PackIconForkAwesome()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PackIconForkAwesome), new FrameworkPropertyMetadata(typeof(PackIconForkAwesome)));
        }
#endif

        public PackIconForkAwesome()
        {
#if NETFX_CORE || WINDOWS_UWP
            this.DefaultStyleKey = typeof(PackIconForkAwesome);
#endif
        }

        protected override void SetKind<TKind>(TKind iconKind)
        {
#if NETFX_CORE || WINDOWS_UWP
            BindingOperations.SetBinding(this, PackIconForkAwesome.KindProperty, new Binding() { Source = iconKind, Mode = BindingMode.OneTime });
#else
            this.SetCurrentValue(KindProperty, iconKind);
#endif
        }

        protected override void UpdateData()
        {
            if (Kind != default(PackIconForkAwesomeKind))
            {
                string data = null;
                PackIconForkAwesomeDataFactory.DataIndex.Value?.TryGetValue(Kind, out data);
                this.Data = data;
            }
            else
            {
                this.Data = null;
            }
        }
    }
}