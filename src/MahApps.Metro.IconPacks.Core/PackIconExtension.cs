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
        double Width { get; set; }
        double Height { get; set; }
        PackIconFlipOrientation Flip { get; set; }
        double RotationAngle { get; set; }
        bool Spin { get; set; }
        bool SpinAutoReverse { get; set; }
#if (NETFX_CORE || WINDOWS_UWP)
        EasingFunctionBase SpinEasingFunction { get; set; }
#else
        IEasingFunction SpinEasingFunction { get; set; }
#endif
        double SpinDuration { get; set; }
    }

    public static class PackIconExtensionHelper
    {
        public static PackIconControlBase GetPackIcon<TPack, TKind>(this IPackIconExtension packIconExtension, TKind kind) where TPack : PackIconControlBase, new()
        {
            var packIcon = new TPack();
            packIcon.SetKind(kind);
            packIcon.Width = packIconExtension.Width;
            packIcon.Height = packIconExtension.Height;
            packIcon.Flip = packIconExtension.Flip;
            packIcon.RotationAngle = packIconExtension.RotationAngle;
            packIcon.Spin = packIconExtension.Spin;
            packIcon.SpinAutoReverse = packIconExtension.SpinAutoReverse;
            if (packIconExtension.SpinEasingFunction != null)
            {
                packIcon.SpinEasingFunction = packIconExtension.SpinEasingFunction;
            }
            packIcon.SpinDuration = packIconExtension.SpinDuration;
            return packIcon;
        }
    }

#if (NETFX_CORE || WINDOWS_UWP)
    [MarkupExtensionReturnType(ReturnType = typeof(PackIconBase))]
#else
    [MarkupExtensionReturnType(typeof(PackIconBase))]
#endif
    public abstract class BasePackIconExtension : MarkupExtension, IPackIconExtension
    {
        public double Width { get; set; } = 16;
        public double Height { get; set; } = 16;
        public PackIconFlipOrientation Flip { get; set; } = PackIconFlipOrientation.Normal;
        public double RotationAngle { get; set; } = 0d;
        public bool Spin { get; set; } = false;
        public bool SpinAutoReverse { get; set; } = false;
#if (NETFX_CORE || WINDOWS_UWP)
        public EasingFunctionBase SpinEasingFunction { get; set; } = null;
#else
        public IEasingFunction SpinEasingFunction { get; set; } = null;
#endif
        public double SpinDuration { get; set; } = 1d;
    }
}