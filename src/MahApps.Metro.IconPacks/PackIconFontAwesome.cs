
#if !(NETFX_CORE || WINDOWS_UWP)
using System.Windows;
#endif

namespace MahApps.Metro.IconPacks
{
    /// <summary>
    /// All icons sourced from Font Awesome Free <see><cref>https://fontawesome.com/</cref></see> - License <see><cref>https://fontawesome.com/license/free</cref></see>
    /// GitHub <see><cref>https://github.com/FortAwesome/Font-Awesome</cref></see>
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