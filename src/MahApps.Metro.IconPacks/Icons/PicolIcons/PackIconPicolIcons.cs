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
    /// The PICOL icons are licensed under [Artistic License 2.0, Attribution 3.0 Unported (CC BY 3.0)](<see><cref>https://creativecommons.org/licenses/by/3.0/</cref></see>).
    /// Contributions, corrections and requests can be made on GitHub <see><cref>https://github.com/PicolSigns/Icons</cref></see>.
    /// </summary>
    [MetaData("PICOL Icons", "https://github.com/PicolSigns/Icons", "https://github.com/PicolSigns/Icons/blob/master/LICENSE")]
    public class PackIconPicolIcons : PackIconControlBase
    {
        public static readonly DependencyProperty KindProperty
            = DependencyProperty.Register(nameof(Kind), typeof(PackIconPicolIconsKind), typeof(PackIconPicolIcons), new PropertyMetadata(default(PackIconPicolIconsKind), KindPropertyChangedCallback));

        private static void KindPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != e.OldValue)
            {
                ((PackIconPicolIcons)dependencyObject).UpdateData();
            }
        }

        /// <summary>
        /// Gets or sets the icon to display.
        /// </summary>
        public PackIconPicolIconsKind Kind
        {
            get { return (PackIconPicolIconsKind)GetValue(KindProperty); }
            set { SetValue(KindProperty, value); }
        }

#if !(NETFX_CORE || WINDOWS_UWP)
        static PackIconPicolIcons()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PackIconPicolIcons), new FrameworkPropertyMetadata(typeof(PackIconPicolIcons)));
        }
#endif

        public PackIconPicolIcons()
        {
#if NETFX_CORE || WINDOWS_UWP
            this.DefaultStyleKey = typeof(PackIconPicolIcons);
#endif
        }

        protected override void SetKind<TKind>(TKind iconKind)
        {
#if NETFX_CORE || WINDOWS_UWP
            BindingOperations.SetBinding(this, PackIconPicolIcons.KindProperty, new Binding() { Source = iconKind, Mode = BindingMode.OneTime });
#else
            this.SetCurrentValue(KindProperty, iconKind);
#endif
        }

        protected override void UpdateData()
        {
            if (Kind != default(PackIconPicolIconsKind))
            {
                string data = null;
                PackIconPicolIconsDataFactory.DataIndex.Value?.TryGetValue(Kind, out data);
                this.Data = data;
            }
            else
            {
                this.Data = null;
            }
        }
    }
}