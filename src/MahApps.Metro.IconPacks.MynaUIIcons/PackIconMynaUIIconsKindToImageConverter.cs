namespace MahApps.Metro.IconPacks.Converter
{
    public class PackIconMynaUIIconsKindToImageConverter : PackIconKindToImageConverterBase
    {
        /// <inheritdoc />
        protected override string GetPathData(object iconKind)
        {
            string data = null;
            if (iconKind is PackIconMynaUIIconsKind kind)
            {
                PackIconDataFactory<PackIconMynaUIIconsKind>.DataIndex.Value?.TryGetValue(kind, out data);
            }
            return data;
        }
    }
}