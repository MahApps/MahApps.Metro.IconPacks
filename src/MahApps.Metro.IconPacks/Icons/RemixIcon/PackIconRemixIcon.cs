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
    /// RemixIcon released under the [Apache License Version 2.0](<see><cref>https://github.com/Remix-Design/RemixIcon/blob/master/License</cref></see>)
    /// Contributions, corrections and requests can be made on GitHub <see><cref>https://github.com/Remix-Design/RemixIcon</cref></see>.
    /// </summary>
    [MetaData("Remix Icon", "https://remixicon.com/", "https://github.com/Remix-Design/RemixIcon/blob/master/License")]
    public class PackIconRemixIcon : PackIconControlBase
    {
        public static readonly DependencyProperty KindProperty
            = DependencyProperty.Register(nameof(Kind), typeof(PackIconRemixIconKind), typeof(PackIconRemixIcon), new PropertyMetadata(default(PackIconRemixIconKind), KindPropertyChangedCallback));

        private static void KindPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != e.OldValue)
            {
                ((PackIconRemixIcon)dependencyObject).UpdateData();
            }
        }

        /// <summary>
        /// Gets or sets the icon to display.
        /// </summary>
        public PackIconRemixIconKind Kind
        {
            get { return (PackIconRemixIconKind)GetValue(KindProperty); }
            set { SetValue(KindProperty, value); }
        }

#if !(NETFX_CORE || WINDOWS_UWP)
        static PackIconRemixIcon()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PackIconRemixIcon), new FrameworkPropertyMetadata(typeof(PackIconRemixIcon)));
        }
#endif

        public PackIconRemixIcon()
        {
#if NETFX_CORE || WINDOWS_UWP
            this.DefaultStyleKey = typeof(PackIconRemixIcon);
#endif
        }

        protected override void SetKind<TKind>(TKind iconKind)
        {
#if NETFX_CORE || WINDOWS_UWP
            BindingOperations.SetBinding(this, PackIconRemixIcon.KindProperty, new Binding() { Source = iconKind, Mode = BindingMode.OneTime });
#else
            this.SetCurrentValue(KindProperty, iconKind);
#endif
        }

        protected override void UpdateData()
        {
            if (Kind != default(PackIconRemixIconKind))
            {
                string data = null;
                PackIconRemixIconDataFactory.DataIndex.Value?.TryGetValue(Kind, out data);
                this.Data = data;
            }
            else
            {
                this.Data = null;
            }
        }
    }
}