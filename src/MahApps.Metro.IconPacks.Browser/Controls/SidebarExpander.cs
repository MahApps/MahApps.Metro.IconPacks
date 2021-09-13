using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace MahApps.Metro.IconPacks.Browser.Controls
{
    [TemplatePart (Name = nameof(PART_ResizingThumb), Type = typeof(Thumb))]
    public class SidebarExpander : Expander
    {
        private Thumb PART_ResizingThumb;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            PART_ResizingThumb = (Thumb)GetTemplateChild(nameof(PART_ResizingThumb));
            PART_ResizingThumb.DragDelta += PART_ResizingThumb_DragDelta;
        }

        private void PART_ResizingThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            // We only want to resize if we are open
            if (IsExpanded)
            {
                var newWidth = ActualWidth - e.HorizontalChange;
                if (newWidth < MinWidth)
                {
                    newWidth = MinWidth;
                }
                if (newWidth > MaxWidth)
                {
                    newWidth = MaxWidth;
                }
                MahApps.Metro.IconPacks.Browser.Properties.Settings.Default.SidebarExpandedWidth = newWidth;
            }
        }


        // Helper for the animation
        public double AnimateWidthFactor
        {
            get { return (double)GetValue(AnimateWidthFactorProperty); }
            set { SetValue(AnimateWidthFactorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AnimateWidthFactor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AnimateWidthFactorProperty = DependencyProperty.Register("AnimateWidthFactor", typeof(double), typeof(SidebarExpander), new PropertyMetadata(0d));


    }
}
