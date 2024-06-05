namespace MahApps.Metro.IconPacks.Converter
{
    public class PackIconPixelartIconsKindToImageConverter : PackIconKindToImageConverterBase
    {
        /// <inheritdoc />
        protected override string GetPathData(object iconKind)
        {
            string data = null;
            if (iconKind is PackIconPixelartIconsKind kind)
            {
                PackIconDataFactory<PackIconPixelartIconsKind>.DataIndex.Value?.TryGetValue(kind, out data);
            }
            return data;
        }
    }
}