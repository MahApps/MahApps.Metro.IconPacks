
#if !(NETFX_CORE || WINDOWS_UWP)
using System.Windows;
#endif

namespace MahApps.Metro.IconPacks
{
    /// <summary>
    /// All icons sourced from Material Design Icons Font v1.6.50 - <see><cref>https://materialdesignicons.com</cref></see> - in accordance of
    /// <see><cref>https://github.com/Templarian/MaterialDesign/blob/master/license.txt</cref></see>.
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