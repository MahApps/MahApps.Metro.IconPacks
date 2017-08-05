/*****************   NCore Softwares Pvt. Ltd., India   **************************

   ColorBox

   Copyright (C) 2013 NCore Softwares Pvt. Ltd.

   This program is provided to you under the terms of the Microsoft Public
   License (Ms-PL) as published at http://colorbox.codeplex.com/license

***********************************************************************************/

using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace MahApps.Metro.IconPacks.Browser
{
    class HueSelector : BaseSelector
    {
        public double Hue
        {
            get { return (double)GetValue(HueProperty); }
            set { SetValue(HueProperty, value); }
        }
        public static readonly DependencyProperty HueProperty =
            DependencyProperty.Register("Hue", typeof(double), typeof(HueSelector),
            new FrameworkPropertyMetadata(0.0, new PropertyChangedCallback(HueChanged), new CoerceValueCallback(HueCoerce)));
        public static void HueChanged(object o, DependencyPropertyChangedEventArgs e)
        {
            HueSelector h = (HueSelector)o;
            h.SetHueOffset();
            h.SetColor();
        }
        public static object HueCoerce(DependencyObject d, object Brightness)
        {
            double v = (double)Brightness;
            if (v < 0) return 0.0;
            if (v > 1) return 1.0;
            return v;
        }


        public double HueOffset
        {
            get { return (double)GetValue(HueOffsetProperty); }
            private set { SetValue(HueOffsetProperty, value); }
        }
        public static readonly DependencyProperty HueOffsetProperty =
            DependencyProperty.Register("HueOffset", typeof(double), typeof(HueSelector), new UIPropertyMetadata(0.0));


        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Point p = e.GetPosition(this);

                if (Orientation == Orientation.Vertical)
                {
                    Hue = 1 - (p.Y / this.ActualHeight);
                }
                else
                {
                    Hue = 1 - (p.X / this.ActualWidth);
                }
            }
            base.OnMouseMove(e);
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Point p = e.GetPosition(this);

                if (Orientation == Orientation.Vertical)
                {
                    Hue = 1 - (p.Y / this.ActualHeight);
                }
                else
                {
                    Hue = 1 - (p.X / this.ActualWidth);
                }
            }
            Mouse.Capture(this);
            base.OnMouseMove(e);
        }

        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            this.ReleaseMouseCapture();
            base.OnMouseUp(e);
        }

        protected override void OnRender(DrawingContext dc)
        {
            LinearGradientBrush lb = new LinearGradientBrush();

            lb.StartPoint = new Point(0, 0);

            if (Orientation == Orientation.Vertical)
                lb.EndPoint = new Point(0, 1);
            else
                lb.EndPoint = new Point(1, 0);

            lb.GradientStops.Add(new GradientStop(Color.FromRgb(0xFF, 0x00, 0x00), 1.00));
            lb.GradientStops.Add(new GradientStop(Color.FromRgb(0xFF, 0xFF, 0x00), 0.85));
            lb.GradientStops.Add(new GradientStop(Color.FromRgb(0x00, 0xFF, 0x00), 0.76));
            lb.GradientStops.Add(new GradientStop(Color.FromRgb(0x00, 0xFF, 0xFF), 0.50));
            lb.GradientStops.Add(new GradientStop(Color.FromRgb(0x00, 0x00, 0xFF), 0.33));
            lb.GradientStops.Add(new GradientStop(Color.FromRgb(0xFF, 0x00, 0xFF), 0.16));
            lb.GradientStops.Add(new GradientStop(Color.FromRgb(0xFF, 0x00, 0x00), 0.00));

            dc.DrawRectangle(lb, null, new Rect(0, 0, ActualWidth, ActualHeight));

            SetHueOffset();
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            SetHueOffset();
            return base.ArrangeOverride(finalSize);
        }


        private void SetHueOffset()
        {
            double length = ActualHeight;
            if (Orientation == Orientation.Horizontal)
                length = ActualWidth;

            HueOffset = length - (length * Hue);
        }

        private void SetColor()
        {
            base.Color = ColorHelper.ColorFromHSB(Hue, 1, 1);
        }
    }
}
