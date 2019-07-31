using Windows.UI.Xaml.Media;

namespace MahApps.Metro.IconPacks
{
    /// <summary>
    /// All icons sourced from Entypo+ Icons Font <see><cref>http://www.entypo.com</cref></see>
    /// Licensed under <see><cref>http://creativecommons.org/licenses/by-sa/4.0/</cref></see>.
    /// </summary>
    public class PathIconEntypo : PathIconControl<PackIconEntypoKind>
    {
        public PathIconEntypo() : base(PackIconEntypoDataFactory.Create)
        {
        }
    }
}