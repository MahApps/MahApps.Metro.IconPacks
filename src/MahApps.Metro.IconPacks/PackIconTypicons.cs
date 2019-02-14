
#if !(NETFX_CORE || WINDOWS_UWP)
using System.Windows;
#endif

namespace MahApps.Metro.IconPacks
{
    /// <summary>
    /// Typicons Icons/Artwork distributed under [CC BY-SA](<see><cref>https://creativecommons.org/licenses/by-sa/3.0/</cref></see>) licence.
    /// Typicons Font distributed under 'SIL Open Font License' licence.
    /// </summary>
    public class PackIconTypicons : PackIconControl<PackIconTypiconsKind>
    {
#if !(NETFX_CORE || WINDOWS_UWP)
        static PackIconTypicons()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PackIconTypicons), new FrameworkPropertyMetadata(typeof(PackIconTypicons)));
        }
#endif

        public PackIconTypicons() : base(PackIconTypiconsDataFactory.Create)
        {
#if NETFX_CORE || WINDOWS_UWP
            this.DefaultStyleKey = typeof(PackIconTypicons);
#endif
        }
    }
}