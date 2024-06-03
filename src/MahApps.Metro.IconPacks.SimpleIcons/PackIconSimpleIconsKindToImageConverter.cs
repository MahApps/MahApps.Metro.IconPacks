namespace MahApps.Metro.IconPacks.Converter
{
    public class PackIconSimpleIconsKindToImageConverter : PackIconKindToImageConverterBase
    {
        /// <inheritdoc />
        protected override string GetPathData(object iconKind)
        {
            string data = null;
            if (iconKind is PackIconSimpleIconsKind kind)
            {
                PackIconDataFactory<PackIconSimpleIconsKind>.DataIndex.Value?.TryGetValue(kind, out data);
            }
            return data;
        }
    }
}