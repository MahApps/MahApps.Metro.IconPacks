namespace MahApps.Metro.IconPacks.Converter
{
    public class PackIconUniconsKindToImageConverter : PackIconKindToImageConverterBase
    {
        /// <inheritdoc />
        protected override string GetPathData(object iconKind)
        {
            string data = null;
            if (iconKind is PackIconUniconsKind kind)
            {
                PackIconDataFactory<PackIconUniconsKind>.DataIndex.Value?.TryGetValue(kind, out data);
            }
            return data;
        }
    }
}