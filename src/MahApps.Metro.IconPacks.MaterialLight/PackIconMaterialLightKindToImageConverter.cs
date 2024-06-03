namespace MahApps.Metro.IconPacks.Converter
{
    public class PackIconMaterialLightKindToImageConverter : PackIconKindToImageConverterBase
    {
        /// <inheritdoc />
        protected override string GetPathData(object iconKind)
        {
            string data = null;
            if (iconKind is PackIconMaterialLightKind kind)
            {
                PackIconDataFactory<PackIconMaterialLightKind>.DataIndex.Value?.TryGetValue(kind, out data);
            }
            return data;
        }
    }
}