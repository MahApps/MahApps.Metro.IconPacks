using System.Windows.Media;

namespace MahApps.Metro.IconPacks.Converter
{
    public class PackIconBoxIconsKindToImageConverter : PackIconKindToImageConverterBase
    {
        /// <inheritdoc />
        protected override string GetPathData(object iconKind)
        {
            string data = null;
            if (iconKind is PackIconBoxIconsKind boxIconsKind)
            {
                PackIconBoxIconsDataFactory.DataIndex.Value?.TryGetValue(boxIconsKind, out data);
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