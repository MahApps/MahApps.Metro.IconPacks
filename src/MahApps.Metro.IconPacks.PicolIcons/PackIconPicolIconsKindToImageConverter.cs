namespace MahApps.Metro.IconPacks.Converter
{
    public class PackIconPicolIconsKindToImageConverter : PackIconKindToImageConverterBase
    {
        /// <inheritdoc />
        protected override string GetPathData(object iconKind)
        {
            string data = null;
            if (iconKind is PackIconPicolIconsKind kind)
            {
                PackIconDataFactory<PackIconPicolIconsKind>.DataIndex.Value?.TryGetValue(kind, out data);
            }
            return data;
        }
    }
}