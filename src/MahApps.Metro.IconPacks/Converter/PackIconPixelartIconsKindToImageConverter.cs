namespace MahApps.Metro.IconPacks.Converter
{
    public class PackIconPixelartIconsKindToImageConverter : PackIconKindToImageConverterBase
    {
        /// <inheritdoc />
        protected override string GetPathData(object iconKind)
        {
            string data = null;
            if (iconKind is PackIconPixelartIconsKind PixelartIconsKind)
            {
                PackIconPixelartIconsDataFactory.DataIndex.Value?.TryGetValue(PixelartIconsKind, out data);
            }
            return data;
        }
    }
}