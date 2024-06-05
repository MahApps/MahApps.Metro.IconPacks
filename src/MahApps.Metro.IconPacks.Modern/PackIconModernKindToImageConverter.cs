namespace MahApps.Metro.IconPacks.Converter
{
    public class PackIconModernKindToImageConverter : PackIconKindToImageConverterBase
    {
        /// <inheritdoc />
        protected override string GetPathData(object iconKind)
        {
            string data = null;
            if (iconKind is PackIconModernKind kind)
            {
                PackIconDataFactory<PackIconModernKind>.DataIndex.Value?.TryGetValue(kind, out data);
            }
            return data;
        }
    }
}