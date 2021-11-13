using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.IO;

namespace MahApps.Metro.IconPacks
{
    public static class PackIconCursorHelper
    {
        /// <summary>
        /// Get the <see cref="Geometry"/> that will be used to create the cursor
        /// </summary>
        /// <param name="pathData"> String data of icon geometry.</param>
        /// <param name="transformGroup"> Transformation group of the icon (we get it from <see cref="BasePackIconImageExtension.GetPathData(object)"/>).</param>
        /// <param name="width"> Width of cursor (optional, between 1 and 256, default is 32)</param>
        /// <param name="height"> Width of cursor (optional, between 1 and 256, default is 32)</param>
        /// <returns>
        /// A <see cref="Geometry"/> object that contains the icon to be used in the cursor.
        /// The <see cref="Geometry"/> has all transformations to be scaled to desired size and aligned to top left corner.
        /// </returns>
        public static Geometry GetCursorGeometry(string pathData, TransformGroup transformGroup, double width = 32, double height = 32)
        {
            if (width < 1 || width > 256)
            {
                throw new ArgumentOutOfRangeException(nameof(width) + " should be between 1 and 256.");
            }
            if (height < 1 || height > 256)
            {
                throw new ArgumentOutOfRangeException(nameof(height) + " should be between 1 and 256.");
            }

            Geometry geometry = Geometry.Parse(pathData); // This geometry is frozen and can not be modified
            geometry = geometry.Clone(); // Create a modifiable copy of geometry.

            // Apply icon pack transformation
            geometry.Transform = transformGroup;

            // The resulting geometry bounds may not be aligned to (0,0) point. We need to shifted.
            Rect rect = geometry.Bounds;
            transformGroup.Children.Add(new TranslateTransform(-rect.X, -rect.Y));

            // Apply the requested size.
            transformGroup.Children.Add(new ScaleTransform(width / rect.Width, height / rect.Height));


            geometry.Freeze();
            return geometry;
        }

        /// <summary>
        /// Get <see cref="Cursor"/> from a <see cref="Geometry"/> object
        /// </summary>
        /// <param name="geometry"> Cursor <see cref="Geometry"/></param>
        /// <param name="brush"> Fill <see cref="Brush"/>. If not set a white brush will be used.</param>
        /// <param name="strokeBrush"> Outline <see cref="Brush"/>. If not set a black brush will be used.</param>
        /// <param name="strokeThickness"> Outline thickness. Default is 0.5</param>
        /// <param name="hotspot">Hot spot point of the cursor (from (0,0) to (255,255)). Default is (0,0)</param>
        /// <returns>
        /// A <see cref="Cursor"/>.
        /// </returns>
        public static Cursor GeometryToCursor(Geometry geometry, Brush brush = null, Brush strokeBrush = null, double strokeThickness = 0.5, Point hotspot = default)
        {
            if (hotspot.X < 0 || hotspot.X > 255)
            {
                throw new ArgumentOutOfRangeException("hotspot.X should be between 0 and 255.");
            }
            if (hotspot.Y < 0 || hotspot.Y > 255)
            {
                throw new ArgumentOutOfRangeException("hotspot.Y should be between 0 and 255.");
            }

            Pen pen = new Pen(strokeBrush ?? DefaultStrokeBrush, strokeThickness);
            brush = brush ?? DefaultBrush;

            var visual = new DrawingVisual();
            using (DrawingContext dc = visual.RenderOpen())
            {
                dc.DrawGeometry(brush, pen, geometry);
            }
            Rect rect = visual.ContentBounds;

            int width = (int)rect.Width;
            if (width < 1)
            {
                width = (int)DefaultWidth;
            }
            int height = (int)rect.Height;
            if (height < 1)
            {
                height = (int)DefaultHeight;
            }

            RenderTargetBitmap renderTargetBitmap = new RenderTargetBitmap(width, height, 96, 96, PixelFormats.Pbgra32);
            renderTargetBitmap.Render(visual);
            return BitmapSourceToCursor(renderTargetBitmap, hotspot);
        }

        /// <summary>
        /// Convert a <see cref="BitmapSource"/> to a <see cref="Cursor"/> object.
        /// </summary>
        /// <param name="bitmapSource">A <see cref="BitmapSource"/> to be converted.</param>
        /// <param name="hotSpot"> A <see cref="Point"/> of cursor hot spot</param>
        /// <returns>
        /// A <see cref="Cursor"/>.
        /// </returns>
        public static Cursor BitmapSourceToCursor(BitmapSource bitmapSource, Point hotSpot)
        {
            if (hotSpot.X < 0 || hotSpot.X > 255)
            {
                throw new ArgumentOutOfRangeException("hotspot.X should be between 0 and 255.");
            }
            if (hotSpot.Y < 0 || hotSpot.Y > 255)
            {
                throw new ArgumentOutOfRangeException("hotspot.Y should be between 0 and 255.");
            }

            if (bitmapSource.Width < 1 || bitmapSource.Width > 256)
            {
                throw new ArgumentOutOfRangeException("BitmapSource.Width should be between 1 and 256.");
            }
            if (bitmapSource.Height < 1 || bitmapSource.Height > 256)
            {
                throw new ArgumentOutOfRangeException("BitmapSource.Height should be between 1 and 256.");
            }
            
            double width = bitmapSource.Width == 256 ? 0 :  bitmapSource.Width;
            double height = bitmapSource.Height == 256 ? 0 :  bitmapSource.Height;

            // Taken from https://stackoverflow.com/questions/46805/custom-cursor-in-wpf/27077188#27077188

            var pngStream = new MemoryStream();
            PngBitmapEncoder png = new PngBitmapEncoder();
            png.Frames.Add(BitmapFrame.Create(bitmapSource));
            png.Save(pngStream);

            // write cursor header info
            var cursorStream = new MemoryStream();
            cursorStream.Write(new byte[2] { 0x00, 0x00 }, 0, 2);                               // ICONDIR: Reserved. Must always be 0.
            cursorStream.Write(new byte[2] { 0x02, 0x00 }, 0, 2);                               // ICONDIR: Specifies image type: 1 for icon (.ICO) image, 2 for cursor (.CUR) image. Other values are invalid
            cursorStream.Write(new byte[2] { 0x01, 0x00 }, 0, 2);                               // ICONDIR: Specifies number of images in the file.
            cursorStream.Write(new byte[1] { (byte)width }, 0, 1);                              // ICONDIRENTRY: Specifies image width in pixels. Can be any number between 0 and 255. Value 0 means image width is 256 pixels.
            cursorStream.Write(new byte[1] { (byte)height }, 0, 1);                             // ICONDIRENTRY: Specifies image height in pixels. Can be any number between 0 and 255. Value 0 means image height is 256 pixels.
            cursorStream.Write(new byte[1] { 0x00 }, 0, 1);                                     // ICONDIRENTRY: Specifies number of colors in the color palette. Should be 0 if the image does not use a color palette.
            cursorStream.Write(new byte[1] { 0x00 }, 0, 1);                                     // ICONDIRENTRY: Reserved. Should be 0.
            cursorStream.Write(new byte[2] { (byte)hotSpot.X, 0x00 }, 0, 2);                    // ICONDIRENTRY: Specifies the horizontal coordinates of the hot spot in number of pixels from the left.
            cursorStream.Write(new byte[2] { (byte)hotSpot.Y, 0x00 }, 0, 2);                    // ICONDIRENTRY: Specifies the vertical coordinates of the hot spot in number of pixels from the top.
            cursorStream.Write(new byte[4] {                                                    // ICONDIRENTRY: Specifies the size of the image's data in bytes
                                          (byte)(pngStream.Length & 0x000000FF),
                                          (byte)((pngStream.Length & 0x0000FF00) >> 0x08),
                                          (byte)((pngStream.Length & 0x00FF0000) >> 0x10),
                                          (byte)((pngStream.Length & 0xFF000000) >> 0x18)
                                       }, 0, 4);
            cursorStream.Write(new byte[4] {                                                    // ICONDIRENTRY: Specifies the offset of BMP or PNG data from the beginning of the ICO/CUR file
                                          (byte)0x16,
                                          (byte)0x00,
                                          (byte)0x00,
                                          (byte)0x00,
                                       }, 0, 4);

            // copy PNG stream to cursor stream
            pngStream.Seek(0, SeekOrigin.Begin);
            pngStream.CopyTo(cursorStream);

            // return cursor stream
            cursorStream.Seek(0, SeekOrigin.Begin);
            return new Cursor(cursorStream);
        }
        
        /// <summary>
        /// Get or set the default width of cursor (32 by default).
        /// </summary>
        public static double DefaultWidth { get; set; } = 32;
        
        /// <summary>
        /// Get or set the default height of cursor (32 by default).
        /// </summary>
        public static double DefaultHeight { get; set; } = 32;
        
        /// <summary>
        /// Get or set the default stroke thickness of cursor (1.0 by default).
        /// </summary>
        public static double DefaultStrokeThickness { get; set; } = 1.0;
         
        /// <summary>
        /// Get or set the default stroke brush of cursor (Black by default).
        /// </summary>
       public static Brush DefaultStrokeBrush { get; set; } = Brushes.Black;
        
        /// <summary>
        /// Get or set the default fill brush of cursor (Black by default).
        /// </summary>
        public static Brush DefaultBrush { get; set; } = Brushes.Black;
    }
}
