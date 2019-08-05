
#if !(NETFX_CORE || WINDOWS_UWP)
using System.Windows;
#endif

namespace MahApps.Metro.IconPacks
{
    /// <summary>
    /// eva-icons licensed under the MIT License <see><cref>https://github.com/akveo/eva-icons/blob/master/LICENSE.txt</cref></see>
    /// Contributions, corrections and requests can be made on GitHub <see><cref>https://github.com/akveo/eva-icons</cref></see>.
    /// </summary>
    public class PackIconEvaIcons : PackIconControl<PackIconEvaIconsKind>
    {
#if !(NETFX_CORE || WINDOWS_UWP)
        static PackIconEvaIcons()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PackIconEvaIcons), new FrameworkPropertyMetadata(typeof(PackIconEvaIcons)));
        }
#endif

        public PackIconEvaIcons() : base(PackIconEvaIconsDataFactory.Create)
        {
#if NETFX_CORE || WINDOWS_UWP
            this.DefaultStyleKey = typeof(PackIconEvaIcons);
#endif
        }
    }
}