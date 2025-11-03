namespace MahApps.Metro.IconPacks.Converter
{
    public class PackIconJamIconsKindToImageConverter : PackIconKindToImageConverterBase
    {
        /// <inheritdoc />
        protected override string GetPathData(object iconKind)
        {
            string data = null;
            if (iconKind is PackIconJamIconsKind kind)
            {
                PackIconDataFactory<PackIconJamIconsKind>.DataIndex.Value?.TryGetValue(kind, out data);
            }
            return data;
        }
    }
}