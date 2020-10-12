namespace MahApps.Metro.IconPacks.Converter
{
    public class PackIconZondiconsKindToImageConverter : PackIconKindToImageConverterBase
    {
        /// <inheritdoc />
        protected override string GetPathData(object iconKind)
        {
            string data = null;
            if (iconKind is PackIconZondiconsKind kind)
            {
                PackIconZondiconsDataFactory.DataIndex.Value?.TryGetValue(kind, out data);
            }
            return data;
        }
    }
}