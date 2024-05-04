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
    /// All icons sourced from GitHub Octicons <see><cref>https://github.com/Klarr-Agency/Circum-Icons</cref></see>
    /// In accordance of <see><cref>https://github.com/Klarr-Agency/Circum-Icons?tab=MPL-2.0-1-ov-file</cref></see>.
    /// </summary>
    [MetaData("Circum Icons Free", "https://github.com/Klarr-Agency/Circum-Icons", "https://github.com/Klarr-Agency/Circum-Icons?tab=MPL-2.0-1-ov-file")]
    public class PackIconCircumIcons : PackIconControlBase
    {
        public static readonly DependencyProperty KindProperty
            = DependencyProperty.Register(nameof(Kind), typeof(PackIconCircumIconsKind), typeof(PackIconCircumIcons), new PropertyMetadata(default(PackIconCircumIconsKind), KindPropertyChangedCallback));

        private static void KindPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != e.OldValue)
            {
                ((PackIconCircumIcons)dependencyObject).UpdateData();
            }
        }

        /// <summary>
        /// Gets or sets the icon to display.
        /// </summary>
        public PackIconCircumIconsKind Kind
        {
            get { return (PackIconCircumIconsKind)GetValue(KindProperty); }
            set { SetValue(KindProperty, value); }
        }

#if !(NETFX_CORE || WINDOWS_UWP)
        static PackIconCircumIcons()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PackIconCircumIcons), new FrameworkPropertyMetadata(typeof(PackIconCircumIcons)));
        }
#endif

        public PackIconCircumIcons()
        {
#if NETFX_CORE || WINDOWS_UWP
            this.DefaultStyleKey = typeof(PackIconCircumIcons);
#endif
        }

        protected override void SetKind<TKind>(TKind iconKind)
        {
#if NETFX_CORE || WINDOWS_UWP
            BindingOperations.SetBinding(this, PackIconCircumIcons.KindProperty, new Binding() { Source = iconKind, Mode = BindingMode.OneTime });
#else
            this.SetCurrentValue(KindProperty, iconKind);
#endif
        }

        protected override void UpdateData()
        {
            if (Kind != default(PackIconCircumIconsKind))
            {
                string data = null;
                PackIconCircumIconsDataFactory.DataIndex.Value?.TryGetValue(Kind, out data);
                this.Data = data;
            }
            else
            {
                this.Data = null;
            }
        }
    }
}