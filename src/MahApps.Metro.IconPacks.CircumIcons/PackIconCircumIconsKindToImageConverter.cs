namespace MahApps.Metro.IconPacks.Converter
{
    public class PackIconCircumIconsKindToImageConverter : PackIconKindToImageConverterBase
    {
        /// <inheritdoc />
        protected override string GetPathData(object iconKind)
        {
            string data = null;
            if (iconKind is PackIconCircumIconsKind kind)
            {
                PackIconDataFactory<PackIconCircumIconsKind>.DataIndex.Value?.TryGetValue(kind, out data);
            }
            return data;
        }
    }
}