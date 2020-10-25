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
    /// Fontaudio by @fefanto <see><cref>https://github.com/fefanto</cref></see>. License: Icons: CC BY 4.0, Fonts: SIL OFL 1.1, Code: MIT License. <see><cref>https://github.com/fefanto/fontaudio#license</cref></see>
    /// Contributions, corrections and requests can be made on GitHub <see><cref>https://github.com/fefanto/fontaudio</cref></see>.
    /// </summary>
    [MetaData("Fontaudio", "https://github.com/fefanto/fontaudio", "https://github.com/fefanto/fontaudio#license")]
    public class PackIconFontaudio : PackIconControlBase
    {
        public static readonly DependencyProperty KindProperty
            = DependencyProperty.Register(nameof(Kind), typeof(PackIconFontaudioKind), typeof(PackIconFontaudio), new PropertyMetadata(default(PackIconFontaudioKind), KindPropertyChangedCallback));

        private static void KindPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != e.OldValue)
            {
                ((PackIconFontaudio)dependencyObject).UpdateData();
            }
        }

        /// <summary>
        /// Gets or sets the icon to display.
        /// </summary>
        public PackIconFontaudioKind Kind
        {
            get { return (PackIconFontaudioKind)GetValue(KindProperty); }
            set { SetValue(KindProperty, value); }
        }

#if !(NETFX_CORE || WINDOWS_UWP)
        static PackIconFontaudio()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PackIconFontaudio), new FrameworkPropertyMetadata(typeof(PackIconFontaudio)));
        }
#endif

        public PackIconFontaudio()
        {
#if NETFX_CORE || WINDOWS_UWP
            this.DefaultStyleKey = typeof(PackIconFontaudio);
#endif
        }

        protected override void SetKind<TKind>(TKind iconKind)
        {
#if NETFX_CORE || WINDOWS_UWP
            BindingOperations.SetBinding(this, PackIconFontaudio.KindProperty, new Binding() { Source = iconKind, Mode = BindingMode.OneTime });
#else
            this.SetCurrentValue(KindProperty, iconKind);
#endif
        }

        protected override void UpdateData()
        {
            if (Kind != default(PackIconFontaudioKind))
            {
                string data = null;
                PackIconFontaudioDataFactory.DataIndex.Value?.TryGetValue(Kind, out data);
                this.Data = data;
            }
            else
            {
                this.Data = null;
            }
        }
    }
}