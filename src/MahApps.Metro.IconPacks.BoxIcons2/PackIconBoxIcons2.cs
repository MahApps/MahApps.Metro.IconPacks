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
    /// BoxIcons licensed under [MIT](<see><cref>https://v2.boxicons.com/usage#license</cref></see>)
    /// Contributions, corrections and requests can be made on GitHub <see><cref>https://github.com/atisawd/boxicons</cref></see>.
    /// </summary>
    [MetaData("Boxicons v2", "https://v2.boxicons.com/", "https://v2.boxicons.com/usage#license")]
    public class PackIconBoxIcons2 : PackIconControlBase
    {
        public static readonly DependencyProperty KindProperty
            = DependencyProperty.Register(nameof(Kind), typeof(PackIconBoxIcons2Kind), typeof(PackIconBoxIcons2), new PropertyMetadata(default(PackIconBoxIcons2Kind), KindPropertyChangedCallback));

        private static void KindPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != e.OldValue)
            {
                ((PackIconBoxIcons2)dependencyObject).UpdateData();
            }
        }

        /// <summary>
        /// Gets or sets the icon to display.
        /// </summary>
        public PackIconBoxIcons2Kind Kind
        {
            get { return (PackIconBoxIcons2Kind)GetValue(KindProperty); }
            set { SetValue(KindProperty, value); }
        }

#if !(NETFX_CORE || WINDOWS_UWP)
        static PackIconBoxIcons2()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PackIconBoxIcons2), new FrameworkPropertyMetadata(typeof(PackIconBoxIcons2)));
        }
#endif

        public PackIconBoxIcons2()
        {
#if NETFX_CORE || WINDOWS_UWP
            this.DefaultStyleKey = typeof(PackIconBoxIcons2);
#endif
        }

        protected override void SetKind<TKind>(TKind iconKind)
        {
#if NETFX_CORE || WINDOWS_UWP
            BindingOperations.SetBinding(this, PackIconBoxIcons2.KindProperty, new Binding() { Source = iconKind, Mode = BindingMode.OneTime });
#else
            this.SetCurrentValue(KindProperty, iconKind);
#endif
        }

        protected override void UpdateData()
        {
            if (Kind != default(PackIconBoxIcons2Kind))
            {
                string data = null;
                PackIconDataFactory<PackIconBoxIcons2Kind>.DataIndex.Value?.TryGetValue(Kind, out data);
                this.Data = data;
            }
            else
            {
                this.Data = null;
            }
        }
    }
}