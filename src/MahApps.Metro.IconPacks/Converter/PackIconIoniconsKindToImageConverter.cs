namespace MahApps.Metro.IconPacks.Converter
{
    public class PackIconIoniconsKindToImageConverter : PackIconKindToImageConverterBase
    {
        /// <inheritdoc />
        protected override string GetPathData(object iconKind)
        {
            string data = null;
            if (iconKind is PackIconIoniconsKind ioniconsKind)
            {
                PackIconIoniconsDataFactory.DataIndex.Value?.TryGetValue(ioniconsKind, out data);
            }
            return data;
        }
    }
}