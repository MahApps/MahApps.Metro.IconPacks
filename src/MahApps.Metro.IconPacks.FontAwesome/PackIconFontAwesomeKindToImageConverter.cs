﻿namespace MahApps.Metro.IconPacks.Converter
{
    public class PackIconFontAwesomeKindToImageConverter : PackIconKindToImageConverterBase
    {
        /// <inheritdoc />
        protected override string GetPathData(object iconKind)
        {
            string data = null;
            if (iconKind is PackIconFontAwesomeKind kind)
            {
                PackIconDataFactory<PackIconFontAwesomeKind>.DataIndex.Value?.TryGetValue(kind, out data);
            }
            return data;
        }
    }
}