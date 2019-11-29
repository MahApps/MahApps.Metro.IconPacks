using System.Windows.Media;

namespace MahApps.Metro.IconPacks.Converter
{
    public class PackIconKindToImageConverter : PackIconKindToImageConverterBase
    {
        /// <inheritdoc />
        protected override string GetPathData(object iconKind)
        {
            string data = null;
            if (iconKind is PackIconBoxIconsKind boxIconsKind)
            {
                PackIconBoxIconsDataFactory.DataIndex.Value?.TryGetValue(boxIconsKind, out data);
            }
            else if (iconKind is PackIconEntypoKind entypoKind)
            {
                PackIconEntypoDataFactory.DataIndex.Value?.TryGetValue(entypoKind, out data);
            }
            else if (iconKind is PackIconEvaIconsKind evaIconsKind)
            {
                PackIconEvaIconsDataFactory.DataIndex.Value?.TryGetValue(evaIconsKind, out data);
            }
            else if (iconKind is PackIconFeatherIconsKind featherIconsKind)
            {
                PackIconFeatherIconsDataFactory.DataIndex.Value?.TryGetValue(featherIconsKind, out data);
            }
            else if (iconKind is PackIconFontAwesomeKind fontAwesomeKind)
            {
                PackIconFontAwesomeDataFactory.DataIndex.Value?.TryGetValue(fontAwesomeKind, out data);
            }
            else if (iconKind is PackIconIoniconsKind ioniconsKind)
            {
                PackIconIoniconsDataFactory.DataIndex.Value?.TryGetValue(ioniconsKind, out data);
            }
            else if (iconKind is PackIconJamIconsKind jamIconsKind)
            {
                PackIconJamIconsDataFactory.DataIndex.Value?.TryGetValue(jamIconsKind, out data);
            }
            else if (iconKind is PackIconMaterialKind materialKind)
            {
                PackIconMaterialDataFactory.DataIndex.Value?.TryGetValue(materialKind, out data);
            }
            else if (iconKind is PackIconMaterialDesignKind materialDesignKind)
            {
                PackIconMaterialDesignDataFactory.DataIndex.Value?.TryGetValue(materialDesignKind, out data);
            }
            else if (iconKind is PackIconMaterialLightKind materialLightKind)
            {
                PackIconMaterialLightDataFactory.DataIndex.Value?.TryGetValue(materialLightKind, out data);
            }
            else if (iconKind is PackIconMicronsKind micronsKind)
            {
                PackIconMicronsDataFactory.DataIndex.Value?.TryGetValue(micronsKind, out data);
            }
            else if (iconKind is PackIconModernKind modernKind)
            {
                PackIconModernDataFactory.DataIndex.Value?.TryGetValue(modernKind, out data);
            }
            else if (iconKind is PackIconOcticonsKind octiconsKind)
            {
                PackIconOcticonsDataFactory.DataIndex.Value?.TryGetValue(octiconsKind, out data);
            }
            else if (iconKind is PackIconPicolIconsKind picolIconsKind)
            {
                PackIconPicolIconsDataFactory.DataIndex.Value?.TryGetValue(picolIconsKind, out data);
            }
            else if (iconKind is PackIconRPGAwesomeKind rpgAwesomeKind)
            {
                PackIconRPGAwesomeDataFactory.DataIndex.Value?.TryGetValue(rpgAwesomeKind, out data);
            }
            else if (iconKind is PackIconSimpleIconsKind simpleIconsKind)
            {
                PackIconSimpleIconsDataFactory.DataIndex.Value?.TryGetValue(simpleIconsKind, out data);
            }
            else if (iconKind is PackIconTypiconsKind typiconsKind)
            {
                PackIconTypiconsDataFactory.DataIndex.Value?.TryGetValue(typiconsKind, out data);
            }
            else if (iconKind is PackIconUniconsKind uniconsKind)
            {
                PackIconUniconsDataFactory.DataIndex.Value?.TryGetValue(uniconsKind, out data);
            }
            else if (iconKind is PackIconWeatherIconsKind weatherIconsKind)
            {
                PackIconWeatherIconsDataFactory.DataIndex.Value?.TryGetValue(weatherIconsKind, out data);
            }
            else if (iconKind is PackIconZondiconsKind zondiconsKind)
            {
                PackIconZondiconsDataFactory.DataIndex.Value?.TryGetValue(zondiconsKind, out data);
            }
            return data;
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

            var transform =
                ((iconKind is PackIconBoxIconsKind)
                 || (iconKind is PackIconEvaIconsKind)
                 || (iconKind is PackIconJamIconsKind)
                 || (iconKind is PackIconMaterialDesignKind)
                 || (iconKind is PackIconRPGAwesomeKind)
                 || (iconKind is PackIconTypiconsKind))
                    ? new ScaleTransform(1, -1)
                    : new ScaleTransform(1, 1);

            var drawingGroup = new DrawingGroup
            {
                Children = { geometryDrawing },
                Transform = transform
            };

            return drawingGroup;
        }
    }
}