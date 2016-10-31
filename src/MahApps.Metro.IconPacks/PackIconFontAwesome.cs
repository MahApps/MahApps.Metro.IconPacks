
#if !NETFX_CORE
using System.Windows;
#endif

namespace MahApps.Metro.IconPacks
{
    /// <summary>
    /// Icons from the FontAwesome Icons project, <see><cref>http://fontawesome.io</cref></see>.
    /// </summary>
    public class PackIconFontAwesome : PackIcon<PackIconFontAwesomeKind>
    {
#if !NETFX_CORE
        static PackIconFontAwesome()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PackIconFontAwesome), new FrameworkPropertyMetadata(typeof(PackIconFontAwesome)));
        }
#endif

        public PackIconFontAwesome() : base(PackIconFontAwesomeDataFactory.Create)
        {
#if NETFX_CORE
            this.DefaultStyleKey = typeof(PackIconFontAwesome);
#endif
        }
    }
}