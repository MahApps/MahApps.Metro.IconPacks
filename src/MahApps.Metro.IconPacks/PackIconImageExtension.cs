using System;
using System.Windows.Markup;
using System.Windows.Media;

namespace MahApps.Metro.IconPacks
{
    [MarkupExtensionReturnType(typeof(ImageSource))]
    public class PackIconImageExtension : BasePackIconImageExtension
    {
        public PackIconImageExtension()
        {
        }

        public PackIconImageExtension(Enum kind)
        {
            this.Kind = kind;
        }

        [ConstructorArgument("kind")]
        public Enum Kind { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return CreateImageSource(this.Kind, this.Brush ?? Brushes.Black);
        }

        /// <inheritdoc />
        protected override string GetPathData(object iconKind)
        {
            string data = null;
            switch (iconKind)
            {
                case PackIconBootstrapIconsKind kind:
                    PackIconBootstrapIconsDataFactory.DataIndex.Value?.TryGetValue(kind, out data);
                    return data;
                case PackIconBoxIconsKind kind:
                    PackIconBoxIconsDataFactory.DataIndex.Value?.TryGetValue(kind, out data);
                    return data;
                case PackIconCodiconsKind kind:
                    PackIconCodiconsDataFactory.DataIndex.Value?.TryGetValue(kind, out data);
                    return data;
                case PackIconCooliconsKind kind:
                    PackIconCooliconsDataFactory.DataIndex.Value?.TryGetValue(kind, out data);
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
                case PackIconFileIconsKind kind:
                    PackIconFileIconsDataFactory.DataIndex.Value?.TryGetValue(kind, out data);
                    return data;
                case PackIconFontaudioKind kind:
                    PackIconFontaudioDataFactory.DataIndex.Value?.TryGetValue(kind, out data);
                    return data;
                case PackIconFontAwesomeKind kind:
                    PackIconFontAwesomeDataFactory.DataIndex.Value?.TryGetValue(kind, out data);
                    return data;
                case PackIconFontistoKind kind:
                    PackIconFontistoDataFactory.DataIndex.Value?.TryGetValue(kind, out data);
                    return data;
                case PackIconForkAwesomeKind kind:
                    PackIconForkAwesomeDataFactory.DataIndex.Value?.TryGetValue(kind, out data);
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
                case PackIconRadixIconsKind kind:
                    PackIconRadixIconsDataFactory.DataIndex.Value?.TryGetValue(kind, out data);
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
                case PackIconVaadinIconsKind kind:
                    PackIconVaadinIconsDataFactory.DataIndex.Value?.TryGetValue(kind, out data);
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
                case PackIconBootstrapIconsKind _:
                case PackIconBoxIconsKind _:
                case PackIconCodiconsKind _:
                case PackIconCooliconsKind _:
                case PackIconEvaIconsKind _:
                case PackIconFileIconsKind _:
                case PackIconFontaudioKind _:
                case PackIconFontistoKind _:
                case PackIconForkAwesomeKind _:
                case PackIconJamIconsKind _:
                case PackIconMaterialDesignKind _:
                case PackIconRadixIconsKind _:
                case PackIconRemixIconKind _:
                case PackIconRPGAwesomeKind _:
                case PackIconTypiconsKind _:
                case PackIconVaadinIconsKind _:
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
                    LineJoin = PenLineJoin.Round
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
                Transform = this.GetScaleTransform(iconKind)
            };

            return drawingGroup;
        }
    }
}