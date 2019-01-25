using System;
using System.Globalization;
using System.Windows.Media;

namespace MahApps.Metro.IconPacks.Converter
{
    /// <summary>
    /// Converts any given PackIcon*Kind enum to a ImageSource.
    /// </summary>
    public class IconKindToImageConverter : MarkupConverter
    {
        /// <summary>
        /// Gets or sets the thickness to draw the icon with.
        /// </summary>
        public double Thickness { get; set; } = 0.25;

        /// <summary>
        /// Gets or sets the ScaleTransform icon that is to be created.
        /// </summary>
        public ScaleTransform ScaleTransform { get; set; } = new ScaleTransform(1, 1);

        protected override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var foregroundBrush = parameter as Brush ?? Brushes.Black;

            switch (value)
            {
                case PackIconEntypoKind kind:
                    return ConvertToImage(new PackIconEntypo() { Kind = kind }.Data, foregroundBrush);

                case PackIconFontAwesomeKind kind:
                    return ConvertToImage(new PackIconFontAwesome() { Kind = kind }.Data, foregroundBrush);

                case PackIconMaterialKind kind:
                    return ConvertToImage(new PackIconMaterial() { Kind = kind }.Data, foregroundBrush);

                case PackIconMaterialLightKind kind:
                    return ConvertToImage(new PackIconMaterialLight() { Kind = kind }.Data, foregroundBrush);

                case PackIconModernKind kind:
                    return ConvertToImage(new PackIconModern() { Kind = kind }.Data, foregroundBrush);

                case PackIconOcticonsKind kind:
                    return ConvertToImage(new PackIconOcticons() { Kind = kind }.Data, foregroundBrush);

                case PackIconSimpleIconsKind kind:
                    return ConvertToImage(new PackIconSimpleIcons() { Kind = kind }.Data, foregroundBrush);

                default:
                    return null;
            }
        }

        protected override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }

        private object ConvertToImage(string path, Brush foregroundBrush)
        {
            var geometryDrawing = new GeometryDrawing
            {
                Geometry = Geometry.Parse(path),
                Brush = foregroundBrush,
                Pen = new Pen(foregroundBrush, Thickness)
            };

            var drawingGroup = new DrawingGroup
            {
                Children = { geometryDrawing },
                Transform = ScaleTransform,
            };

            return new DrawingImage(drawingGroup);
        }
    }
}