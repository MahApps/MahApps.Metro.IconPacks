﻿namespace MahApps.Metro.IconPacks.Converter
{
    public class PackIconEntypoKindToImageConverter : PackIconKindToImageConverterBase
    {
        /// <inheritdoc />
        protected override string GetPathData(object iconKind)
        {
            string data = null;
            if (iconKind is PackIconEntypoKind kind)
            {
                PackIconDataFactory<PackIconEntypoKind>.DataIndex.Value?.TryGetValue(kind, out data);
            }
            return data;
        }
    }
}