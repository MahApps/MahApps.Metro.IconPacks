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
    /// Lucide Icons are licensed under [ISC License](<see><cref>https://github.com/lucide-icons/lucide?tab=ISC-1-ov-file#readme</cref></see>).
    /// Contributions, corrections and requests can be made on GitHub <see><cref>https://github.com/lucide-icons/lucide</cref></see>.
    /// </summary>
    [MetaData("Lucide Icons", "https://github.com/lucide-icons/lucide", "https://github.com/lucide-icons/lucide?tab=ISC-1-ov-file#readme")]
    public class PackIconLucide : PackIconControlBase
    {
        public static readonly DependencyProperty KindProperty
            = DependencyProperty.Register(nameof(Kind), typeof(PackIconLucideKind), typeof(PackIconLucide), new PropertyMetadata(default(PackIconLucideKind), KindPropertyChangedCallback));

        private static void KindPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != e.OldValue)
            {
                ((PackIconLucide)dependencyObject).UpdateData();
            }
        }

        /// <summary>
        /// Gets or sets the icon to display.
        /// </summary>
        public PackIconLucideKind Kind
        {
            get { return (PackIconLucideKind)GetValue(KindProperty); }
            set { SetValue(KindProperty, value); }
        }

#if !(NETFX_CORE || WINDOWS_UWP)
        static PackIconLucide()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PackIconLucide), new FrameworkPropertyMetadata(typeof(PackIconLucide)));
        }
#endif

        public PackIconLucide()
        {
#if NETFX_CORE || WINDOWS_UWP
            this.DefaultStyleKey = typeof(PackIconLucide);
#endif
        }

        protected override void SetKind<TKind>(TKind iconKind)
        {
#if NETFX_CORE || WINDOWS_UWP
            BindingOperations.SetBinding(this, PackIconLucide.KindProperty, new Binding() { Source = iconKind, Mode = BindingMode.OneTime });
#else
            this.SetCurrentValue(KindProperty, iconKind);
#endif
        }

        protected override void UpdateData()
        {
            if (Kind != default(PackIconLucideKind))
            {
                string data = null;
                PackIconDataFactory<PackIconLucideKind>.DataIndex.Value?.TryGetValue(Kind, out data);
                this.Data = data;
            }
            else
            {
                this.Data = null;
            }
        }
    }
}