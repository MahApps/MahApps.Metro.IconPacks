using System;
using System.Windows.Markup;
using System.Windows.Media.Animation;

namespace MahApps.Metro.IconPacks
{
    public class PackIconExtension<TPack,TKind>: MarkupExtension where TPack:PackIcon<TKind>,new()
    {
        public TKind Kind { get; set; }
        public double? Width { get; set; }
        public double? Height { get; set; }
        public PackIconFlipOrientation? Flip { get; set; }
        public double? Rotation { get; set; }
        public bool? Spin { get; set; }
        public bool? SpinAutoReverse { get; set; }
        public IEasingFunction SpinEasingFunction { get; set; }
        public double? SpinDuration { get; set; }
        public PackIconExtension() { }
        public PackIconExtension(TKind kind)
        {
            Kind = kind;
        }
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            var packIcon = new TPack { Kind = Kind };

            if (Width != null)
                packIcon.Width = Width.Value;
            if (Height != null)
                packIcon.Height = Height.Value;
            if (Flip != null)
                packIcon.Flip = Flip.Value;
            if (Rotation !=null)
                packIcon.Rotation = Rotation.Value;
            if (Spin !=null)
                packIcon.Spin = Spin.Value;
            if (SpinAutoReverse !=null)
                packIcon.SpinAutoReverse = SpinAutoReverse.Value;
            if (SpinEasingFunction !=null)
                packIcon.SpinEasingFunction = SpinEasingFunction;
            if (SpinDuration !=null)
                packIcon.SpinDuration = SpinDuration.Value;
            return packIcon;
        }
    }
}
