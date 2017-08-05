/*****************   NCore Softwares Pvt. Ltd., India   **************************

   ColorBox

   Copyright (C) 2013 NCore Softwares Pvt. Ltd.

   This program is provided to you under the terms of the Microsoft Public
   License (Ms-PL) as published at http://colorbox.codeplex.com/license

***********************************************************************************/

using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace MahApps.Metro.IconPacks.Browser
{
    class SaturationBrightnessSelector : BaseSelector
    {
        public Thickness OffsetPadding
        {
            get { return (Thickness)GetValue(OffsetPaddingProperty); }
            set { SetValue(OffsetPaddingProperty, value); }
        }
        public static readonly DependencyProperty OffsetPaddingProperty =
            DependencyProperty.Register("OffsetPadding", typeof(Thickness), typeof(SaturationBrightnessSelector), new UIPropertyMetadata(new Thickness(0.0)));


        public double Hue
        {
            private get { return (double)GetValue(HueProperty); }
            set { SetValue(HueProperty, value); }
        }
        public static readonly DependencyProperty HueProperty =
            DependencyProperty.Register("Hue", typeof(double), typeof(SaturationBrightnessSelector), new
            FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback(HueChanged)));
        public static void HueChanged(object o, DependencyPropertyChangedEventArgs e)
        {
            SaturationBrightnessSelector h = (SaturationBrightnessSelector)o;
            h.SetColor();
        }


        public double SaturationOffset
        {
            get { return (double)GetValue(SaturationOffsetProperty); }
            set { SetValue(SaturationOffsetProperty, value); }
        }
        public static readonly DependencyProperty SaturationOffsetProperty =
            DependencyProperty.Register("SaturationOffset", typeof(double), typeof(SaturationBrightnessSelector), new UIPropertyMetadata(0.0));


        public double Saturation
        {
            get { return (double)GetValue(SaturationProperty); }
            set { SetValue(SaturationProperty, value); }
        }
        public static readonly DependencyProperty SaturationProperty =
            DependencyProperty.Register("Saturation", typeof(double), typeof(SaturationBrightnessSelector),
            new FrameworkPropertyMetadata(0.0, new PropertyChangedCallback(SaturationChanged), new CoerceValueCallback(SaturationCoerce)));
        public static void SaturationChanged(object o, DependencyPropertyChangedEventArgs e)
        {
            SaturationBrightnessSelector h = (SaturationBrightnessSelector)o;
            h.SetSaturationOffset();
        }
        public static object SaturationCoerce(DependencyObject d, object Brightness)
        {
            double v = (double)Brightness;
            if (v < 0) return 0.0;
            if (v > 1) return 1.0;
            return v;
        }


        public double BrightnessOffset
        {
            get { return (double)GetValue(BrightnessOffsetProperty); }
            set { SetValue(BrightnessOffsetProperty, value); }
        }
        public static readonly DependencyProperty BrightnessOffsetProperty =
            DependencyProperty.Register("BrightnessOffset", typeof(double), typeof(SaturationBrightnessSelector), new UIPropertyMetadata(0.0));


        public double Brightness
        {
            get { return (double)GetValue(BrightnessProperty); }
            set { SetValue(BrightnessProperty, value); }
        }
        public static readonly DependencyProperty BrightnessProperty =
            DependencyProperty.Register("Brightness", typeof(double), typeof(SaturationBrightnessSelector),
            new FrameworkPropertyMetadata(0.0, new PropertyChangedCallback(BrightnessChanged), new CoerceValueCallback(BrightnessCoerce)));
        public static void BrightnessChanged(object o, DependencyPropertyChangedEventArgs e)
        {
            SaturationBrightnessSelector h = (SaturationBrightnessSelector)o;
            h.SetBrightnessOffset();
        }
        public static object BrightnessCoerce(DependencyObject d, object Brightness)
        {
            double v = (double)Brightness;
            if (v < 0) return 0.0;
            if (v > 1) return 1.0;
            return v;
        }



        protected override void OnMouseMove(System.Windows.Input.MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Point p = e.GetPosition(this);
                Saturation = (p.X / (this.ActualWidth - OffsetPadding.Right));
                Brightness = (((this.ActualHeight - OffsetPadding.Bottom) - p.Y) / (this.ActualHeight - OffsetPadding.Bottom));
                SetColor();
            }
            base.OnMouseMove(e);
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            Point p = e.GetPosition(this);
            Saturation = (p.X / (this.ActualWidth - OffsetPadding.Right));
            Brightness = (((this.ActualHeight - OffsetPadding.Bottom) - p.Y) / (this.ActualHeight - OffsetPadding.Bottom));
            SetColor();

            Mouse.Capture(this);
            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            this.ReleaseMouseCapture();
            base.OnMouseUp(e);
        }

        protected override void OnRender(DrawingContext dc)
        {
            LinearGradientBrush h = new LinearGradientBrush();
            h.StartPoint = new Point(0, 0);
            h.EndPoint = new Point(1, 0);
            h.GradientStops.Add(new GradientStop(Colors.White, 0.00));
            h.GradientStops.Add(new GradientStop(ColorHelper.ColorFromHSB(Hue, 1, 1), 1.0));
            dc.DrawRectangle(h, null, new Rect(0, 0, ActualWidth, ActualHeight));

            LinearGradientBrush v = new LinearGradientBrush();
            v.StartPoint = new Point(0, 0);
            v.EndPoint = new Point(0, 1);
            v.GradientStops.Add(new GradientStop(Color.FromArgb(0xFF, 0, 0, 0), 1.00));
            v.GradientStops.Add(new GradientStop(Color.FromArgb(0x80, 0, 0, 0), 0.50));
            v.GradientStops.Add(new GradientStop(Color.FromArgb(0x00, 0, 0, 0), 0.00));
            dc.DrawRectangle(v, null, new Rect(0, 0, ActualWidth, ActualHeight));

            SetSaturationOffset();
            SetBrightnessOffset();
        }

        private void SetSaturationOffset()
        {
            SaturationOffset = OffsetPadding.Left + ((ActualWidth - (OffsetPadding.Right + OffsetPadding.Left)) * Saturation);
        }

        private void SetBrightnessOffset()
        {
            BrightnessOffset = OffsetPadding.Top + ((ActualHeight - (OffsetPadding.Bottom + OffsetPadding.Top)) - ((ActualHeight - (OffsetPadding.Bottom + OffsetPadding.Top)) * Brightness));
        }

        public void SetColor()
        {
            Color = ColorHelper.ColorFromHSB(Hue, Saturation, Brightness);
        }
    }
}
