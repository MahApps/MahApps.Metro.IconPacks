
#if !NETFX_CORE
using System.Windows;
#endif

namespace MahApps.Metro.IconPacks
{
    /// <summary>
    /// Icons from the Material Design Icons project, <see><cref>https://materialdesignicons.com/</cref></see>.
    /// </summary>
    public class PackIconMaterialLight : PackIcon<PackIconMaterialLightKind>
    {
#if !NETFX_CORE
        static PackIconMaterialLight()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PackIconMaterialLight), new FrameworkPropertyMetadata(typeof(PackIconMaterialLight)));
        }
#endif

        public PackIconMaterialLight() : base(PackIconMaterialLightDataFactory.Create)
        {
#if NETFX_CORE
            this.DefaultStyleKey = typeof(PackIconMaterialLight);
#endif
        }
    }
}