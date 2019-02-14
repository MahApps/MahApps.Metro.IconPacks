
#if !(NETFX_CORE || WINDOWS_UWP)
using System.Windows;
#endif

namespace MahApps.Metro.IconPacks
{
    /// <summary>
    /// Icons from the Material Design Icons project, <see><cref>https://materialdesignicons.com/</cref></see>.
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