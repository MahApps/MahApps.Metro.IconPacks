﻿using System.Windows.Media;

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
                case PackIconBoxIconsKind kind:
                    PackIconBoxIconsDataFactory.DataIndex.Value?.TryGetValue(kind, out data);
                    return data;
                case PackIconEntypoKind kind:
                    PackIconEntypoDataFactory.DataIndex.Value?.TryGetValue(kind, out data);
                    return data;
                case PackIconEvaIconsKind kind:
                    PackIconEvaIconsDataFactory.DataIndex.Value?.TryGetValue(kind, out data);
                    return data;
                case PackIconFeatherIconsKind kind:
                    PackIconFeatherIconsDataFactory.DataIndex.Value?.TryGetValue(kind, out data);
                    return data;
                case PackIconFontAwesomeKind kind:
                    PackIconFontAwesomeDataFactory.DataIndex.Value?.TryGetValue(kind, out data);
                    return data;
                case PackIconIoniconsKind kind:
                    PackIconIoniconsDataFactory.DataIndex.Value?.TryGetValue(kind, out data);
                    return data;
                case PackIconJamIconsKind kind:
                    PackIconJamIconsDataFactory.DataIndex.Value?.TryGetValue(kind, out data);
                    return data;
                case PackIconMaterialDesignKind kind:
                    PackIconMaterialDesignDataFactory.DataIndex.Value?.TryGetValue(kind, out data);
                    return data;
                case PackIconMaterialKind kind:
                    PackIconMaterialDataFactory.DataIndex.Value?.TryGetValue(kind, out data);
                    return data;
                case PackIconMaterialLightKind kind:
                    PackIconMaterialLightDataFactory.DataIndex.Value?.TryGetValue(kind, out data);
                    return data;
                case PackIconMicronsKind kind:
                    PackIconMicronsDataFactory.DataIndex.Value?.TryGetValue(kind, out data);
                    return data;
                case PackIconModernKind kind:
                    PackIconModernDataFactory.DataIndex.Value?.TryGetValue(kind, out data);
                    return data;
                case PackIconOcticonsKind kind:
                    PackIconOcticonsDataFactory.DataIndex.Value?.TryGetValue(kind, out data);
                    return data;
                case PackIconPicolIconsKind kind:
                    PackIconPicolIconsDataFactory.DataIndex.Value?.TryGetValue(kind, out data);
                    return data;
                case PackIconPixelartIconsKind kind:
                    PackIconPixelartIconsDataFactory.DataIndex.Value?.TryGetValue(kind, out data);
                    return data;
                case PackIconRemixIconKind kind:
                    PackIconRemixIconDataFactory.DataIndex.Value?.TryGetValue(kind, out data);
                    return data;
                case PackIconRPGAwesomeKind kind:
                    PackIconRPGAwesomeDataFactory.DataIndex.Value?.TryGetValue(kind, out data);
                    return data;
                case PackIconSimpleIconsKind kind:
                    PackIconSimpleIconsDataFactory.DataIndex.Value?.TryGetValue(kind, out data);
                    return data;
                case PackIconTypiconsKind kind:
                    PackIconTypiconsDataFactory.DataIndex.Value?.TryGetValue(kind, out data);
                    return data;
                case PackIconUniconsKind kind:
                    PackIconUniconsDataFactory.DataIndex.Value?.TryGetValue(kind, out data);
                    return data;
                case PackIconWeatherIconsKind kind:
                    PackIconWeatherIconsDataFactory.DataIndex.Value?.TryGetValue(kind, out data);
                    return data;
                case PackIconZondiconsKind kind:
                    PackIconZondiconsDataFactory.DataIndex.Value?.TryGetValue(kind, out data);
                    return data;
                default:
                    return null;
            }
        }

        /// <inheritdoc />
        protected override ScaleTransform GetScaleTransform(object iconKind)
        {
            switch (iconKind)
            {
                case PackIconBoxIconsKind _:
                case PackIconEvaIconsKind _:
                case PackIconJamIconsKind _:
                case PackIconMaterialDesignKind _:
                case PackIconRemixIconKind _:
                case PackIconRPGAwesomeKind _:
                case PackIconTypiconsKind _:
                    return new ScaleTransform(1, -1);
                default:
                    return new ScaleTransform(1, 1);
            }
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