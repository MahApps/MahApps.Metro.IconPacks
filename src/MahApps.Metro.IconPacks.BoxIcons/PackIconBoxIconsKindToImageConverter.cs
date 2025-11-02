namespace MahApps.Metro.IconPacks.Converter
{
    public class PackIconBoxIconsKindToImageConverter : PackIconKindToImageConverterBase
    {
        /// <inheritdoc />
        protected override string GetPathData(object iconKind)
        {
            string data = null;
            if (iconKind is PackIconBoxIconsKind kind)
            {
                PackIconDataFactory<PackIconBoxIconsKind>.DataIndex.Value?.TryGetValue(kind, out data);
            }
            return data;
        }
    }
}