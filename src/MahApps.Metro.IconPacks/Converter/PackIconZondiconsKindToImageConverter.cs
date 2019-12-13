namespace MahApps.Metro.IconPacks.Converter
{
    public class PackIconZondiconsKindToImageConverter : PackIconKindToImageConverterBase
    {
        /// <inheritdoc />
        protected override string GetPathData(object iconKind)
        {
            string data = null;
            if (iconKind is PackIconZondiconsKind zondiconsKind)
            {
                PackIconZondiconsDataFactory.DataIndex.Value?.TryGetValue(zondiconsKind, out data);
            }
            return data;
        }
    }
}