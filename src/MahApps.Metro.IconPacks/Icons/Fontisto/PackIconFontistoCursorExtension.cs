using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;

namespace MahApps.Metro.IconPacks
{
    [MarkupExtensionReturnType(typeof(Cursor))]
    public class FontistoCursorExtension : FontistoImageExtension, IPackIconCursorExtension
    {
        public FontistoCursorExtension() : base() => base.Brush = PackIconCursorHelper.DefaultBrush;
        public FontistoCursorExtension(PackIconFontistoKind kind) : base(kind) => base.Brush = PackIconCursorHelper.DefaultBrush;

        /// <inheritdoc/>
        public Point HotSpot { get; set; }
        /// <inheritdoc/>
        public double Width { get; set; } = PackIconCursorHelper.DefaultWidth;
        /// <inheritdoc/>
        public double Height { get; set; } = PackIconCursorHelper.DefaultHeight;
        /// <inheritdoc/>
        public Brush StrokeBrush { get; set; }
        /// <inheritdoc/>
        public double StrokeThickness { get; set; } = PackIconCursorHelper.DefaultStrokeThickness;

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            TransformGroup transformGroup = (TransformGroup)GetTransformGroup(this.Kind);
            Geometry geometry = PackIconCursorHelper.GetCursorGeometry(GetPathData(this.Kind), transformGroup, Width, Height);
            return PackIconCursorHelper.GeometryToCursor(geometry, Brush, StrokeBrush, StrokeThickness, HotSpot);
        }
    }
}