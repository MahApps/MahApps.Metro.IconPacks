using System.Windows;

namespace MahApps.Metro.IconPacks
{
    /// <summary>
    /// Icons from the FontAwesome Icons project, <see><cref>http://fontawesome.io</cref></see>.
    /// </summary>
    public class PackIconFontAwesome : PackIcon<PackIconFontAwesomeKind>
    {        
        static PackIconFontAwesome()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PackIconFontAwesome), new FrameworkPropertyMetadata(typeof(PackIconFontAwesome)));
        }     

        public PackIconFontAwesome() : base(PackIconFontAwesomeDataFactory.Create)
        { }    
    }
}
