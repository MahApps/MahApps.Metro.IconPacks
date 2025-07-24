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
    /// MynaUI Icons are licensed under [MIT license](<see><cref>https://github.com/praveenjuge/mynaui-icons?tab=MIT-1-ov-file</cref></see>).
    /// Contributions, corrections and requests can be made on GitHub <see><cref>https://github.com/praveenjuge/mynaui-icons</cref></see>.
    /// </summary>
    [MetaData("MynaUI Icons", "https://github.com/praveenjuge/mynaui-icons", "https://github.com/praveenjuge/mynaui-icons?tab=MIT-1-ov-file")]
    public class PackIconMynaUIIcons : PackIconControlBase
    {
        public static readonly DependencyProperty KindProperty
            = DependencyProperty.Register(nameof(Kind), typeof(PackIconMynaUIIconsKind), typeof(PackIconMynaUIIcons), new PropertyMetadata(default(PackIconMynaUIIconsKind), KindPropertyChangedCallback));

        private static void KindPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != e.OldValue)
            {
                ((PackIconMynaUIIcons)dependencyObject).UpdateData();
            }
        }

        /// <summary>
        /// Gets or sets the icon to display.
        /// </summary>
        public PackIconMynaUIIconsKind Kind
        {
            get { return (PackIconMynaUIIconsKind)GetValue(KindProperty); }
            set { SetValue(KindProperty, value); }
        }

#if !(NETFX_CORE || WINDOWS_UWP)
        static PackIconMynaUIIcons()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PackIconMynaUIIcons), new FrameworkPropertyMetadata(typeof(PackIconMynaUIIcons)));
        }
#endif

        public PackIconMynaUIIcons()
        {
#if NETFX_CORE || WINDOWS_UWP
            this.DefaultStyleKey = typeof(PackIconMynaUIIcons);
#endif
        }

        protected override void SetKind<TKind>(TKind iconKind)
        {
#if NETFX_CORE || WINDOWS_UWP
            BindingOperations.SetBinding(this, PackIconMynaUIIcons.KindProperty, new Binding() { Source = iconKind, Mode = BindingMode.OneTime });
#else
            this.SetCurrentValue(KindProperty, iconKind);
#endif
        }

        protected override void UpdateData()
        {
            if (Kind != default(PackIconMynaUIIconsKind))
            {
                string data = null;
                PackIconDataFactory<PackIconMynaUIIconsKind>.DataIndex.Value?.TryGetValue(Kind, out data);
                this.Data = data;
            }
            else
            {
                this.Data = null;
            }
        }
    }
}