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
    /// Feather is licensed under the MIT License <see><cref>https://github.com/feathericons/feather/blob/master/LICENSE</cref></see>
    /// Contributions, corrections and requests can be made on GitHub <see><cref>https://github.com/feathericons/feather</cref></see>.
    /// </summary>
    public class PackIconFeatherIcons : PackIconControlBase
    {
        private static Lazy<IDictionary<PackIconFeatherIconsKind, string>> _dataIndex;

        public static readonly DependencyProperty KindProperty
            = DependencyProperty.Register(nameof(Kind), typeof(PackIconFeatherIconsKind), typeof(PackIconFeatherIcons), new PropertyMetadata(default(PackIconFeatherIconsKind), KindPropertyChangedCallback));

        private static void KindPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != e.OldValue)
            {
                ((PackIconFeatherIcons)dependencyObject).UpdateData();
            }
        }

        /// <summary>
        /// Gets or sets the icon to display.
        /// </summary>
        public PackIconFeatherIconsKind Kind
        {
            get { return (PackIconFeatherIconsKind)GetValue(KindProperty); }
            set { SetValue(KindProperty, value); }
        }

#if !(NETFX_CORE || WINDOWS_UWP)
        static PackIconFeatherIcons()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PackIconFeatherIcons), new FrameworkPropertyMetadata(typeof(PackIconFeatherIcons)));
        }
#endif

        public PackIconFeatherIcons()
        {
#if NETFX_CORE || WINDOWS_UWP
            this.DefaultStyleKey = typeof(PackIconFeatherIcons);
#endif

            if (_dataIndex == null)
            {
                _dataIndex = new Lazy<IDictionary<PackIconFeatherIconsKind, string>>(PackIconFeatherIconsDataFactory.Create);
            }
        }

        protected override void SetKind<TKind>(TKind iconKind)
        {
#if NETFX_CORE || WINDOWS_UWP
            BindingOperations.SetBinding(this, PackIconFeatherIcons.KindProperty, new Binding() { Source = iconKind, Mode = BindingMode.OneTime });
#else
            this.SetCurrentValue(KindProperty, iconKind);
#endif
        }

        protected override void UpdateData()
        {
            if (Kind != default(PackIconFeatherIconsKind))
            {
                string data = null;
                _dataIndex.Value?.TryGetValue(Kind, out data);
                this.Data = data;
            }
            else
            {
                this.Data = null;
            }
        }
    }
}