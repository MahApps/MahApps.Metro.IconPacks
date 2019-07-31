
#if !(NETFX_CORE || WINDOWS_UWP)
using System.Windows;
#endif

namespace MahApps.Metro.IconPacks
{
    /// <summary>
    /// All icons sourced from Entypo+ Icons Font <see><cref>http://www.entypo.com</cref></see>
    /// Licensed under <see><cref>http://creativecommons.org/licenses/by-sa/4.0/</cref></see>.
    /// </summary>
    public class PackIconEntypo : PackIconControl<PackIconEntypoKind>
    {
#if !(NETFX_CORE || WINDOWS_UWP)
        static PackIconEntypo()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PackIconEntypo), new FrameworkPropertyMetadata(typeof(PackIconEntypo)));
        }
#endif

        public PackIconEntypo() : base(PackIconEntypoDataFactory.Create)
        {
#if NETFX_CORE || WINDOWS_UWP
            this.DefaultStyleKey = typeof(PackIconEntypo);
#endif
        }
    }
}