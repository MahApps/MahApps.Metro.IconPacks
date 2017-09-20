/*****************   NCore Softwares Pvt. Ltd., India   **************************

   ColorBox

   Copyright (C) 2013 NCore Softwares Pvt. Ltd.

   This program is provided to you under the terms of the Microsoft Public
   License (Ms-PL) as published at http://colorbox.codeplex.com/license

***********************************************************************************/

using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MahApps.Metro.IconPacks.Browser
{
    abstract class BaseSelector : FrameworkElement
    {
        public Orientation Orientation
        {
            get { return (Orientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        public static readonly DependencyProperty OrientationProperty =
            DependencyProperty.Register("Orientation", typeof(Orientation), typeof(BaseSelector),
            new UIPropertyMetadata(Orientation.Vertical));

        public Color Color
        {
            get { return (Color)GetValue(ColorProperty); }
            protected set { SetValue(ColorProperty, value); }
        }

        public static readonly DependencyProperty ColorProperty =
            DependencyProperty.Register("Color", typeof(Color), typeof(BaseSelector),
            new UIPropertyMetadata(Colors.Red));
    }
}
