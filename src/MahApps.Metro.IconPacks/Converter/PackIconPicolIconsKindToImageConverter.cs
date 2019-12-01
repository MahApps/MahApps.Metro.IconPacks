namespace MahApps.Metro.IconPacks.Converter
{
    public class PackIconPicolIconsKindToImageConverter : PackIconKindToImageConverterBase
    {
        /// <inheritdoc />
        protected override string GetPathData(object iconKind)
        {
            string data = null;
            if (iconKind is PackIconPicolIconsKind picolIconsKind)
            {
                PackIconPicolIconsDataFactory.DataIndex.Value?.TryGetValue(picolIconsKind, out data);
            }
            return data;
        }
    }
}