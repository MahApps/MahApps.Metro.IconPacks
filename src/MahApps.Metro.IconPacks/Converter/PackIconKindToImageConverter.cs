using System.Windows.Media;

namespace MahApps.Metro.IconPacks.Converter
{
    public class PackIconKindToImageConverter : PackIconKindToImageConverterBase
    {
        /// <inheritdoc />
        protected override string GetPathData(object iconKind)
        {
            string data = null;
            switch (iconKind)
            {
                case PackIconBoxIconsKind boxIconsKind:
                    PackIconBoxIconsDataFactory.DataIndex.Value?.TryGetValue(boxIconsKind, out data);
                    return data;
                case PackIconEntypoKind entypoKind:
                    PackIconEntypoDataFactory.DataIndex.Value?.TryGetValue(entypoKind, out data);
                    return data;
                case PackIconEvaIconsKind evaIconsKind:
                    PackIconEvaIconsDataFactory.DataIndex.Value?.TryGetValue(evaIconsKind, out data);
                    return data;
                case PackIconFeatherIconsKind featherIconsKind:
                    PackIconFeatherIconsDataFactory.DataIndex.Value?.TryGetValue(featherIconsKind, out data);
                    return data;
                case PackIconFontAwesomeKind fontAwesomeKind:
                    PackIconFontAwesomeDataFactory.DataIndex.Value?.TryGetValue(fontAwesomeKind, out data);
                    return data;
                case PackIconIoniconsKind ioniconsKind:
                    PackIconIoniconsDataFactory.DataIndex.Value?.TryGetValue(ioniconsKind, out data);
                    return data;
                case PackIconJamIconsKind jamIconsKind:
                    PackIconJamIconsDataFactory.DataIndex.Value?.TryGetValue(jamIconsKind, out data);
                    return data;
                case PackIconMaterialDesignKind materialDesignKind:
                    PackIconMaterialDesignDataFactory.DataIndex.Value?.TryGetValue(materialDesignKind, out data);
                    return data;
                case PackIconMaterialKind materialKind:
                    PackIconMaterialDataFactory.DataIndex.Value?.TryGetValue(materialKind, out data);
                    return data;
                case PackIconMaterialLightKind materialLightKind:
                    PackIconMaterialLightDataFactory.DataIndex.Value?.TryGetValue(materialLightKind, out data);
                    return data;
                case PackIconMicronsKind micronsKind:
                    PackIconMicronsDataFactory.DataIndex.Value?.TryGetValue(micronsKind, out data);
                    return data;
                case PackIconModernKind modernKind:
                    PackIconModernDataFactory.DataIndex.Value?.TryGetValue(modernKind, out data);
                    return data;
                case PackIconOcticonsKind octiconsKind:
                    PackIconOcticonsDataFactory.DataIndex.Value?.TryGetValue(octiconsKind, out data);
                    return data;
                case PackIconPicolIconsKind picolIconsKind:
                    PackIconPicolIconsDataFactory.DataIndex.Value?.TryGetValue(picolIconsKind, out data);
                    return data;
                case PackIconPixelartIconsKind pixelartIconsKind:
                    PackIconPixelartIconsDataFactory.DataIndex.Value?.TryGetValue(pixelartIconsKind, out data);
                    return data;
                case PackIconRPGAwesomeKind rpgAwesomeKind:
                    PackIconRPGAwesomeDataFactory.DataIndex.Value?.TryGetValue(rpgAwesomeKind, out data);
                    return data;
                case PackIconSimpleIconsKind simpleIconsKind:
                    PackIconSimpleIconsDataFactory.DataIndex.Value?.TryGetValue(simpleIconsKind, out data);
                    return data;
                case PackIconTypiconsKind typiconsKind:
                    PackIconTypiconsDataFactory.DataIndex.Value?.TryGetValue(typiconsKind, out data);
                    return data;
                case PackIconUniconsKind uniconsKind:
                    PackIconUniconsDataFactory.DataIndex.Value?.TryGetValue(uniconsKind, out data);
                    return data;
                case PackIconWeatherIconsKind weatherIconsKind:
                    PackIconWeatherIconsDataFactory.DataIndex.Value?.TryGetValue(weatherIconsKind, out data);
                    return data;
                case PackIconZondiconsKind zondiconsKind:
                    PackIconZondiconsDataFactory.DataIndex.Value?.TryGetValue(zondiconsKind, out data);
                    return data;
                default:
                    return null;
            }
        }

        /// <inheritdoc />
        protected override ScaleTransform GetScaleTransform(object iconKind)
        {
            return
                ((iconKind is PackIconBoxIconsKind)
                 || (iconKind is PackIconEvaIconsKind)
                 || (iconKind is PackIconJamIconsKind)
                 || (iconKind is PackIconMaterialDesignKind)
                 || (iconKind is PackIconRPGAwesomeKind)
                 || (iconKind is PackIconTypiconsKind))
                    ? new ScaleTransform(1, -1)
                    : new ScaleTransform(1, 1);
        }

        /// <inheritdoc />
        protected override DrawingGroup GetDrawingGroup(object iconKind, Brush foregroundBrush, string path)
        {
            var geometryDrawing = new GeometryDrawing
            {
                Geometry = Geometry.Parse(path)
            };

            if (iconKind is PackIconFeatherIconsKind)
            {
                var pen = new Pen(foregroundBrush, 2d)
                {
                    StartLineCap = PenLineCap.Round,
                    EndLineCap = PenLineCap.Round,
                    LineJoin = PenLineJoin.Round,
                };
                geometryDrawing.Pen = pen;
            }
            else
            {
                geometryDrawing.Brush = foregroundBrush;
            }

            var drawingGroup = new DrawingGroup
            {
                Children = { geometryDrawing },
                Transform = this.GetTransformGroup(iconKind)
            };

            return drawingGroup;
        }
    }
}