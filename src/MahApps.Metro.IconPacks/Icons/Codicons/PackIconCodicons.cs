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
    /// Codicons are licensed under the [Creative Commons Attribution 4.0 International Public License](<see><cref>https://github.com/microsoft/vscode-codicons/blob/main/LICENSE</cref></see>).
    /// Contributions, corrections and requests can be made on GitHub <see><cref>https://github.com/microsoft/vscode-codicons</cref></see>.
    /// </summary>
    [MetaData("Codicons", "https://github.com/microsoft/vscode-codicons", "https://github.com/microsoft/vscode-codicons/blob/main/LICENSE")]
    public class PackIconCodicons : PackIconControlBase
    {
        public static readonly DependencyProperty KindProperty
            = DependencyProperty.Register(nameof(Kind), typeof(PackIconCodiconsKind), typeof(PackIconCodicons), new PropertyMetadata(default(PackIconCodiconsKind), KindPropertyChangedCallback));

        private static void KindPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != e.OldValue)
            {
                ((PackIconCodicons)dependencyObject).UpdateData();
            }
        }

        /// <summary>
        /// Gets or sets the icon to display.
        /// </summary>
        public PackIconCodiconsKind Kind
        {
            get { return (PackIconCodiconsKind)GetValue(KindProperty); }
            set { SetValue(KindProperty, value); }
        }

#if !(NETFX_CORE || WINDOWS_UWP)
        static PackIconCodicons()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PackIconCodicons), new FrameworkPropertyMetadata(typeof(PackIconCodicons)));
        }
#endif

        public PackIconCodicons()
        {
#if NETFX_CORE || WINDOWS_UWP
            this.DefaultStyleKey = typeof(PackIconCodicons);
#endif
        }

        protected override void SetKind<TKind>(TKind iconKind)
        {
#if NETFX_CORE || WINDOWS_UWP
            BindingOperations.SetBinding(this, PackIconCodicons.KindProperty, new Binding() { Source = iconKind, Mode = BindingMode.OneTime });
#else
            this.SetCurrentValue(KindProperty, iconKind);
#endif
        }

        protected override void UpdateData()
        {
            if (Kind != default(PackIconCodiconsKind))
            {
                string data = null;
                PackIconCodiconsDataFactory.DataIndex.Value?.TryGetValue(Kind, out data);
                this.Data = data;
            }
            else
            {
                this.Data = null;
            }
        }
    }
}