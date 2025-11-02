namespace MahApps.Metro.IconPacks.Converter
{
    public class PackIconFileIconsKindToImageConverter : PackIconKindToImageConverterBase
    {
        /// <inheritdoc />
        protected override string GetPathData(object iconKind)
        {
            string data = null;
            if (iconKind is PackIconFileIconsKind kind)
            {
                PackIconDataFactory<PackIconFileIconsKind>.DataIndex.Value?.TryGetValue(kind, out data);
            }
            return data;
        }
    }
}