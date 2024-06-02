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
    /// All icons sourced from Modern UI Icons Font <see><cref>https://github.com/Templarian/WindowsIcons</cref></see>
    /// In accordance of <see><cref>https://github.com/Templarian/WindowsIcons/blob/master/WindowsPhone/license.txt</cref></see>.
    /// </summary>
    [MetaData("Modern UI Icons", "https://github.com/Templarian/WindowsIcons", "https://github.com/Templarian/WindowsIcons/blob/master/WindowsPhone/license.txt")]
    public class PackIconModern : PackIconControlBase
    {
        public static readonly DependencyProperty KindProperty
            = DependencyProperty.Register(nameof(Kind), typeof(PackIconModernKind), typeof(PackIconModern), new PropertyMetadata(default(PackIconModernKind), KindPropertyChangedCallback));

        private static void KindPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != e.OldValue)
            {
                ((PackIconModern)dependencyObject).UpdateData();
            }
        }

        /// <summary>
        /// Gets or sets the icon to display.
        /// </summary>
        public PackIconModernKind Kind
        {
            get { return (PackIconModernKind)GetValue(KindProperty); }
            set { SetValue(KindProperty, value); }
        }

#if !(NETFX_CORE || WINDOWS_UWP)
        static PackIconModern()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PackIconModern), new FrameworkPropertyMetadata(typeof(PackIconModern)));
        }
#endif

        public PackIconModern()
        {
#if NETFX_CORE || WINDOWS_UWP
            this.DefaultStyleKey = typeof(PackIconModern);
#endif
        }

        protected override void SetKind<TKind>(TKind iconKind)
        {
#if NETFX_CORE || WINDOWS_UWP
            BindingOperations.SetBinding(this, PackIconModern.KindProperty, new Binding() { Source = iconKind, Mode = BindingMode.OneTime });
#else
            this.SetCurrentValue(KindProperty, iconKind);
#endif
        }

        protected override void UpdateData()
        {
            if (Kind != default(PackIconModernKind))
            {
                string data = null;
                PackIconModernDataFactory.DataIndex.Value?.TryGetValue(Kind, out data);
                this.Data = data;
            }
            else
            {
                this.Data = null;
            }
        }
    }
}