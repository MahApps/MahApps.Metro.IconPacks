using Windows.UI.Xaml.Media;

namespace MahApps.Metro.IconPacks
{
    /// <summary>
    /// Icons from the Material Design Icons project, <see><cref>https://materialdesignicons.com/</cref></see>.
    /// </summary>
    public class PathIconMaterial : PathIconControl<PackIconMaterialKind>
    {
        public PathIconMaterial() : base(PackIconMaterialDataFactory.Create)
        {
        }
    }
}