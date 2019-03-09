
#if !(NETFX_CORE || WINDOWS_UWP)
using System.Windows;
#endif

namespace MahApps.Metro.IconPacks
{
    /// <summary>
    /// Unicons are Open Source icons and licensed under [Apache 2.0](<see><cref>https://www.apache.org/licenses/LICENSE-2.0.txt</cref></see>). You're free to use these icons in your personal and commercial project. We would love to see the attribution in your app's **about** screen, but it's not mandatory.
    /// Contributions, corrections and requests can be made on GitHub <see><cref>https://github.com/Iconscout/unicons</cref></see>.
    /// </summary>
    public class PackIconUnicons : PackIconControl<PackIconUniconsKind>
    {
#if !(NETFX_CORE || WINDOWS_UWP)
        static PackIconUnicons()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PackIconUnicons), new FrameworkPropertyMetadata(typeof(PackIconUnicons)));
        }
#endif

        public PackIconUnicons() : base(PackIconUniconsDataFactory.Create)
        {
#if NETFX_CORE || WINDOWS_UWP
            this.DefaultStyleKey = typeof(PackIconUnicons);
#endif
        }
    }
}