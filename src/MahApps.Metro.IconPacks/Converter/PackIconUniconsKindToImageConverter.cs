namespace MahApps.Metro.IconPacks.Converter
{
    public class PackIconUniconsKindToImageConverter : PackIconKindToImageConverterBase
    {
        /// <inheritdoc />
        protected override string GetPathData(object iconKind)
        {
            string data = null;
            if (iconKind is PackIconUniconsKind uniconsKind)
            {
                PackIconUniconsDataFactory.DataIndex.Value?.TryGetValue(uniconsKind, out data);
            }
            return data;
        }
    }
}