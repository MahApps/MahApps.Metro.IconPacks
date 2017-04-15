
#if !NETFX_CORE
using System.Windows;
#endif

namespace MahApps.Metro.IconPacks
{
    /// <summary>
    /// All SVG icons for popular brands, maintained by Dan Leech <see><cref>https://twitter.com/bathtype</cref></see>.
    /// Contributions, corrections and requests can be made on GitHub <see><cref>https://github.com/danleech/simple-icons</cref></see>.
    /// </summary>
    public class PackIconSimpleIcons : PackIcon<PackIconSimpleIconsKind>
    {
#if !NETFX_CORE
        static PackIconSimpleIcons()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PackIconSimpleIcons), new FrameworkPropertyMetadata(typeof(PackIconSimpleIcons)));
        }
#endif

        public PackIconSimpleIcons() : base(PackIconSimpleIconsDataFactory.Create)
        {
#if NETFX_CORE
            this.DefaultStyleKey = typeof(PackIconSimpleIcons);
#endif
        }
    }
}