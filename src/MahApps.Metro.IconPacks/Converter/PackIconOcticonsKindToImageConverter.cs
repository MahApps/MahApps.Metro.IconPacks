namespace MahApps.Metro.IconPacks.Converter
{
    public class PackIconOcticonsKindToImageConverter : PackIconKindToImageConverterBase
    {
        /// <inheritdoc />
        protected override string GetPathData(object iconKind)
        {
            string data = null;
            if (iconKind is PackIconOcticonsKind octiconsKind)
            {
                PackIconOcticonsDataFactory.DataIndex.Value?.TryGetValue(octiconsKind, out data);
            }
            return data;
        }
    }
}