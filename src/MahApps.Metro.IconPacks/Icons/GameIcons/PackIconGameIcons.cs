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
    /// All icons sourced from Game Icons <see><cref>https://github.com/game-icons/icons</cref></see>
    /// In accordance of <see><cref>https://github.com/game-icons/icons?tab=License-1-ov-file#readme</cref></see>.
    /// </summary>
    [MetaData("Game Icons", "https://github.com/game-icons/icons", "https://github.com/game-icons/icons?tab=License-1-ov-file#readme")]
    public class PackIconGameIcons : PackIconControlBase
    {
        public static readonly DependencyProperty KindProperty
            = DependencyProperty.Register(nameof(Kind), typeof(PackIconGameIconsKind), typeof(PackIconGameIcons), new PropertyMetadata(default(PackIconGameIconsKind), KindPropertyChangedCallback));

        private static void KindPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != e.OldValue)
            {
                ((PackIconGameIcons)dependencyObject).UpdateData();
            }
        }

        /// <summary>
        /// Gets or sets the icon to display.
        /// </summary>
        public PackIconGameIconsKind Kind
        {
            get { return (PackIconGameIconsKind)GetValue(KindProperty); }
            set { SetValue(KindProperty, value); }
        }

#if !(NETFX_CORE || WINDOWS_UWP)
        static PackIconGameIcons()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PackIconGameIcons), new FrameworkPropertyMetadata(typeof(PackIconGameIcons)));
        }
#endif

        public PackIconGameIcons()
        {
#if NETFX_CORE || WINDOWS_UWP
            this.DefaultStyleKey = typeof(PackIconGameIcons);
#endif
        }

        protected override void SetKind<TKind>(TKind iconKind)
        {
#if NETFX_CORE || WINDOWS_UWP
            BindingOperations.SetBinding(this, PackIconGameIcons.KindProperty, new Binding() { Source = iconKind, Mode = BindingMode.OneTime });
#else
            this.SetCurrentValue(KindProperty, iconKind);
#endif
        }

        protected override void UpdateData()
        {
            if (Kind != default(PackIconGameIconsKind))
            {
                string data = null;
                PackIconGameIconsDataFactory.DataIndex.Value?.TryGetValue(Kind, out data);
                this.Data = data;
            }
            else
            {
                this.Data = null;
            }
        }
    }
}