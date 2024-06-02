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
    /// File Icons are licensed under the [MIT license](<see><cref>https://github.com/file-icons/atom/blob/master/LICENSE.md</cref></see>).
    /// Contributions, corrections and requests can be made on GitHub <see><cref>https://github.com/file-icons/icons</cref></see>.
    /// </summary>
    [MetaData("File Icons", "https://github.com/file-icons/icons", "https://github.com/file-icons/atom/blob/master/LICENSE.md")]
    public class PackIconFileIcons : PackIconControlBase
    {
        public static readonly DependencyProperty KindProperty
            = DependencyProperty.Register(nameof(Kind), typeof(PackIconFileIconsKind), typeof(PackIconFileIcons), new PropertyMetadata(default(PackIconFileIconsKind), KindPropertyChangedCallback));

        private static void KindPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != e.OldValue)
            {
                ((PackIconFileIcons)dependencyObject).UpdateData();
            }
        }

        /// <summary>
        /// Gets or sets the icon to display.
        /// </summary>
        public PackIconFileIconsKind Kind
        {
            get { return (PackIconFileIconsKind)GetValue(KindProperty); }
            set { SetValue(KindProperty, value); }
        }

#if !(NETFX_CORE || WINDOWS_UWP)
        static PackIconFileIcons()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PackIconFileIcons), new FrameworkPropertyMetadata(typeof(PackIconFileIcons)));
        }
#endif

        public PackIconFileIcons()
        {
#if NETFX_CORE || WINDOWS_UWP
            this.DefaultStyleKey = typeof(PackIconFileIcons);
#endif
        }

        protected override void SetKind<TKind>(TKind iconKind)
        {
#if NETFX_CORE || WINDOWS_UWP
            BindingOperations.SetBinding(this, PackIconFileIcons.KindProperty, new Binding() { Source = iconKind, Mode = BindingMode.OneTime });
#else
            this.SetCurrentValue(KindProperty, iconKind);
#endif
        }

        protected override void UpdateData()
        {
            if (Kind != default(PackIconFileIconsKind))
            {
                string data = null;
                PackIconFileIconsDataFactory.DataIndex.Value?.TryGetValue(Kind, out data);
                this.Data = data;
            }
            else
            {
                this.Data = null;
            }
        }
    }
}