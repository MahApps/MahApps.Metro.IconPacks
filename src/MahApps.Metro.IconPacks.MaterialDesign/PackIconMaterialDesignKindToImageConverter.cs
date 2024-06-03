namespace MahApps.Metro.IconPacks.Converter
{
    public class PackIconMaterialDesignKindToImageConverter : PackIconKindToImageConverterBase
    {
        /// <inheritdoc />
        protected override string GetPathData(object iconKind)
        {
            string data = null;
            if (iconKind is PackIconMaterialDesignKind kind)
            {
                PackIconDataFactory<PackIconMaterialDesignKind>.DataIndex.Value?.TryGetValue(kind, out data);
            }
            return data;
        }
    }
}