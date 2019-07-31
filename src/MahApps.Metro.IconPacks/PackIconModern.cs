
#if !(NETFX_CORE || WINDOWS_UWP)
using System.Windows;
#endif

namespace MahApps.Metro.IconPacks
{
    /// <summary>
    /// All icons sourced from Modern UI Icons Font - <see><cref>http://modernuiicons.com</cref></see> - in accordance of
    /// <see><cref>https://github.com/Templarian/WindowsIcons/blob/master/WindowsPhone/license.txt</cref></see>.
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