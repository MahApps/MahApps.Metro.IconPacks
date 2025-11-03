namespace MahApps.Metro.IconPacks.Converter
{
    public class PackIconCooliconsKindToImageConverter : PackIconKindToImageConverterBase
    {
        /// <inheritdoc />
        protected override string GetPathData(object iconKind)
        {
            string data = null;
            if (iconKind is PackIconCooliconsKind kind)
            {
                PackIconDataFactory<PackIconCooliconsKind>.DataIndex.Value?.TryGetValue(kind, out data);
            }
            return data;
        }
    }
}