
#if !(NETFX_CORE || WINDOWS_UWP)
using System.Windows;
#endif

namespace MahApps.Metro.IconPacks
{
    /// <summary>
    /// The PICOL icons are licensed under [Artistic License 2.0, Attribution 3.0 Unported (CC BY 3.0)](<see><cref>https://creativecommons.org/licenses/by/3.0/</cref></see>).
    /// Contributions, corrections and requests can be made on GitHub <see><cref>https://github.com/PicolSigns</cref></see>.
    /// </summary>
    public class PackIconPicolIcons : PackIconControl<PackIconPicolIconsKind>
    {
#if !(NETFX_CORE || WINDOWS_UWP)
        static PackIconPicolIcons()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PackIconPicolIcons), new FrameworkPropertyMetadata(typeof(PackIconPicolIcons)));
        }
#endif

        public PackIconPicolIcons() : base(PackIconPicolIconsDataFactory.Create)
        {
#if NETFX_CORE || WINDOWS_UWP
            this.DefaultStyleKey = typeof(PackIconPicolIcons);
#endif
        }
    }
}