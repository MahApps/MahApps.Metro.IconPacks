
#if !(NETFX_CORE || WINDOWS_UWP)
using System.Windows;
#endif

namespace MahApps.Metro.IconPacks
{
    /// <summary>
    /// Icons from the Material Design Icons project, <see><cref>https://materialdesignicons.com/</cref></see>.
    /// </summary>
    public class PackIconMaterial : PackIconControl<PackIconMaterialKind>
    {
#if !(NETFX_CORE || WINDOWS_UWP)
        static PackIconMaterial()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PackIconMaterial), new FrameworkPropertyMetadata(typeof(PackIconMaterial)));
        }
#endif

        public PackIconMaterial() : base(PackIconMaterialDataFactory.Create)
        {
#if NETFX_CORE || WINDOWS_UWP
            this.DefaultStyleKey = typeof(PackIconMaterial);
#endif
        }
    }
}