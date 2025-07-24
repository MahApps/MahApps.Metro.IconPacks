namespace MahApps.Metro.IconPacks.Converter
{
    public class PackIconFontAwesome6KindToImageConverter : PackIconKindToImageConverterBase
    {
        /// <inheritdoc />
        protected override string GetPathData(object iconKind)
        {
            string data = null;
            if (iconKind is PackIconFontAwesome6Kind kind)
            {
                PackIconDataFactory<PackIconFontAwesome6Kind>.DataIndex.Value?.TryGetValue(kind, out data);
            }
            return data;
        }
    }
}