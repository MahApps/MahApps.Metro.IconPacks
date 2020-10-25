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
    /// All icons sourced from GitHub Octicons <see><cref>https://octicons.github.com</cref></see>
    /// In accordance of <see><cref>https://github.com/primer/octicons/blob/master/LICENSE</cref></see>.
    /// </summary>
    [MetaData("Octicons", "https://primer.style/octicons/", "https://github.com/primer/octicons/blob/master/LICENSE")]
    public class PackIconOcticons : PackIconControlBase
    {
        public static readonly DependencyProperty KindProperty
            = DependencyProperty.Register(nameof(Kind), typeof(PackIconOcticonsKind), typeof(PackIconOcticons), new PropertyMetadata(default(PackIconOcticonsKind), KindPropertyChangedCallback));

        private static void KindPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != e.OldValue)
            {
                ((PackIconOcticons)dependencyObject).UpdateData();
            }
        }

        /// <summary>
        /// Gets or sets the icon to display.
        /// </summary>
        public PackIconOcticonsKind Kind
        {
            get { return (PackIconOcticonsKind)GetValue(KindProperty); }
            set { SetValue(KindProperty, value); }
        }

#if !(NETFX_CORE || WINDOWS_UWP)
        static PackIconOcticons()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PackIconOcticons), new FrameworkPropertyMetadata(typeof(PackIconOcticons)));
        }
#endif

        public PackIconOcticons()
        {
#if NETFX_CORE || WINDOWS_UWP
            this.DefaultStyleKey = typeof(PackIconOcticons);
#endif
        }

        protected override void SetKind<TKind>(TKind iconKind)
        {
#if NETFX_CORE || WINDOWS_UWP
            BindingOperations.SetBinding(this, PackIconOcticons.KindProperty, new Binding() { Source = iconKind, Mode = BindingMode.OneTime });
#else
            this.SetCurrentValue(KindProperty, iconKind);
#endif
        }

        protected override void UpdateData()
        {
            if (Kind != default(PackIconOcticonsKind))
            {
                string data = null;
                PackIconOcticonsDataFactory.DataIndex.Value?.TryGetValue(Kind, out data);
                this.Data = data;
            }
            else
            {
                this.Data = null;
            }
        }
    }
}