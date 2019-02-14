
#if !(NETFX_CORE || WINDOWS_UWP)
using System.Windows;
#endif

namespace MahApps.Metro.IconPacks
{
    /// <summary>
    /// Icons from the Modern UI Icons project, <see><cref>http://modernuiicons.com</cref></see>.
    /// </summary>
    public class PackIconModern : PackIconControl<PackIconModernKind>
    {
#if !(NETFX_CORE || WINDOWS_UWP)
        static PackIconModern()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PackIconModern), new FrameworkPropertyMetadata(typeof(PackIconModern)));
        }
#endif

        public PackIconModern() : base(PackIconModernDataFactory.Create)
        {
#if NETFX_CORE || WINDOWS_UWP
            this.DefaultStyleKey = typeof(PackIconModern);
#endif
        }
    }
}