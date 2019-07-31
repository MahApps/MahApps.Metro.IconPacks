using Windows.UI.Xaml.Media;

namespace MahApps.Metro.IconPacks
{
    /// <summary>
    /// All icons sourced from Material Design Light Icons - <see><cref>https://github.com/Templarian/MaterialDesignLight</cref></see> - in accordance of
    /// <see><cref>https://github.com/Templarian/MaterialDesignLight/blob/master/LICENSE.md</cref></see>.
    /// </summary>
    public class PathIconMaterialLight : PathIconControl<PackIconMaterialLightKind>
    {
        public PathIconMaterialLight() : base(PackIconMaterialLightDataFactory.Create)
        {
        }
    }
}