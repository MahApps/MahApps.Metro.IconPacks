using System.Windows.Media;

namespace MahApps.Metro.IconPacks.Converter
{
    public class PackIconFeatherIconsKindToImageConverter : PackIconKindToImageConverterBase
    {
        /// <inheritdoc />
        protected override string GetPathData(object iconKind)
        {
            string data = null;
            if (iconKind is PackIconFeatherIconsKind kind)
            {
                PackIconFeatherIconsDataFactory.DataIndex.Value?.TryGetValue(kind, out data);
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

            var pen = new Pen(foregroundBrush, 2d)
            {
                StartLineCap = PenLineCap.Round,
                EndLineCap = PenLineCap.Round,
                LineJoin = PenLineJoin.Round
            };
            geometryDrawing.Pen = pen;

            var drawingGroup = new DrawingGroup
            {
                Children = { geometryDrawing },
                Transform = this.GetTransformGroup(iconKind)
            };

            return drawingGroup;
        }
    }
}