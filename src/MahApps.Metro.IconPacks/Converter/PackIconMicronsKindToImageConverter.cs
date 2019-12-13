namespace MahApps.Metro.IconPacks.Converter
{
    public class PackIconMicronsKindToImageConverter : PackIconKindToImageConverterBase
    {
        /// <inheritdoc />
        protected override string GetPathData(object iconKind)
        {
            string data = null;
            if (iconKind is PackIconMicronsKind micronsKind)
            {
                PackIconMicronsDataFactory.DataIndex.Value?.TryGetValue(micronsKind, out data);
            }
            return data;
        }
    }
}