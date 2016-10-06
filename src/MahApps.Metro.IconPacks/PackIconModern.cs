using System.Windows;

namespace MahApps.Metro.IconPacks
{
    /// <summary>
    /// Icons from the Modern UI Icons project, <see><cref>http://modernuiicons.com</cref></see>.
    /// </summary>
    public class PackIconModern : PackIcon<PackIconModernKind>
    {        
        static PackIconModern()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PackIconModern), new FrameworkPropertyMetadata(typeof(PackIconModern)));
        }     

        public PackIconModern() : base(PackIconModernDataFactory.Create)
        { }    
    }
}
