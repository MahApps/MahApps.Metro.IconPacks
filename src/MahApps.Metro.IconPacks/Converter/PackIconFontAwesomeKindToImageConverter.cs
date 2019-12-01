namespace MahApps.Metro.IconPacks.Converter
{
    public class PackIconFontAwesomeKindToImageConverter : PackIconKindToImageConverterBase
    {
        /// <inheritdoc />
        protected override string GetPathData(object iconKind)
        {
            string data = null;
            if (iconKind is PackIconFontAwesomeKind fontAwesomeKind)
            {
                PackIconFontAwesomeDataFactory.DataIndex.Value?.TryGetValue(fontAwesomeKind, out data);
            }
            return data;
        }
    }
}