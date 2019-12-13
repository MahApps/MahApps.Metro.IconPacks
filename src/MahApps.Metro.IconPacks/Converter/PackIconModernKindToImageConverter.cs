namespace MahApps.Metro.IconPacks.Converter
{
    public class PackIconModernKindToImageConverter : PackIconKindToImageConverterBase
    {
        /// <inheritdoc />
        protected override string GetPathData(object iconKind)
        {
            string data = null;
            if (iconKind is PackIconModernKind modernKind)
            {
                PackIconModernDataFactory.DataIndex.Value?.TryGetValue(modernKind, out data);
            }
            return data;
        }
    }
}