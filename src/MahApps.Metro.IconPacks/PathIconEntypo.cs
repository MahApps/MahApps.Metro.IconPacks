using Windows.UI.Xaml.Media;

namespace MahApps.Metro.IconPacks
{
    /// <summary>
    /// Icons from Entypo+ Icons Font - <see><cref>http://www.entypo.com</cref></see>.
    /// </summary>
    public class PathIconEntypo : PathIconControl<PackIconEntypoKind>
    {
        public PathIconEntypo() : base(PackIconEntypoDataFactory.Create)
        {
        }
    }
}