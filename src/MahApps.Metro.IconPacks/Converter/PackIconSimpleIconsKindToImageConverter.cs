namespace MahApps.Metro.IconPacks.Converter
{
    public class PackIconSimpleIconsKindToImageConverter : PackIconKindToImageConverterBase
    {
        /// <inheritdoc />
        protected override string GetPathData(object iconKind)
        {
            string data = null;
            if (iconKind is PackIconSimpleIconsKind simpleIconsKind)
            {
                PackIconSimpleIconsDataFactory.DataIndex.Value?.TryGetValue(simpleIconsKind, out data);
            }
            return data;
        }
    }
}