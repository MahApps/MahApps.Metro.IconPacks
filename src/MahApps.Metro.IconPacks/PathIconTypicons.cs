using Windows.UI.Xaml.Media;

namespace MahApps.Metro.IconPacks
{
    /// <summary>
    /// Typicons Icons/Artwork distributed under [CC BY-SA](<see><cref>https://creativecommons.org/licenses/by-sa/3.0/</cref></see>) licence.
    /// Typicons Font distributed under 'SIL Open Font License' licence.
    /// </summary>
    public class PathIconTypicons : PathIconControl<PackIconTypiconsKind>
    {
        public PathIconTypicons() : base(PackIconTypiconsDataFactory.Create)
        {
            var transformGroup = this.RenderTransform as TransformGroup ?? new TransformGroup();
            var scaleTransform = new ScaleTransform() {ScaleY = -1};
            transformGroup.Children.Insert(0, scaleTransform);
        }
    }
}
