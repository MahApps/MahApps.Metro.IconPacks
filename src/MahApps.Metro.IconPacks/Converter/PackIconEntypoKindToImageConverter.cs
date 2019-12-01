namespace MahApps.Metro.IconPacks.Converter
{
    public class PackIconEntypoKindToImageConverter : PackIconKindToImageConverterBase
    {
        /// <inheritdoc />
        protected override string GetPathData(object iconKind)
        {
            string data = null;
            if (iconKind is PackIconEntypoKind entypoKind)
            {
                PackIconEntypoDataFactory.DataIndex.Value?.TryGetValue(entypoKind, out data);
            }
            return data;
        }
    }
}