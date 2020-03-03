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

            if (((BasePackIconExtension) packIconExtension).IsFieldChanged(BasePackIconExtension.ChangedFieldFlags.Width))
            {
                packIcon.Width = packIconExtension.Width;
            }

            if (((BasePackIconExtension) packIconExtension).IsFieldChanged(BasePackIconExtension.ChangedFieldFlags.Height))
            {
                packIcon.Height = packIconExtension.Height;
            }

            if (((BasePackIconExtension) packIconExtension).IsFieldChanged(BasePackIconExtension.ChangedFieldFlags.Flip))
            {
                packIcon.Flip = packIconExtension.Flip;
            }

            if (((BasePackIconExtension) packIconExtension).IsFieldChanged(BasePackIconExtension.ChangedFieldFlags.RotationAngle))
            {
                packIcon.RotationAngle = packIconExtension.RotationAngle;
            }

            if (((BasePackIconExtension) packIconExtension).IsFieldChanged(BasePackIconExtension.ChangedFieldFlags.Spin))
            {
                packIcon.Spin = packIconExtension.Spin;
            }

            if (((BasePackIconExtension) packIconExtension).IsFieldChanged(BasePackIconExtension.ChangedFieldFlags.SpinAutoReverse))
            {
                packIcon.SpinAutoReverse = packIconExtension.SpinAutoReverse;
            }

            if (((BasePackIconExtension) packIconExtension).IsFieldChanged(BasePackIconExtension.ChangedFieldFlags.SpinEasingFunction))
            {
                packIcon.SpinEasingFunction = packIconExtension.SpinEasingFunction;
            }

            if (((BasePackIconExtension) packIconExtension).IsFieldChanged(BasePackIconExtension.ChangedFieldFlags.SpinDuration))
            {
                packIcon.SpinDuration = packIconExtension.SpinDuration;
            }

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
        private double _width = 16d;

        public double Width
        {
            get => _width;
            set
            {
                if (Equals(_width, value))
                {
                    return;
                }

                _width = value;
                WriteFieldChangedFlag(ChangedFieldFlags.Width, true);
            }
        }

        private double _height = 16d;

        public double Height
        {
            get => _height;
            set
            {
                if (Equals(_height, value))
                {
                    return;
                }

                _height = value;
                WriteFieldChangedFlag(ChangedFieldFlags.Height, true);
            }
        }

        private PackIconFlipOrientation _flip = PackIconFlipOrientation.Normal;

        public PackIconFlipOrientation Flip
        {
            get => _flip;
            set
            {
                if (Equals(_flip, value))
                {
                    return;
                }

                _flip = value;
                WriteFieldChangedFlag(ChangedFieldFlags.Flip, true);
            }
        }

        private double _rotationAngle = 0d;

        public double RotationAngle
        {
            get => _rotationAngle;
            set
            {
                if (Equals(_rotationAngle, value))
                {
                    return;
                }

                _rotationAngle = value;
                WriteFieldChangedFlag(ChangedFieldFlags.RotationAngle, true);
            }
        }

        private bool _spin;

        public bool Spin
        {
            get => _spin;
            set
            {
                if (Equals(_spin, value))
                {
                    return;
                }

                _spin = value;
                WriteFieldChangedFlag(ChangedFieldFlags.Spin, true);
            }
        }

        private bool _spinAutoReverse;

        public bool SpinAutoReverse
        {
            get => _spinAutoReverse;
            set
            {
                if (Equals(_spinAutoReverse, value))
                {
                    return;
                }

                _spinAutoReverse = value;
                WriteFieldChangedFlag(ChangedFieldFlags.SpinAutoReverse, true);
            }
        }

#if (NETFX_CORE || WINDOWS_UWP)
        private EasingFunctionBase _spinEasingFunction = null;

        public EasingFunctionBase SpinEasingFunction
#else
        private IEasingFunction _spinEasingFunction = null;

        public IEasingFunction SpinEasingFunction
#endif
        {
            get => _spinEasingFunction;
            set
            {
                if (Equals(_spinEasingFunction, value))
                {
                    return;
                }

                _spinEasingFunction = value;
                WriteFieldChangedFlag(ChangedFieldFlags.SpinEasingFunction, true);
            }
        }

        private double _spinDuration = 1d;

        public double SpinDuration
        {
            get => _spinDuration;
            set
            {
                if (Equals(_spinDuration, value))
                {
                    return;
                }

                _spinDuration = value;
                WriteFieldChangedFlag(ChangedFieldFlags.SpinDuration, true);
            }
        }

        internal ChangedFieldFlags changedField; // Cache changed field bits

        internal bool IsFieldChanged(ChangedFieldFlags reqFlag)
        {
            return (changedField & reqFlag) != 0;
        }

        internal void WriteFieldChangedFlag(ChangedFieldFlags reqFlag, bool set)
        {
            if (set)
            {
                changedField |= reqFlag;
            }
            else
            {
                changedField &= (~reqFlag);
            }
        }

        [Flags]
        internal enum ChangedFieldFlags : ushort
        {
            Width = 0x0001,
            Height = 0x0002,
            Flip = 0x0004,
            RotationAngle = 0x0008,
            Spin = 0x0010,
            SpinAutoReverse = 0x0020,
            SpinEasingFunction = 0x0040,
            SpinDuration = 0x0080
        }
    }
}