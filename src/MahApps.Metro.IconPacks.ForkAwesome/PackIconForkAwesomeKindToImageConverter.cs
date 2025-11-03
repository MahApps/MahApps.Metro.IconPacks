namespace MahApps.Metro.IconPacks.Converter
{
    public class PackIconForkAwesomeKindToImageConverter : PackIconKindToImageConverterBase
    {
        /// <inheritdoc />
        protected override string GetPathData(object iconKind)
        {
            string data = null;
            if (iconKind is PackIconForkAwesomeKind kind)
            {
                PackIconDataFactory<PackIconForkAwesomeKind>.DataIndex.Value?.TryGetValue(kind, out data);
            }
            return data;
        }
    }
}