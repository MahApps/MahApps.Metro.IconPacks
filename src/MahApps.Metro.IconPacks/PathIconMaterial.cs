using Windows.UI.Xaml.Media;

namespace MahApps.Metro.IconPacks
{
    /// <summary>
    /// All icons sourced from Material Design Icons Font v1.6.50 - <see><cref>https://materialdesignicons.com</cref></see> - in accordance of
    /// <see><cref>https://github.com/Templarian/MaterialDesign/blob/master/license.txt</cref></see>.
    /// </summary>
    public class PathIconMaterial : PathIconControl<PackIconMaterialKind>
    {
        public PathIconMaterial() : base(PackIconMaterialDataFactory.Create)
        {
        }
    }
}