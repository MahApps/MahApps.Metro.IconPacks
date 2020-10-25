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
    /// Typicons Icons/Artwork distributed under [CC BY-SA](<see><cref>https://creativecommons.org/licenses/by-sa/3.0/</cref></see>) license.
    /// Typicons Font distributed under 'SIL Open Font License' license.
    /// </summary>
    [MetaData("Typicons", "https://www.s-ings.com/typicons/", "https://creativecommons.org/licenses/by-sa/3.0/")]
    public class PackIconTypicons : PackIconControlBase
    {
        public static readonly DependencyProperty KindProperty
            = DependencyProperty.Register(nameof(Kind), typeof(PackIconTypiconsKind), typeof(PackIconTypicons), new PropertyMetadata(default(PackIconTypiconsKind), KindPropertyChangedCallback));

        private static void KindPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != e.OldValue)
            {
                ((PackIconTypicons)dependencyObject).UpdateData();
            }
        }

        /// <summary>
        /// Gets or sets the icon to display.
        /// </summary>
        public PackIconTypiconsKind Kind
        {
            get { return (PackIconTypiconsKind)GetValue(KindProperty); }
            set { SetValue(KindProperty, value); }
        }

#if !(NETFX_CORE || WINDOWS_UWP)
        static PackIconTypicons()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PackIconTypicons), new FrameworkPropertyMetadata(typeof(PackIconTypicons)));
        }
#endif

        public PackIconTypicons()
        {
#if NETFX_CORE || WINDOWS_UWP
            this.DefaultStyleKey = typeof(PackIconTypicons);
#endif
        }

        protected override void SetKind<TKind>(TKind iconKind)
        {
#if NETFX_CORE || WINDOWS_UWP
            BindingOperations.SetBinding(this, PackIconTypicons.KindProperty, new Binding() { Source = iconKind, Mode = BindingMode.OneTime });
#else
            this.SetCurrentValue(KindProperty, iconKind);
#endif
        }

        protected override void UpdateData()
        {
            if (Kind != default(PackIconTypiconsKind))
            {
                string data = null;
                PackIconTypiconsDataFactory.DataIndex.Value?.TryGetValue(Kind, out data);
                this.Data = data;
            }
            else
            {
                this.Data = null;
            }
        }
    }
}