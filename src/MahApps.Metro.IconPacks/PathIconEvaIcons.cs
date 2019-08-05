using Windows.UI.Xaml.Media;

namespace MahApps.Metro.IconPacks
{
    /// <summary>
    /// eva-icons licensed under the MIT License <see><cref>https://github.com/akveo/eva-icons/blob/master/LICENSE.txt</cref></see>
    /// Contributions, corrections and requests can be made on GitHub <see><cref>https://github.com/akveo/eva-icons</cref></see>.
    /// </summary>
    public class PathIconEvaIcons : PathIconControl<PackIconEvaIconsKind>
    {
        public PathIconEvaIcons() : base(PackIconEvaIconsDataFactory.Create)
        {
            var transformGroup = this.RenderTransform as TransformGroup ?? new TransformGroup();
            var scaleTransform = new ScaleTransform() {ScaleY = -1};
            transformGroup.Children.Insert(0, scaleTransform);
        }
    }
}