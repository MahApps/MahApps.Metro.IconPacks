
#if !(NETFX_CORE || WINDOWS_UWP)
using System.Windows;
#endif

namespace MahApps.Metro.IconPacks
{
    /// <summary>
    /// Jam Icons licensed under the MIT License <see><cref>https://github.com/michaelampr/jam/blob/master/LICENSE</cref></see>
    /// Contributions, corrections and requests can be made on GitHub <see><cref>https://github.com/michaelampr/jam</cref></see>.
    /// </summary>
    public class PackIconJamIcons : PackIconControl<PackIconJamIconsKind>
    {
#if !(NETFX_CORE || WINDOWS_UWP)
        static PackIconJamIcons()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PackIconJamIcons), new FrameworkPropertyMetadata(typeof(PackIconJamIcons)));
        }
#endif

        public PackIconJamIcons() : base(PackIconJamIconsDataFactory.Create)
        {
#if NETFX_CORE || WINDOWS_UWP
            this.DefaultStyleKey = typeof(PackIconJamIcons);
#endif
        }
    }
}