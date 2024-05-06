namespace MahApps.Metro.IconPacks.Converter
{
    public class PackIconGameIconsKindToImageConverter : PackIconKindToImageConverterBase
    {
        /// <inheritdoc />
        protected override string GetPathData(object iconKind)
        {
            string data = null;
            if (iconKind is PackIconGameIconsKind kind)
            {
                PackIconGameIconsDataFactory.DataIndex.Value?.TryGetValue(kind, out data);
            }
            return data;
        }
    }
}