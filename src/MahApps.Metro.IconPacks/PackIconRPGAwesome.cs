
#if !(NETFX_CORE || WINDOWS_UWP)
using System.Windows;
#endif

namespace MahApps.Metro.IconPacks
{
    /// <summary>
    /// The Rpg Awesome font is licensed under the [SIL OFL 1.1](<see><cref>http://scripts.sil.org/OFL</cref></see>)
    /// Contributions, corrections and requests can be made on GitHub <see><cref>https://github.com/nagoshiashumari/Rpg-Awesome</cref></see>.
    /// </summary>
    public class PackIconRPGAwesome : PackIconControl<PackIconRPGAwesomeKind>
    {
#if !(NETFX_CORE || WINDOWS_UWP)
        static PackIconRPGAwesome()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PackIconRPGAwesome), new FrameworkPropertyMetadata(typeof(PackIconRPGAwesome)));
        }
#endif

        public PackIconRPGAwesome() : base(PackIconRPGAwesomeDataFactory.Create)
        {
#if NETFX_CORE || WINDOWS_UWP
            this.DefaultStyleKey = typeof(PackIconRPGAwesome);
#endif
        }
    }
}