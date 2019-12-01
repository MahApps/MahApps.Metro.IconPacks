using System.Windows.Media;

namespace MahApps.Metro.IconPacks.Converter
{
    public class PackIconJamIconsKindToImageConverter : PackIconKindToImageConverterBase
    {
        /// <inheritdoc />
        protected override string GetPathData(object iconKind)
        {
            string data = null;
            if (iconKind is PackIconJamIconsKind jamIconsKind)
            {
                PackIconJamIconsDataFactory.DataIndex.Value?.TryGetValue(jamIconsKind, out data);
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