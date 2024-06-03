namespace MahApps.Metro.IconPacks.Converter
{
    public class PackIconMemoryIconsKindToImageConverter : PackIconKindToImageConverterBase
    {
        /// <inheritdoc />
        protected override string GetPathData(object iconKind)
        {
            string data = null;
            if (iconKind is PackIconMemoryIconsKind kind)
            {
                PackIconDataFactory<PackIconMemoryIconsKind>.DataIndex.Value?.TryGetValue(kind, out data);
            }
            return data;
        }
    }
}