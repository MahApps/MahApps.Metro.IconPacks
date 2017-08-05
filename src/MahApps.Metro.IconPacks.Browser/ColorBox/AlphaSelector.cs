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

namespace MahApps.Metro.IconPacks.Browser
{
    internal class AlphaSelector : BaseSelector
    {
        public double Alpha
        {
            get { return (double)GetValue(AlphaProperty); }
            set { SetValue(AlphaProperty, value); }
        }

        public static readonly DependencyProperty AlphaProperty =
            DependencyProperty.Register("Alpha", typeof(double), typeof(AlphaSelector),
                new FrameworkPropertyMetadata(1.0, new PropertyChangedCallback(AlphaChanged), new CoerceValueCallback(AlphaCoerce)));

        public static void AlphaChanged(object o, DependencyPropertyChangedEventArgs e)
        {
            AlphaSelector h = (AlphaSelector)o;
            h.SetAlphaOffset();
            h.SetColor();
        }

        public static object AlphaCoerce(DependencyObject d, object Brightness)
        {
            var v = (double)Brightness;
            if (v < 0) return 0.0;
            if (v > 1) return 1.0;
            return v;
        }

        public double AlphaOffset
        {
            get { return (double)GetValue(AlphaOffsetProperty); }
            private set { SetValue(AlphaOffsetProperty, value); }
        }

        public static readonly DependencyProperty AlphaOffsetProperty =
            DependencyProperty.Register("AlphaOffset", typeof(double), typeof(AlphaSelector),
                new UIPropertyMetadata(0.0));

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Point p = e.GetPosition(this);

                if (Orientation == Orientation.Vertical)
                {
                    Alpha = 1 - (p.Y / this.ActualHeight);
                }
                else
                {
                    Alpha = 1 - (p.X / this.ActualWidth);
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
                    Alpha = 1 - (p.Y / this.ActualHeight);
                }
                else
                {
                    Alpha = 1 - (p.X / this.ActualWidth);
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

            lb.GradientStops.Add(new GradientStop(Color.FromArgb(0xFF, 0x00, 0x00, 0x00), 0.00));
            lb.GradientStops.Add(new GradientStop(Color.FromArgb(0x00, 0x00, 0x00, 0x00), 1.00));

            dc.DrawRectangle(lb, null, new Rect(0, 0, ActualWidth, ActualHeight));

            SetAlphaOffset();
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            SetAlphaOffset();
            return base.ArrangeOverride(finalSize);
        }


        private void SetAlphaOffset()
        {
            double length = ActualHeight;
            if (Orientation == Orientation.Horizontal)
                length = ActualWidth;

            AlphaOffset = length - (length * Alpha);
        }

        private void SetColor()
        {
            Color = Color.FromArgb((byte)Math.Round(Alpha * 255), 0, 0, 0);
        }
    }
}
