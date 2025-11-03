namespace MahApps.Metro.IconPacks.Converter
{
    public class PackIconLucideKindToImageConverter : PackIconKindToImageConverterBase
    {
        /// <inheritdoc />
        protected override string GetPathData(object iconKind)
        {
            string data = null;
            if (iconKind is PackIconLucideKind kind)
            {
                PackIconDataFactory<PackIconLucideKind>.DataIndex.Value?.TryGetValue(kind, out data);
            }
            return data;
        }
    }
}