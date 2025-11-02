namespace MahApps.Metro.IconPacks.Converter
{
    public class PackIconBootstrapIconsKindToImageConverter : PackIconKindToImageConverterBase
    {
        /// <inheritdoc />
        protected override string GetPathData(object iconKind)
        {
            string data = null;
            if (iconKind is PackIconBootstrapIconsKind kind)
            {
                PackIconDataFactory<PackIconBootstrapIconsKind>.DataIndex.Value?.TryGetValue(kind, out data);
            }
            return data;
        }
    }
}