
#if !(NETFX_CORE || WINDOWS_UWP)
using System.Windows;
#endif

namespace MahApps.Metro.IconPacks
{
    /// <summary>
    /// BoxIcons licensed under [SIL OFL 1.1](<see><cref>http://scripts.sil.org/OFL</cref></see>)
    /// Contributions, corrections and requests can be made on GitHub <see><cref>https://github.com/atisawd/boxicons</cref></see>.
    /// </summary>
    public class PackIconBoxIcons : PackIconControl<PackIconBoxIconsKind>
    {
#if !(NETFX_CORE || WINDOWS_UWP)
        static PackIconBoxIcons()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PackIconBoxIcons), new FrameworkPropertyMetadata(typeof(PackIconBoxIcons)));
        }
#endif

        public PackIconBoxIcons() : base(PackIconBoxIconsDataFactory.Create)
        {
#if NETFX_CORE || WINDOWS_UWP
            this.DefaultStyleKey = typeof(PackIconBoxIcons);
#endif
        }
    }
}