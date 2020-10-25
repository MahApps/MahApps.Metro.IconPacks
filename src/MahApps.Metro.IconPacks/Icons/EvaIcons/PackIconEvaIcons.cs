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
    /// eva-icons licensed under the MIT License <see><cref>https://github.com/akveo/eva-icons/blob/master/LICENSE.txt</cref></see>
    /// Contributions, corrections and requests can be made on GitHub <see><cref>https://github.com/akveo/eva-icons</cref></see>.
    /// </summary>
    [MetaData("Eva Icons", "https://akveo.github.io/eva-icons/", "https://github.com/akveo/eva-icons/blob/master/LICENSE.txt")]
    public class PackIconEvaIcons : PackIconControlBase
    {
        public static readonly DependencyProperty KindProperty
            = DependencyProperty.Register(nameof(Kind), typeof(PackIconEvaIconsKind), typeof(PackIconEvaIcons), new PropertyMetadata(default(PackIconEvaIconsKind), KindPropertyChangedCallback));

        private static void KindPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != e.OldValue)
            {
                ((PackIconEvaIcons)dependencyObject).UpdateData();
            }
        }

        /// <summary>
        /// Gets or sets the icon to display.
        /// </summary>
        public PackIconEvaIconsKind Kind
        {
            get { return (PackIconEvaIconsKind)GetValue(KindProperty); }
            set { SetValue(KindProperty, value); }
        }

#if !(NETFX_CORE || WINDOWS_UWP)
        static PackIconEvaIcons()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PackIconEvaIcons), new FrameworkPropertyMetadata(typeof(PackIconEvaIcons)));
        }
#endif

        public PackIconEvaIcons()
        {
#if NETFX_CORE || WINDOWS_UWP
            this.DefaultStyleKey = typeof(PackIconEvaIcons);
#endif
        }

        protected override void SetKind<TKind>(TKind iconKind)
        {
#if NETFX_CORE || WINDOWS_UWP
            BindingOperations.SetBinding(this, PackIconEvaIcons.KindProperty, new Binding() { Source = iconKind, Mode = BindingMode.OneTime });
#else
            this.SetCurrentValue(KindProperty, iconKind);
#endif
        }

        protected override void UpdateData()
        {
            if (Kind != default(PackIconEvaIconsKind))
            {
                string data = null;
                PackIconEvaIconsDataFactory.DataIndex.Value?.TryGetValue(Kind, out data);
                this.Data = data;
            }
            else
            {
                this.Data = null;
            }
        }
    }
}