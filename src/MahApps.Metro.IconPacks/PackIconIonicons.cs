
#if !NETFX_CORE
using System.Windows;
#endif

namespace MahApps.Metro.IconPacks
{
    /// <summary>
    /// Ionicons is licensed under the [MIT license](<see><cref>https://github.com/ionic-team/ionicons/blob/master/LICENSE</cref></see>).
    /// Contributions, corrections and requests can be made on GitHub <see><cref>https://github.com/ionic-team/ionicons</cref></see>.
    /// </summary>
    public class PackIconIonicons : PackIconControl<PackIconIoniconsKind>
    {
#if !NETFX_CORE
        static PackIconIonicons()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PackIconIonicons), new FrameworkPropertyMetadata(typeof(PackIconIonicons)));
        }
#endif

        public PackIconIonicons() : base(PackIconIoniconsDataFactory.Create)
        {
#if NETFX_CORE
            this.DefaultStyleKey = typeof(PackIconIonicons);
#endif
        }
    }
}