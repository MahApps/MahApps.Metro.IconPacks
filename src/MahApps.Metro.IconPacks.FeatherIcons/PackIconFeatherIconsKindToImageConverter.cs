namespace MahApps.Metro.IconPacks.Converter
{
    public class PackIconFeatherIconsKindToImageConverter : PackIconKindToImageConverterBase
    {
        /// <inheritdoc />
        protected override string GetPathData(object iconKind)
        {
            string data = null;
            if (iconKind is PackIconFeatherIconsKind kind)
            {
                PackIconDataFactory<PackIconFeatherIconsKind>.DataIndex.Value?.TryGetValue(kind, out data);
            }
            return data;
        }
    }
}