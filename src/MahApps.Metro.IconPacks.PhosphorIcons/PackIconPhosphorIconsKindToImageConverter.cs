﻿namespace MahApps.Metro.IconPacks.Converter
{
    public class PackIconPhosphorIconsKindToImageConverter : PackIconKindToImageConverterBase
    {
        /// <inheritdoc />
        protected override string GetPathData(object iconKind)
        {
            string data = null;
            if (iconKind is PackIconPhosphorIconsKind kind)
            {
                PackIconDataFactory<PackIconPhosphorIconsKind>.DataIndex.Value?.TryGetValue(kind, out data);
            }
            return data;
        }
    }
}