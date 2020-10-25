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
    /// The Microns icons are licensed under [CC BY-SA](<see><cref>https://creativecommons.org/licenses/by-sa/3.0/</cref></see>) license.
    /// Contributions, corrections and requests can be made on GitHub <see><cref>https://github.com/stephenhutchings/microns</cref></see>.
    /// </summary>
    [MetaData("Microns", "https://www.s-ings.com/projects/microns-icon-font/", "https://github.com/stephenhutchings/microns/blob/master/LICENCE.md")]
    public class PackIconMicrons : PackIconControlBase
    {
        public static readonly DependencyProperty KindProperty
            = DependencyProperty.Register(nameof(Kind), typeof(PackIconMicronsKind), typeof(PackIconMicrons), new PropertyMetadata(default(PackIconMicronsKind), KindPropertyChangedCallback));

        private static void KindPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != e.OldValue)
            {
                ((PackIconMicrons)dependencyObject).UpdateData();
            }
        }

        /// <summary>
        /// Gets or sets the icon to display.
        /// </summary>
        public PackIconMicronsKind Kind
        {
            get { return (PackIconMicronsKind)GetValue(KindProperty); }
            set { SetValue(KindProperty, value); }
        }

#if !(NETFX_CORE || WINDOWS_UWP)
        static PackIconMicrons()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PackIconMicrons), new FrameworkPropertyMetadata(typeof(PackIconMicrons)));
        }
#endif

        public PackIconMicrons()
        {
#if NETFX_CORE || WINDOWS_UWP
            this.DefaultStyleKey = typeof(PackIconMicrons);
#endif
        }

        protected override void SetKind<TKind>(TKind iconKind)
        {
#if NETFX_CORE || WINDOWS_UWP
            BindingOperations.SetBinding(this, PackIconMicrons.KindProperty, new Binding() { Source = iconKind, Mode = BindingMode.OneTime });
#else
            this.SetCurrentValue(KindProperty, iconKind);
#endif
        }

        protected override void UpdateData()
        {
            if (Kind != default(PackIconMicronsKind))
            {
                string data = null;
                PackIconMicronsDataFactory.DataIndex.Value?.TryGetValue(Kind, out data);
                this.Data = data;
            }
            else
            {
                this.Data = null;
            }
        }
    }
}