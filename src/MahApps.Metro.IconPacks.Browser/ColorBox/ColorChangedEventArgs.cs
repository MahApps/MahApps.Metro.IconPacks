/*****************   NCore Softwares Pvt. Ltd., India   **************************

   ColorBox

   Copyright (C) 2013 NCore Softwares Pvt. Ltd.

   This program is provided to you under the terms of the Microsoft Public
   License (Ms-PL) as published at http://colorbox.codeplex.com/license

***********************************************************************************/

using System.Windows;
using System.Windows.Media;

namespace MahApps.Metro.IconPacks.Browser
{
    public class ColorChangedEventArgs : RoutedEventArgs
    {
        public ColorChangedEventArgs(RoutedEvent routedEvent, Color color)
        {
            this.RoutedEvent = routedEvent;
            this.Color = color;
        }

        private Color _Color;
        public Color Color
        {
            get { return _Color; }
            set { _Color = value; }
        }
    }
}
