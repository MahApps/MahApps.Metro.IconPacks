
#if !NETFX_CORE
using System.Windows;
#endif

namespace MahApps.Metro.IconPacks
{
    /// <summary>
    /// Icons from the Material Design Icons project, <see><cref>https://materialdesignicons.com/</cref></see>.
    /// </summary>
    public class PackIconMaterial : PackIcon<PackIconMaterialKind>
    {
#if !NETFX_CORE
        static PackIconMaterial()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PackIconMaterial), new FrameworkPropertyMetadata(typeof(PackIconMaterial)));
        }
#endif

        public PackIconMaterial() : base(PackIconMaterialDataFactory.Create)
        {
#if NETFX_CORE
            this.DefaultStyleKey = typeof(PackIconMaterial);
#endif
        }
    }
}