using System;
#if (NETFX_CORE || WINDOWS_UWP)
using Windows.UI.Xaml;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
#else
using System.Windows.Markup;
using System.Windows.Media.Animation;
#endif

namespace MahApps.Metro.IconPacks
{
    public interface IPackIconExtension
    {
        double? Width { get; set; }
        double? Height { get; set; }
        PackIconFlipOrientation? Flip { get; set; }
        double? RotationAngle { get; set; }
        bool? Spin { get; set; }
        bool? SpinAutoReverse { get; set; }
#if (NETFX_CORE || WINDOWS_UWP)
        EasingFunctionBase SpinEasingFunction { get; set; }
#else
        IEasingFunction SpinEasingFunction { get; set; }
#endif
        double? SpinDuration { get; set; }
    }

    public static class PackIconExtensionHelper
    {
        public static PackIconControl<TKind> GetPackIcon<TPack, TKind>(this IPackIconExtension packIconExtension, TKind kind) where TPack : PackIconControl<TKind>, new()
        {
            var packIcon = new TPack {Kind = kind};
            if (packIconExtension.Width != null)
                packIcon.Width = packIconExtension.Width.Value;
            if (packIconExtension.Height != null)
                packIcon.Height = packIconExtension.Height.Value;
            if (packIconExtension.Flip != null)
                packIcon.Flip = packIconExtension.Flip.Value;
            if (packIconExtension.RotationAngle != null)
                packIcon.RotationAngle = packIconExtension.RotationAngle.Value;
            if (packIconExtension.Spin != null)
                packIcon.Spin = packIconExtension.Spin.Value;
            if (packIconExtension.SpinAutoReverse != null)
                packIcon.SpinAutoReverse = packIconExtension.SpinAutoReverse.Value;
            if (packIconExtension.SpinEasingFunction != null)
                packIcon.SpinEasingFunction = packIconExtension.SpinEasingFunction;
            if (packIconExtension.SpinDuration != null)
                packIcon.SpinDuration = packIconExtension.SpinDuration.Value;
            return packIcon;
        }
    }

#if (NETFX_CORE || WINDOWS_UWP)
    [MarkupExtensionReturnType(ReturnType = typeof(PackIconBase))]
#else
    [MarkupExtensionReturnType(typeof(PackIconBase))]
#endif
    public abstract class PackIconExtension<TPack, TKind> : MarkupExtension, IPackIconExtension where TPack : PackIconControl<TKind>, new()
    {
#if !(NETFX_CORE || WINDOWS_UWP)
        [ConstructorArgument("kind")]
#endif
        public TKind Kind { get; set; }

        public double? Width { get; set; }
        public double? Height { get; set; }
        public PackIconFlipOrientation? Flip { get; set; }
        public double? RotationAngle { get; set; }
        public bool? Spin { get; set; }
        public bool? SpinAutoReverse { get; set; }
#if (NETFX_CORE || WINDOWS_UWP)
        public EasingFunctionBase SpinEasingFunction { get; set; }
#else
        public IEasingFunction SpinEasingFunction { get; set; }
#endif
        public double? SpinDuration { get; set; }

        protected PackIconExtension()
        {
        }

        protected PackIconExtension(TKind kind)
        {
            this.Kind = kind;
        }

#if (NETFX_CORE || WINDOWS_UWP)
        protected override object ProvideValue()
#else
        public override object ProvideValue(IServiceProvider serviceProvider)
#endif
        {
            return this.GetPackIcon<TPack, TKind>(this.Kind);
        }
    }
}