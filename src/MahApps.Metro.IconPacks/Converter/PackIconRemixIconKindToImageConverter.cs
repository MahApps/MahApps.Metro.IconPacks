using System.Windows.Media;

namespace MahApps.Metro.IconPacks.Converter
{
    public class PackIconRemixIconKindToImageConverter : PackIconKindToImageConverterBase
    {
        /// <inheritdoc />
        protected override string GetPathData(object iconKind)
        {
            string data = null;
            if (iconKind is PackIconRemixIconKind RemixIconKind)
            {
                PackIconRemixIconDataFactory.DataIndex.Value?.TryGetValue(RemixIconKind, out data);
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