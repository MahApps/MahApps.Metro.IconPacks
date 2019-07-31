
#if !(NETFX_CORE || WINDOWS_UWP)
using System.Windows;
#endif

namespace MahApps.Metro.IconPacks
{
    /// <summary>
    /// All icons sourced from GitHub Octicons - <see><cref>https://octicons.github.com</cref></see> - 
    /// <see><cref>https://github.com/primer/octicons/blob/master/LICENSE</cref></see>.
    /// </summary>
    public class PackIconOcticons : PackIconControl<PackIconOcticonsKind>
    {
#if !(NETFX_CORE || WINDOWS_UWP)
        static PackIconOcticons()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PackIconOcticons), new FrameworkPropertyMetadata(typeof(PackIconOcticons)));
        }
#endif

        public PackIconOcticons() : base(PackIconOcticonsDataFactory.Create)
        {
#if NETFX_CORE || WINDOWS_UWP
            this.DefaultStyleKey = typeof(PackIconOcticons);
#endif
        }
    }
}