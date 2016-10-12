using System.Windows;

namespace MahApps.Metro.IconPacks
{
    /// <summary>
    /// Icons from GitHub Octicons - <see><cref>https://octicons.github.com</cref></see>
    /// </summary>
    public class PackIconOcticons : PackIcon<PackIconOcticonsKind>
    {        
        static PackIconOcticons()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PackIconOcticons), new FrameworkPropertyMetadata(typeof(PackIconOcticons)));
        }     

        public PackIconOcticons() : base(PackIconOcticonsDataFactory.Create)
        { }    
    }
}
