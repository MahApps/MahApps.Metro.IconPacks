namespace MahApps.Metro.IconPacks.Converter
{
    public class PackIconFontAwesome5KindToImageConverter : PackIconKindToImageConverterBase
    {
        /// <inheritdoc />
        protected override string GetPathData(object iconKind)
        {
            string data = null;
            if (iconKind is PackIconFontAwesome5Kind kind)
            {
                PackIconDataFactory<PackIconFontAwesome5Kind>.DataIndex.Value?.TryGetValue(kind, out data);
            }
            return data;
        }
    }
}