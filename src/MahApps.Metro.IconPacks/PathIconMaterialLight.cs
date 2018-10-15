using Windows.UI.Xaml.Media;

namespace MahApps.Metro.IconPacks
{
    /// <summary>
    /// Icons from the Material Design Icons project, <see><cref>https://materialdesignicons.com/</cref></see>.
    /// </summary>
    public class PathIconMaterialLight : PathIconControl<PackIconMaterialLightKind>
    {
        public PathIconMaterialLight() : base(PackIconMaterialLightDataFactory.Create)
        {
        }
    }
}