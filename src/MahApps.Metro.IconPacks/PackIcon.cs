using System;
using System.Collections.Generic;
using System.Linq;
#if NETFX_CORE
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
#else
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
#endif
using ControlzEx;

namespace MahApps.Metro.IconPacks
{
    /// <summary>
    /// Enum PackIconFlipOrientation for the Flip property of the PackIcon
    /// </summary>
    public enum PackIconFlipOrientation
    {
        /// <summary>
        /// No flip
        /// </summary>
        Normal,

        /// <summary>
        /// Flip the icon horizontal
        /// </summary>
        Horizontal,

        /// <summary>
        /// Flip the icon vertical
        /// </summary>
        Vertical,

        /// <summary>
        /// Flip the icon vertical and horizontal
        /// </summary>
        Both
    }

    /// <summary>
    /// Class PackIcon which is the custom base class of MahApps.Metro.IconPacks.
    /// </summary>
    /// <typeparam name="TKind">The type of the enum kind.</typeparam>
    /// <seealso cref="ControlzEx.PackIconBase{TKind}" />
    public class PackIcon<TKind> : PackIconBase<TKind>
    {

#if NETFX_CORE
        private long _opacityRegisterToken;
        private long _visibilityRegisterToken;

        public PackIcon(Func<IDictionary<TKind, string>> dataIndexFactory) : base(dataIndexFactory)
        {
            this.Loaded += (sender, args) =>
            {
                this._opacityRegisterToken = this.RegisterPropertyChangedCallback(OpacityProperty, this.CoerceSpinProperty);
                this._visibilityRegisterToken = this.RegisterPropertyChangedCallback(VisibilityProperty, this.CoerceSpinProperty);
            };
            this.Unloaded += (sender, args) =>
            {
                this.UnregisterPropertyChangedCallback(OpacityProperty, this._opacityRegisterToken);
                this.UnregisterPropertyChangedCallback(VisibilityProperty, this._visibilityRegisterToken);
            };
        }

        private void CoerceSpinProperty(DependencyObject sender, DependencyProperty dp)
        {
            var packIcon = sender as PackIcon<TKind>;
            if (packIcon != null && (dp == OpacityProperty || dp == VisibilityProperty))
            {
                this.Spin = packIcon.Visibility == Visibility.Visible && packIcon.SpinDuration > 0 && packIcon.Opacity > 0;
            }
        }
#else
        static PackIcon()
        {
            OpacityProperty.OverrideMetadata(typeof(PackIcon<TKind>), new UIPropertyMetadata(1d, (d, e) => { d.CoerceValue(SpinProperty); }));
            VisibilityProperty.OverrideMetadata(typeof(PackIcon<TKind>), new UIPropertyMetadata(Visibility.Visible, (d, e) => { d.CoerceValue(SpinProperty); }));
        }

        public PackIcon(Func<IDictionary<TKind, string>> dataIndexFactory) : base(dataIndexFactory)
        {
        }
#endif

#if NETFX_CORE
        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.CoerceSpinProperty(this, SpinProperty);
            if (this.Spin)
            {
                this.StopSpinAnimation();
                this.BeginSpinAnimation();
            }
        }
#else
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.CoerceValue(SpinProperty);
            if (this.Spin)
            {
                this.StopSpinAnimation();
                this.BeginSpinAnimation();
            }
        }
#endif

        /// <summary>
        /// Identifies the Flip dependency property.
        /// </summary>
        public static readonly DependencyProperty FlipProperty
            = DependencyProperty.Register(
                "Flip",
                typeof(PackIconFlipOrientation),
                typeof(PackIcon<TKind>),
                new PropertyMetadata(PackIconFlipOrientation.Normal));

        /// <summary>
        /// Gets or sets the flip orientation.
        /// </summary>
        public PackIconFlipOrientation Flip
        {
            get { return (PackIconFlipOrientation)this.GetValue(FlipProperty); }
            set { this.SetValue(FlipProperty, value); }
        }

        /// <summary>
        /// Identifies the Rotation dependency property.
        /// </summary>
        public static readonly DependencyProperty RotationProperty
            = DependencyProperty.Register(
                "Rotation",
                typeof(double),
                typeof(PackIcon<TKind>),
#if NETFX_CORE
                new PropertyMetadata(0d));
#else
                new PropertyMetadata(0d, null, RotationPropertyCoerceValueCallback));

        private static object RotationPropertyCoerceValueCallback(DependencyObject dependencyObject, object value)
        {
            var val = (double)value;
            return val < 0 ? 0d : (val > 360 ? 360d : value);
        }
#endif

        /// <summary>
        /// Gets or sets the rotation (angle).
        /// </summary>
        /// <value>The rotation.</value>
        public double Rotation
        {
            get { return (double)this.GetValue(RotationProperty); }
            set { this.SetValue(RotationProperty, value); }
        }

        /// <summary>
        /// Identifies the Spin dependency property.
        /// </summary>
        public static readonly DependencyProperty SpinProperty
            = DependencyProperty.Register(
                "Spin",
                typeof(bool),
                typeof(PackIcon<TKind>),
#if NETFX_CORE
                new PropertyMetadata(default(bool), SpinPropertyChangedCallback));
#else
                new PropertyMetadata(default(bool), SpinPropertyChangedCallback, SpinPropertyCoerceValueCallback));

        private static object SpinPropertyCoerceValueCallback(DependencyObject dependencyObject, object value)
        {
            var packIcon = dependencyObject as PackIcon<TKind>;
            if (packIcon != null && (!packIcon.IsVisible || packIcon.Opacity <= 0 || packIcon.SpinDuration <= 0.0))
            {
                return false;
            }
            return value;
        }
#endif

        private static void SpinPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var packIcon = dependencyObject as PackIcon<TKind>;
            if (packIcon != null && e.OldValue != e.NewValue && e.NewValue is bool)
            {
                var spin = (bool)e.NewValue;
                if (spin)
                {
                    packIcon.BeginSpinAnimation();
                }
                else
                {
                    packIcon.StopSpinAnimation();
                }
            }
        }

        private static readonly string SpinnerStoryBoardName = $"{typeof(PackIcon<TKind>).Name}SpinnerStoryBoard";

        private FrameworkElement _innerGrid;
        private FrameworkElement InnerGrid => this._innerGrid ?? (this._innerGrid = this.GetTemplateChild("PART_InnerGrid") as FrameworkElement);

        private void BeginSpinAnimation()
        {
            var element = this.InnerGrid;
            if (null == element)
            {
                return;
            }
            var transformGroup = element.RenderTransform as TransformGroup ?? new TransformGroup();
            var rotateTransform = transformGroup.Children.OfType<RotateTransform>().LastOrDefault();

            if (rotateTransform != null)
            {
                rotateTransform.Angle = 0;
            }
            else
            {
                transformGroup.Children.Add(new RotateTransform());
                element.RenderTransform = transformGroup;
            }

            var storyboard = new Storyboard();
            var animation = new DoubleAnimation
            {
                From = 0,
                To = 360,
                AutoReverse = this.SpinAutoReverse,
                EasingFunction = this.SpinEasingFunction,
                RepeatBehavior = RepeatBehavior.Forever,
                Duration = new Duration(TimeSpan.FromSeconds(this.SpinDuration))
            };
            storyboard.Children.Add(animation);

            Storyboard.SetTarget(animation, element);
#if NETFX_CORE
            Storyboard.SetTargetProperty(animation, "(RenderTransform).(TransformGroup.Children)[2].(Angle)");
#else
            Storyboard.SetTargetProperty(animation, new PropertyPath("(0).(1)[2].(2)", RenderTransformProperty, TransformGroup.ChildrenProperty, RotateTransform.AngleProperty));
#endif

            element.Resources.Add(SpinnerStoryBoardName, storyboard);
            storyboard.Begin();
        }

        private void StopSpinAnimation()
        {
            var element = this.InnerGrid;
            if (null == element)
            {
                return;
            }
            var storyboard = element.Resources[SpinnerStoryBoardName] as Storyboard;
            if (storyboard != null)
            {
                storyboard.Stop();
                element.Resources.Remove(SpinnerStoryBoardName);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the inner icon is spinning.
        /// </summary>
        /// <value><c>true</c> if spin; otherwise, <c>false</c>.</value>
        public bool Spin
        {
            get { return (bool)this.GetValue(SpinProperty); }
            set { this.SetValue(SpinProperty, value); }
        }

        /// <summary>
        /// Identifies the SpinDuration dependency property.
        /// </summary>
        public static readonly DependencyProperty SpinDurationProperty
            = DependencyProperty.Register("SpinDuration",
                typeof(double),
                typeof(PackIcon<TKind>),
#if !NETFX_CORE
                new PropertyMetadata(1d, SpinDurationPropertyChangedCallback, SpinDurationCoerceValueCallback));
#else
                new PropertyMetadata(1d, SpinDurationPropertyChangedCallback));
#endif

        private static void SpinDurationPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var packIcon = dependencyObject as PackIcon<TKind>;
            if (packIcon != null && e.OldValue != e.NewValue && packIcon.Spin && e.NewValue is double)
            {
                packIcon.StopSpinAnimation();
                packIcon.BeginSpinAnimation();
            }
        }

#if !NETFX_CORE
        private static object SpinDurationCoerceValueCallback(DependencyObject dependencyObject, object value)
        {
            var val = (double)value;
            return val < 0 ? 0d : value;
        }
#endif

        /// <summary>
        /// Gets or sets the duration of the spinning animation (in seconds). This will also restart the spin animation.
        /// </summary>
        /// <value>The duration of the spin in seconds.</value>
        public double SpinDuration
        {
            get { return (double)this.GetValue(SpinDurationProperty); }
            set { this.SetValue(SpinDurationProperty, value); }
        }

        /// <summary>
        /// Identifies the SpinEasingFunction dependency property.
        /// </summary>
        public static readonly DependencyProperty SpinEasingFunctionProperty
            = DependencyProperty.Register("SpinEasingFunction",
#if NETFX_CORE
                typeof(EasingFunctionBase),
#else
                typeof(IEasingFunction),
#endif
                typeof(PackIcon<TKind>),
                new PropertyMetadata(null, SpinEasingFunctionPropertyChangedCallback));

        private static void SpinEasingFunctionPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var packIcon = dependencyObject as PackIcon<TKind>;
            if (packIcon != null && e.OldValue != e.NewValue && packIcon.Spin)
            {
                packIcon.StopSpinAnimation();
                packIcon.BeginSpinAnimation();
            }
        }

        /// <summary>
        /// Gets or sets the EasingFunction of the spinning animation. This will also restart the spin animation.
        /// </summary>
        /// <value>The spin easing function.</value>
#if NETFX_CORE
        public EasingFunctionBase SpinEasingFunction
        {
            get { return (EasingFunctionBase)this.GetValue(SpinEasingFunctionProperty); }
            set { this.SetValue(SpinEasingFunctionProperty, value); }
        }
#else
        public IEasingFunction SpinEasingFunction
        {
            get { return (IEasingFunction)this.GetValue(SpinEasingFunctionProperty); }
            set { this.SetValue(SpinEasingFunctionProperty, value); }
        }
#endif

        /// <summary>
        /// Identifies the SpinAutoReverse dependency property.
        /// </summary>
        public static readonly DependencyProperty SpinAutoReverseProperty
            = DependencyProperty.Register("SpinAutoReverse",
                typeof(bool),
                typeof(PackIcon<TKind>),
                new PropertyMetadata(default(bool), SpinAutoReversePropertyChangedCallback));

        private static void SpinAutoReversePropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var packIcon = dependencyObject as PackIcon<TKind>;
            if (packIcon != null && e.OldValue != e.NewValue && packIcon.Spin && e.NewValue is bool)
            {
                packIcon.StopSpinAnimation();
                packIcon.BeginSpinAnimation();
            }
        }

        /// <summary>
        /// Gets or sets the AutoReverse of the spinning animation. This will also restart the spin animation.
        /// </summary>
        /// <value><c>true</c> if [spin automatic reverse]; otherwise, <c>false</c>.</value>
        public bool SpinAutoReverse
        {
            get { return (bool)this.GetValue(SpinAutoReverseProperty); }
            set { this.SetValue(SpinAutoReverseProperty, value); }
        }
    }
}