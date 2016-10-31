
#if !NETFX_CORE
using System.Windows;
#endif

namespace MahApps.Metro.IconPacks
{
    /// <summary>
    /// Icons from GitHub Octicons - <see><cref>https://octicons.github.com</cref></see>
    /// </summary>
    public class PackIconOcticons : PackIcon<PackIconOcticonsKind>
    {
#if !NETFX_CORE
        static PackIconOcticons()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PackIconOcticons), new FrameworkPropertyMetadata(typeof(PackIconOcticons)));
        }
#endif

        public PackIconOcticons() : base(PackIconOcticonsDataFactory.Create)
        {
#if NETFX_CORE
            this.DefaultStyleKey = typeof(PackIconOcticons);
#endif
        }
    }
}