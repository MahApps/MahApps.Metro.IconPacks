namespace MahApps.Metro.IconPacks.Converter
{
    public class PackIconEvaIconsKindToImageConverter : PackIconKindToImageConverterBase
    {
        /// <inheritdoc />
        protected override string GetPathData(object iconKind)
        {
            string data = null;
            if (iconKind is PackIconEvaIconsKind kind)
            {
                PackIconDataFactory<PackIconEvaIconsKind>.DataIndex.Value?.TryGetValue(kind, out data);
            }
            return data;
        }
    }
}