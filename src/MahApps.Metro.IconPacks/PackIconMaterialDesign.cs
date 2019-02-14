
#if !(NETFX_CORE || WINDOWS_UWP)
using System.Windows;
#endif

namespace MahApps.Metro.IconPacks
{
    /// <summary>
    /// All icons sourced from Google Material Design icon font - <see><cref>http://google.github.io/material-design-icons/</cref></see>
    /// google/material-design-icons is licensed under the Apache License 2.0 <see><cref>https://github.com/google/material-design-icons/blob/master/LICENSE</cref></see>
    /// </summary>
    public class PackIconMaterialDesign : PackIconControl<PackIconMaterialDesignKind>
    {
#if !(NETFX_CORE || WINDOWS_UWP)
        static PackIconMaterialDesign()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PackIconMaterialDesign), new FrameworkPropertyMetadata(typeof(PackIconMaterialDesign)));
        }
#endif

        public PackIconMaterialDesign() : base(PackIconMaterialDesignDataFactory.Create)
        {
#if NETFX_CORE || WINDOWS_UWP
            this.DefaultStyleKey = typeof(PackIconMaterialDesign);
#endif
        }
    }
}