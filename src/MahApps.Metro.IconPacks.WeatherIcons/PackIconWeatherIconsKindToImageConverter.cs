﻿namespace MahApps.Metro.IconPacks.Converter
{
    public class PackIconWeatherIconsKindToImageConverter : PackIconKindToImageConverterBase
    {
        /// <inheritdoc />
        protected override string GetPathData(object iconKind)
        {
            string data = null;
            if (iconKind is PackIconWeatherIconsKind kind)
            {
                PackIconDataFactory<PackIconWeatherIconsKind>.DataIndex.Value?.TryGetValue(kind, out data);
            }
            return data;
        }
    }
}