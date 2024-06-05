namespace MahApps.Metro.IconPacks.Converter
{
    public class PackIconOcticonsKindToImageConverter : PackIconKindToImageConverterBase
    {
        /// <inheritdoc />
        protected override string GetPathData(object iconKind)
        {
            string data = null;
            if (iconKind is PackIconOcticonsKind kind)
            {
                PackIconDataFactory<PackIconOcticonsKind>.DataIndex.Value?.TryGetValue(kind, out data);
            }
            return data;
        }
    }
}