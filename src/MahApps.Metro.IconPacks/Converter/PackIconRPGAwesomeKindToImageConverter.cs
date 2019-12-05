using System.Windows.Media;

namespace MahApps.Metro.IconPacks.Converter
{
    public class PackIconRPGAwesomeKindToImageConverter : PackIconKindToImageConverterBase
    {
        /// <inheritdoc />
        protected override string GetPathData(object iconKind)
        {
            string data = null;
            if (iconKind is PackIconRPGAwesomeKind rpgAwesomeKind)
            {
                PackIconRPGAwesomeDataFactory.DataIndex.Value?.TryGetValue(rpgAwesomeKind, out data);
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