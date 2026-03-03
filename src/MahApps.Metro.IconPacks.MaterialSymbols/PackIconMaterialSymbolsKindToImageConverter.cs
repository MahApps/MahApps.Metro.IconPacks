namespace MahApps.Metro.IconPacks.Converter
{
    public class PackIconMaterialSymbolsKindToImageConverter : PackIconKindToImageConverterBase
    {
        /// <inheritdoc />
        protected override string GetPathData(object iconKind)
        {
            string data = null;
            if (iconKind is PackIconMaterialSymbolsKind kind)
            {
                PackIconDataFactory<PackIconMaterialSymbolsKind>.DataIndex.Value?.TryGetValue(kind, out data);
            }
            return data;
        }
    }
}