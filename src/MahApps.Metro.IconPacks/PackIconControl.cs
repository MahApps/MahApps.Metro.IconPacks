using System;
using System.Collections.Generic;
using System.Linq;
#if NETFX_CORE || WINDOWS_UWP
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
#else
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
#endif

namespace MahApps.Metro.IconPacks
{
    public class PackIconControl : PackIconControl<Enum>
    {
#if !(NETFX_CORE || WINDOWS_UWP)
        static PackIconControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PackIconControl), new FrameworkPropertyMetadata(typeof(PackIconControl)));
        }
#endif

        public PackIconControl() : base(PackIconControlDataFactory.Create)
        {
#if NETFX_CORE || WINDOWS_UWP
            this.DefaultStyleKey = typeof(PackIconControl);
#endif
        }
    }
}