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
    /// Zondicons are licensed under the [CC BY 4.0](<see><cref>https://creativecommons.org/licenses/by/4.0/</cref></see>).
    /// Zondicons are availabe at <see><cref>https://www.zondicons.com/</cref></see>.
    /// </summary>
    [MetaData("Zondicons", "https://www.zondicons.com/", "https://creativecommons.org/licenses/by/4.0/")]
    public class PackIconZondicons : PackIconControlBase
    {
        public static readonly DependencyProperty KindProperty
            = DependencyProperty.Register(nameof(Kind), typeof(PackIconZondiconsKind), typeof(PackIconZondicons), new PropertyMetadata(default(PackIconZondiconsKind), KindPropertyChangedCallback));

        private static void KindPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != e.OldValue)
            {
                ((PackIconZondicons)dependencyObject).UpdateData();
            }
        }

        /// <summary>
        /// Gets or sets the icon to display.
        /// </summary>
        public PackIconZondiconsKind Kind
        {
            get { return (PackIconZondiconsKind)GetValue(KindProperty); }
            set { SetValue(KindProperty, value); }
        }

#if !(NETFX_CORE || WINDOWS_UWP)
        static PackIconZondicons()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PackIconZondicons), new FrameworkPropertyMetadata(typeof(PackIconZondicons)));
        }
#endif

        public PackIconZondicons()
        {
#if NETFX_CORE || WINDOWS_UWP
            this.DefaultStyleKey = typeof(PackIconZondicons);
#endif
        }

        protected override void SetKind<TKind>(TKind iconKind)
        {
#if NETFX_CORE || WINDOWS_UWP
            BindingOperations.SetBinding(this, PackIconZondicons.KindProperty, new Binding() { Source = iconKind, Mode = BindingMode.OneTime });
#else
            this.SetCurrentValue(KindProperty, iconKind);
#endif
        }

        protected override void UpdateData()
        {
            if (Kind != default(PackIconZondiconsKind))
            {
                string data = null;
                PackIconZondiconsDataFactory.DataIndex.Value?.TryGetValue(Kind, out data);
                this.Data = data;
            }
            else
            {
                this.Data = null;
            }
        }
    }
}