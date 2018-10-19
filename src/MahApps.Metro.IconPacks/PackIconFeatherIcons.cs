
#if !NETFX_CORE
using System.Windows;
#endif

namespace MahApps.Metro.IconPacks
{
    /// <summary>
    /// Feather is licensed under the MIT License <see><cref>https://github.com/feathericons/feather/blob/master/LICENSE</cref></see>
    /// Contributions, corrections and requests can be made on GitHub <see><cref>https://github.com/feathericons/feather</cref></see>.
    /// </summary>
    public class PackIconFeatherIcons : PackIconControl<PackIconFeatherIconsKind>
    {
#if !NETFX_CORE
        static PackIconFeatherIcons()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PackIconFeatherIcons), new FrameworkPropertyMetadata(typeof(PackIconFeatherIcons)));
        }
#endif

        public PackIconFeatherIcons() : base(PackIconFeatherIconsDataFactory.Create)
        {
#if NETFX_CORE
            this.DefaultStyleKey = typeof(PackIconFeatherIcons);
#endif
        }
    }
}