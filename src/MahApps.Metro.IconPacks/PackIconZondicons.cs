
#if !(NETFX_CORE || WINDOWS_UWP)
using System.Windows;
#endif

namespace MahApps.Metro.IconPacks
{
    /// <summary>
    /// Zondicons is licensed under the [CC BY 4.0](<see><cref>https://creativecommons.org/licenses/by/4.0/</cref></see>).
    /// Zondicons are availabe at <see><cref>https://www.zondicons.com/</cref></see>.
    /// </summary>
    public class PackIconZondicons : PackIconControl<PackIconZondiconsKind>
    {
#if !(NETFX_CORE || WINDOWS_UWP)
        static PackIconZondicons()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PackIconZondicons), new FrameworkPropertyMetadata(typeof(PackIconZondicons)));
        }
#endif

        public PackIconZondicons() : base(PackIconZondiconsDataFactory.Create)
        {
#if NETFX_CORE || WINDOWS_UWP
            this.DefaultStyleKey = typeof(PackIconZondicons);
#endif
        }
    }
}