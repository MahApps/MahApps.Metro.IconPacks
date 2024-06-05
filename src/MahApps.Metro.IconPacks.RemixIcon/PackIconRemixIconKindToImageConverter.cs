namespace MahApps.Metro.IconPacks.Converter
{
    public class PackIconRemixIconKindToImageConverter : PackIconKindToImageConverterBase
    {
        /// <inheritdoc />
        protected override string GetPathData(object iconKind)
        {
            string data = null;
            if (iconKind is PackIconRemixIconKind kind)
            {
                PackIconDataFactory<PackIconRemixIconKind>.DataIndex.Value?.TryGetValue(kind, out data);
            }
            return data;
        }
    }
}