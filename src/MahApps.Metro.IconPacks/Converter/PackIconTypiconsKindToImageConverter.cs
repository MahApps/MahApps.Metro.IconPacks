using System.Windows.Media;

namespace MahApps.Metro.IconPacks.Converter
{
    public class PackIconTypiconsKindToImageConverter : PackIconKindToImageConverterBase
    {
        /// <inheritdoc />
        protected override string GetPathData(object iconKind)
        {
            string data = null;
            if (iconKind is PackIconTypiconsKind kind)
            {
                PackIconTypiconsDataFactory.DataIndex.Value?.TryGetValue(kind, out data);
            }
            return data;
        }

        /// <inheritdoc />
        protected override ScaleTransform GetScaleTransform(object iconKind)
        {
            return new ScaleTransform(1, -1);
        }
    }
}