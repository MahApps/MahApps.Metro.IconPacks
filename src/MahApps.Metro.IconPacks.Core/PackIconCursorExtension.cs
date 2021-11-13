using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace MahApps.Metro.IconPacks
{
    public interface IPackIconCursorExtension 
    {
        /// <summary>
        /// Get or set the hot spot point of the cursor (from (0,0) to (255,255)). Default point is upper left corner (0,0)
        /// </summary>
        Point HotSpot { get; set; }

        /// <summary>
        /// Get or set cursor width (1 to 256). Default value is 32
        /// </summary>
        double Width { get; set; }

        /// <summary>
        /// Get or set cursor height (1 to 256). Default value is 32
        /// </summary>
        double Height { get; set; }

        /// <summary>
        /// Get or set cursor outline color. If not set a black brush will be used.
        /// </summary>
        /// <remarks>
        /// If you don't want to draw an outline stroke, set <see cref="StrokeThickness"/> property to 0.
        /// If you set the <see cref="StrokeBrush"/> property to transparent, the outline will not be drawn, 
        /// but its thickness will be cut from the fill area.
        /// </remarks>
        Brush StrokeBrush { get; set; }

        /// <summary>
        /// Get or set cursor outline thickness. Default value is 0.5
        /// </summary>
        double StrokeThickness { get; set; }
    }
}
