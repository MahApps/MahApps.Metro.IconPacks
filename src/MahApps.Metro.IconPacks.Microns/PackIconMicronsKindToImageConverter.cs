﻿namespace MahApps.Metro.IconPacks.Converter
{
    public class PackIconMicronsKindToImageConverter : PackIconKindToImageConverterBase
    {
        /// <inheritdoc />
        protected override string GetPathData(object iconKind)
        {
            string data = null;
            if (iconKind is PackIconMicronsKind kind)
            {
                PackIconDataFactory<PackIconMicronsKind>.DataIndex.Value?.TryGetValue(kind, out data);
            }
            return data;
        }
    }
}