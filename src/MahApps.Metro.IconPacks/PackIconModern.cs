
#if !NETFX_CORE
using System.Windows;
#endif

namespace MahApps.Metro.IconPacks
{
    /// <summary>
    /// Icons from the Modern UI Icons project, <see><cref>http://modernuiicons.com</cref></see>.
    /// </summary>
    public class PackIconModern : PackIcon<PackIconModernKind>
    {
#if !NETFX_CORE
        static PackIconModern()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PackIconModern), new FrameworkPropertyMetadata(typeof(PackIconModern)));
        }
#endif

        public PackIconModern() : base(PackIconModernDataFactory.Create)
        {
#if NETFX_CORE
            this.DefaultStyleKey = typeof(PackIconModern);
#endif
        }
    }
}