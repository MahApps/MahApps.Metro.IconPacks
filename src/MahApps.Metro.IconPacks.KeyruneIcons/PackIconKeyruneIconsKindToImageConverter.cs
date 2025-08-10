namespace MahApps.Metro.IconPacks.Converter
{
    public class PackIconKeyruneIconsKindToImageConverter : PackIconKindToImageConverterBase
    {
        /// <inheritdoc />
        protected override string GetPathData(object iconKind)
        {
            string data = null;
            if (iconKind is PackIconKeyruneIconsKind kind)
            {
                PackIconDataFactory<PackIconKeyruneIconsKind>.DataIndex.Value?.TryGetValue(kind, out data);
            }
            return data;
        }
    }
}