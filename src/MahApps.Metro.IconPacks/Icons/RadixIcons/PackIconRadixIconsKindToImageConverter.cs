namespace MahApps.Metro.IconPacks.Converter
{
    public class PackIconRadixIconsKindToImageConverter : PackIconKindToImageConverterBase
    {
        /// <inheritdoc />
        protected override string GetPathData(object iconKind)
        {
            string data = null;
            if (iconKind is PackIconRadixIconsKind kind)
            {
                PackIconRadixIconsDataFactory.DataIndex.Value?.TryGetValue(kind, out data);
            }
            return data;
        }
    }
}