﻿using System;
using System.Windows.Markup;
using System.Windows.Media;

namespace MahApps.Metro.IconPacks
{
    [MarkupExtensionReturnType(typeof(ImageSource))]
    public class WeatherIconsImageExtension : BasePackIconImageExtension
    {
        public WeatherIconsImageExtension()
        {
        }

        public WeatherIconsImageExtension(PackIconWeatherIconsKind kind)
        {
            this.Kind = kind;
        }

        [ConstructorArgument("kind")]
        public PackIconWeatherIconsKind Kind { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return CreateImageSource(this.Kind, this.Brush ?? Brushes.Black);
        }

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