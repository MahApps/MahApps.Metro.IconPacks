
#if !NETFX_CORE
using System.Windows;
#endif

namespace MahApps.Metro.IconPacks
{
    /// <summary>
    /// Icons from Entypo+ Icons Font - <see><cref>http://www.entypo.com</cref></see>.
    /// </summary>
    public class PackIconEntypo : PackIcon<PackIconEntypoKind>
    {
#if !NETFX_CORE
        static PackIconEntypo()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PackIconEntypo), new FrameworkPropertyMetadata(typeof(PackIconEntypo)));
        }
#endif

        public PackIconEntypo() : base(PackIconEntypoDataFactory.Create)
        {
#if NETFX_CORE
            this.DefaultStyleKey = typeof(PackIconEntypo);
#endif
        }
    }
}