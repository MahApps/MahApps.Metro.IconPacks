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
    /// Bootstrap Icons are licensed under the [MIT license](<see><cref>https://github.com/twbs/icons/blob/main/LICENSE.md</cref></see>).
    /// Contributions, corrections and requests can be made on GitHub <see><cref>https://github.com/twbs/icons</cref></see>.
    /// </summary>
    [MetaData("Bootstrap Icons", "https://icons.getbootstrap.com/", "https://github.com/twbs/icons/blob/main/LICENSE.md")]
    public class PackIconBootstrapIcons : PackIconControlBase
    {
        public static readonly DependencyProperty KindProperty
            = DependencyProperty.Register(nameof(Kind), typeof(PackIconBootstrapIconsKind), typeof(PackIconBootstrapIcons), new PropertyMetadata(default(PackIconBootstrapIconsKind), KindPropertyChangedCallback));

        private static void KindPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != e.OldValue)
            {
                ((PackIconBootstrapIcons)dependencyObject).UpdateData();
            }
        }

        /// <summary>
        /// Gets or sets the icon to display.
        /// </summary>
        public PackIconBootstrapIconsKind Kind
        {
            get { return (PackIconBootstrapIconsKind)GetValue(KindProperty); }
            set { SetValue(KindProperty, value); }
        }

#if !(NETFX_CORE || WINDOWS_UWP)
        static PackIconBootstrapIcons()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PackIconBootstrapIcons), new FrameworkPropertyMetadata(typeof(PackIconBootstrapIcons)));
        }
#endif

        public PackIconBootstrapIcons()
        {
#if NETFX_CORE || WINDOWS_UWP
            this.DefaultStyleKey = typeof(PackIconBootstrapIcons);
#endif
        }

        protected override void SetKind<TKind>(TKind iconKind)
        {
#if NETFX_CORE || WINDOWS_UWP
            BindingOperations.SetBinding(this, PackIconBootstrapIcons.KindProperty, new Binding() { Source = iconKind, Mode = BindingMode.OneTime });
#else
            this.SetCurrentValue(KindProperty, iconKind);
#endif
        }

        protected override void UpdateData()
        {
            if (Kind != default(PackIconBootstrapIconsKind))
            {
                string data = null;
                PackIconBootstrapIconsDataFactory.DataIndex.Value?.TryGetValue(Kind, out data);
                this.Data = data;
            }
            else
            {
                this.Data = null;
            }
        }
    }
}