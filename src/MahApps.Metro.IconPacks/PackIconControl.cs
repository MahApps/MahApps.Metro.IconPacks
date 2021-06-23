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
    /// </summary>
    public class PackIconControl : PackIconControlBase
    {
        private static PropertyChangedCallback KindPropertyChangedCallback { get; } = (dependencyObject, e) =>
        {
            if (e.NewValue != e.OldValue)
            {
                ((PackIconControl)dependencyObject).UpdateData();
            }
        };

        public static readonly DependencyProperty KindProperty
            = DependencyProperty.Register(nameof(Kind), typeof(Enum), typeof(PackIconControl), new PropertyMetadata(default(Enum), KindPropertyChangedCallback));

        /// <summary>
        /// Gets or sets the icon to display.
        /// </summary>
        public Enum Kind
        {
            get { return (Enum)GetValue(KindProperty); }
            set { SetValue(KindProperty, value); }
        }

#if !(NETFX_CORE || WINDOWS_UWP)
        static PackIconControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PackIconControl), new FrameworkPropertyMetadata(typeof(PackIconControl)));
        }
#endif

        public PackIconControl()
        {
#if NETFX_CORE || WINDOWS_UWP
            this.DefaultStyleKey = typeof(PackIconControl);
#endif
        }

        protected override void SetKind<TKind>(TKind iconKind)
        {
#if NETFX_CORE || WINDOWS_UWP
            BindingOperations.SetBinding(this, PackIconControl.KindProperty, new Binding() { Source = iconKind, Mode = BindingMode.OneTime });
#else
            this.SetCurrentValue(KindProperty, iconKind);
#endif
        }

        protected override void UpdateData()
        {
            if (Kind != default(Enum))
            {
                string data = null;
                PackIconControlDataFactory.DataIndex.Value?.TryGetValue(Kind, out data);
                this.Data = data;
            }
            else
            {
                this.Data = null;
            }
        }
    }
}