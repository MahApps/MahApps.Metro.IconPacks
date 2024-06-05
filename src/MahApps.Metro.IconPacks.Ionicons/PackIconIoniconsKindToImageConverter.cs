namespace MahApps.Metro.IconPacks.Converter
{
    public class PackIconIoniconsKindToImageConverter : PackIconKindToImageConverterBase
    {
        /// <inheritdoc />
        protected override string GetPathData(object iconKind)
        {
            string data = null;
            if (iconKind is PackIconIoniconsKind kind)
            {
                PackIconDataFactory<PackIconIoniconsKind>.DataIndex.Value?.TryGetValue(kind, out data);
            }
            return data;
        }
    }
}