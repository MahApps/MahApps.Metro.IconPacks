using System.Windows.Media;

namespace MahApps.Metro.IconPacks.Converter
{
    public class PackIconEvaIconsKindToImageConverter : PackIconKindToImageConverterBase
    {
        /// <inheritdoc />
        protected override string GetPathData(object iconKind)
        {
            string data = null;
            if (iconKind is PackIconEvaIconsKind evaIconsKind)
            {
                PackIconEvaIconsDataFactory.DataIndex.Value?.TryGetValue(evaIconsKind, out data);
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