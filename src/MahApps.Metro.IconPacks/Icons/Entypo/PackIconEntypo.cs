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
    /// All icons sourced from Entypo+ Icons Font <see><cref>http://www.entypo.com</cref></see>
    /// Licensed under [CC BY 4.0](<see><cref>http://creativecommons.org/licenses/by-sa/4.0/</cref></see>).
    /// </summary>
    [MetaData("Entypo+", "http://www.entypo.com/", "https://creativecommons.org/licenses/by-sa/4.0/")]
    public class PackIconEntypo : PackIconControlBase
    {
        public static readonly DependencyProperty KindProperty
            = DependencyProperty.Register(nameof(Kind), typeof(PackIconEntypoKind), typeof(PackIconEntypo), new PropertyMetadata(default(PackIconEntypoKind), KindPropertyChangedCallback));

        private static void KindPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != e.OldValue)
            {
                ((PackIconEntypo)dependencyObject).UpdateData();
            }
        }

        /// <summary>
        /// Gets or sets the icon to display.
        /// </summary>
        public PackIconEntypoKind Kind
        {
            get { return (PackIconEntypoKind)GetValue(KindProperty); }
            set { SetValue(KindProperty, value); }
        }

#if !(NETFX_CORE || WINDOWS_UWP)
        static PackIconEntypo()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PackIconEntypo), new FrameworkPropertyMetadata(typeof(PackIconEntypo)));
        }
#endif

        public PackIconEntypo()
        {
#if NETFX_CORE || WINDOWS_UWP
            this.DefaultStyleKey = typeof(PackIconEntypo);
#endif
        }

        protected override void SetKind<TKind>(TKind iconKind)
        {
#if NETFX_CORE || WINDOWS_UWP
            BindingOperations.SetBinding(this, PackIconEntypo.KindProperty, new Binding() { Source = iconKind, Mode = BindingMode.OneTime });
#else
            this.SetCurrentValue(KindProperty, iconKind);
#endif
        }

        protected override void UpdateData()
        {
            if (Kind != default(PackIconEntypoKind))
            {
                string data = null;
                PackIconEntypoDataFactory.DataIndex.Value?.TryGetValue(Kind, out data);
                this.Data = data;
            }
            else
            {
                this.Data = null;
            }
        }
    }
}