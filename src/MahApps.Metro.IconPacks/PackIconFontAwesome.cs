
#if !(NETFX_CORE || WINDOWS_UWP)
using System.Windows;
#endif

namespace MahApps.Metro.IconPacks
{
    /// <summary>
    /// Icons from the FontAwesome Icons project, <see><cref>http://fontawesome.io</cref></see>.
    /// </summary>
    public class PackIconFontAwesome : PackIconControl<PackIconFontAwesomeKind>
    {
#if !(NETFX_CORE || WINDOWS_UWP)
        static PackIconFontAwesome()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PackIconFontAwesome), new FrameworkPropertyMetadata(typeof(PackIconFontAwesome)));
        }
#endif

        public PackIconFontAwesome() : base(PackIconFontAwesomeDataFactory.Create)
        {
#if NETFX_CORE || WINDOWS_UWP
            this.DefaultStyleKey = typeof(PackIconFontAwesome);
#endif
        }
    }
}