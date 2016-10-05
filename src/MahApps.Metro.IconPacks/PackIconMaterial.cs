using System.Windows;

namespace MahApps.Metro.IconPacks
{
    /// <summary>
    /// Icons from the Material Design Icons project, <see><cref>https://materialdesignicons.com/</cref></see>.
    /// </summary>
    public class PackIconMaterial : PackIcon<PackIconMaterialKind>
    {        
        static PackIconMaterial()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PackIconMaterial), new FrameworkPropertyMetadata(typeof(PackIconMaterial)));
        }     

        public PackIconMaterial() : base(PackIconMaterialDataFactory.Create)
        { }    
    }
}
