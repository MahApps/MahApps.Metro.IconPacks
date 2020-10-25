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
    /// Vaadin Icons are licensed under the [Apache License 2.0](<see><cref>https://github.com/vaadin/vaadin-icons/blob/master/LICENSE</cref></see>).
    /// Contributions, corrections and requests can be made on GitHub <see><cref>https://github.com/vaadin/vaadin-icons</cref></see>.
    /// </summary>
    [MetaData("Vaadin Icons", "https://vaadin.com/icons", "https://github.com/vaadin/vaadin-icons/blob/master/LICENSE")]
    public class PackIconVaadinIcons : PackIconControlBase
    {
        public static readonly DependencyProperty KindProperty
            = DependencyProperty.Register(nameof(Kind), typeof(PackIconVaadinIconsKind), typeof(PackIconVaadinIcons), new PropertyMetadata(default(PackIconVaadinIconsKind), KindPropertyChangedCallback));

        private static void KindPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != e.OldValue)
            {
                ((PackIconVaadinIcons)dependencyObject).UpdateData();
            }
        }

        /// <summary>
        /// Gets or sets the icon to display.
        /// </summary>
        public PackIconVaadinIconsKind Kind
        {
            get { return (PackIconVaadinIconsKind)GetValue(KindProperty); }
            set { SetValue(KindProperty, value); }
        }

#if !(NETFX_CORE || WINDOWS_UWP)
        static PackIconVaadinIcons()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PackIconVaadinIcons), new FrameworkPropertyMetadata(typeof(PackIconVaadinIcons)));
        }
#endif

        public PackIconVaadinIcons()
        {
#if NETFX_CORE || WINDOWS_UWP
            this.DefaultStyleKey = typeof(PackIconVaadinIcons);
#endif
        }

        protected override void SetKind<TKind>(TKind iconKind)
        {
#if NETFX_CORE || WINDOWS_UWP
            BindingOperations.SetBinding(this, PackIconVaadinIcons.KindProperty, new Binding() { Source = iconKind, Mode = BindingMode.OneTime });
#else
            this.SetCurrentValue(KindProperty, iconKind);
#endif
        }

        protected override void UpdateData()
        {
            if (Kind != default(PackIconVaadinIconsKind))
            {
                string data = null;
                PackIconVaadinIconsDataFactory.DataIndex.Value?.TryGetValue(Kind, out data);
                this.Data = data;
            }
            else
            {
                this.Data = null;
            }
        }
    }
}