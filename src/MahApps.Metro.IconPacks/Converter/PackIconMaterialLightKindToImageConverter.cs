namespace MahApps.Metro.IconPacks.Converter
{
    public class PackIconMaterialLightKindToImageConverter : PackIconKindToImageConverterBase
    {
        /// <inheritdoc />
        protected override string GetPathData(object iconKind)
        {
            string data = null;
            if (iconKind is PackIconMaterialLightKind materialLightKind)
            {
                PackIconMaterialLightDataFactory.DataIndex.Value?.TryGetValue(materialLightKind, out data);
            }
            return data;
        }
    }
}