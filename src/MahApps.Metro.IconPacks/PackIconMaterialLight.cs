
#if !(NETFX_CORE || WINDOWS_UWP)
using System.Windows;
#endif

namespace MahApps.Metro.IconPacks
{
    /// <summary>
    /// All icons sourced from Material Design Light Icons - <see><cref>https://github.com/Templarian/MaterialDesignLight</cref></see> - in accordance of
    /// <see><cref>https://github.com/Templarian/MaterialDesignLight/blob/master/LICENSE.md</cref></see>.
    /// </summary>
    public class PackIconMaterialLight : PackIconControl<PackIconMaterialLightKind>
    {
#if !(NETFX_CORE || WINDOWS_UWP)
        static PackIconMaterialLight()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PackIconMaterialLight), new FrameworkPropertyMetadata(typeof(PackIconMaterialLight)));
        }
#endif

        public PackIconMaterialLight() : base(PackIconMaterialLightDataFactory.Create)
        {
#if NETFX_CORE || WINDOWS_UWP
            this.DefaultStyleKey = typeof(PackIconMaterialLight);
#endif
        }
    }
}