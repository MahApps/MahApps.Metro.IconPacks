namespace MahApps.Metro.IconPacks.Converter
{
    public class PackIconMaterialKindToImageConverter : PackIconKindToImageConverterBase
    {
        /// <inheritdoc />
        protected override string GetPathData(object iconKind)
        {
            string data = null;
            if (iconKind is PackIconMaterialKind materialKind)
            {
                PackIconMaterialDataFactory.DataIndex.Value?.TryGetValue(materialKind, out data);
            }
            return data;
        }
    }
}