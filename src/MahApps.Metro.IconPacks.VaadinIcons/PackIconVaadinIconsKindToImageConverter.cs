namespace MahApps.Metro.IconPacks.Converter
{
    public class PackIconVaadinIconsKindToImageConverter : PackIconKindToImageConverterBase
    {
        /// <inheritdoc />
        protected override string GetPathData(object iconKind)
        {
            string data = null;
            if (iconKind is PackIconVaadinIconsKind kind)
            {
                PackIconDataFactory<PackIconVaadinIconsKind>.DataIndex.Value?.TryGetValue(kind, out data);
            }
            return data;
        }
    }
}