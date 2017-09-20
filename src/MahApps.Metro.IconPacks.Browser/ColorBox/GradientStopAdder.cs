/*****************   NCore Softwares Pvt. Ltd., India   **************************

   ColorBox

   Copyright (C) 2013 NCore Softwares Pvt. Ltd.

   This program is provided to you under the terms of the Microsoft Public
   License (Ms-PL) as published at http://colorbox.codeplex.com/license

***********************************************************************************/

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MahApps.Metro.IconPacks.Browser
{
    internal class GradientStopAdder : Button
    {
        protected override void OnPreviewMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseLeftButtonDown(e);

            if (e.Source is GradientStopAdder && this.ColorBox != null)
            {
                Button btn = e.Source as Button;

                GradientStop _gs = new GradientStop();
                _gs.Offset = Mouse.GetPosition(btn).X / btn.ActualWidth;
                _gs.Color = GetColorFromImage(e.GetPosition(this));
                this.ColorBox.Gradients.Add(_gs);
                this.ColorBox.SelectedGradient = _gs;
                this.ColorBox.Color = _gs.Color;
                this.ColorBox.SetBrush();
            }
        }

        Color GetColorFromImage(Point p)
        {
            try
            {
                Rect bounds = VisualTreeHelper.GetDescendantBounds(this);
                RenderTargetBitmap rtb = new RenderTargetBitmap((Int32)bounds.Width, (Int32)bounds.Height, 96, 96, PixelFormats.Default);
                rtb.Render(this);

                byte[] arr;
                PngBitmapEncoder png = new PngBitmapEncoder();
                png.Frames.Add(BitmapFrame.Create(rtb));
                using (var stream = new System.IO.MemoryStream())
                {
                    png.Save(stream);
                    arr = stream.ToArray();
                }

                BitmapSource bitmap = BitmapFrame.Create(new System.IO.MemoryStream(arr));

                byte[] pixels = new byte[4];
                CroppedBitmap cb = new CroppedBitmap(bitmap, new Int32Rect((int)p.X, (int)p.Y, 1, 1));
                cb.CopyPixels(pixels, 4, 0);
                return Color.FromArgb(pixels[3], pixels[2], pixels[1], pixels[0]);
            }
            catch (Exception)
            {
                return this.ColorBox.Color;
            }
        }

        public ColorBox ColorBox
        {
            get { return (ColorBox)GetValue(ColorBoxProperty); }
            set { SetValue(ColorBoxProperty, value); }
        }
        public static readonly DependencyProperty ColorBoxProperty =
            DependencyProperty.Register("ColorBox", typeof(ColorBox), typeof(GradientStopAdder));
    }
}
