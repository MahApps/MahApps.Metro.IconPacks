using System;
using System.Collections.Generic;
using System.Linq;
#if NETFX_CORE || WINDOWS_UWP
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
#else
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
#endif

namespace MahApps.Metro.IconPacks
{
    /// <summary>
    /// Class PackIconControl which is the base class for any PackIcon control.
    /// </summary>
    /// <typeparam name="TKind">The type of the enum kind.</typeparam>
    /// <seealso cref="PackIconBase{TKind}" />
    public abstract class PackIconControl<TKind> : PackIconBase<TKind>
    {

#if NETFX_CORE || WINDOWS_UWP
        private long _opacityRegisterToken;
        private long _visibilityRegisterToken;

        public PackIconControl(Func<IDictionary<TKind, string>> dataIndexFactory) : base(dataIndexFactory)
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
            var packIcon = sender as PackIconControl<TKind>;
            if (packIcon != null && (dp == OpacityProperty || dp == VisibilityProperty))
            {
                this.Spin = packIcon.Visibility == Visibility.Visible && packIcon.SpinDuration > 0 && packIcon.Opacity > 0;
            }
        }
#else
        static PackIconControl()
        {
            OpacityProperty.OverrideMetadata(typeof(PackIconControl<TKind>), new UIPropertyMetadata(1d, (d, e) => { d.CoerceValue(SpinProperty); }));
            VisibilityProperty.OverrideMetadata(typeof(PackIconControl<TKind>), new UIPropertyMetadata(Visibility.Visible, (d, e) => { d.CoerceValue(SpinProperty); }));
        }

        protected PackIconControl(Func<IDictionary<TKind, string>> dataIndexFactory) : base(dataIndexFactory)
        {
        }
#endif

#if NETFX_CORE || WINDOWS_UWP
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
                nameof(Flip),
                typeof(PackIconFlipOrientation),
                typeof(PackIconControl<TKind>),
                new PropertyMetadata(PackIconFlipOrientation.Normal));

        /// <summary>
        /// Gets or sets the flip orientation.
        /// </summary>
        public PackIconFlipOrientation Flip
        {
            get { return (PackIconFlipOrientation)this.GetValue(FlipProperty); }
            set { this.SetValue(FlipProperty, value); }
        }

#if !(UAP1809)
        /// <summary>
        /// Identifies the Rotation dependency property.
        /// </summary>
        public static readonly DependencyProperty RotationProperty
            = DependencyProperty.Register(
                nameof(Rotation),
                typeof(double),
                typeof(PackIconControl<TKind>),
#if NETFX_CORE || WINDOWS_UWP
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
#endif

        /// <summary>
        /// Identifies the Spin dependency property.
        /// </summary>
        public static readonly DependencyProperty SpinProperty
            = DependencyProperty.Register(
                nameof(Spin),
                typeof(bool),
                typeof(PackIconControl<TKind>),
#if NETFX_CORE || WINDOWS_UWP
                new PropertyMetadata(default(bool), SpinPropertyChangedCallback));
#else
                new PropertyMetadata(default(bool), SpinPropertyChangedCallback, SpinPropertyCoerceValueCallback));

        private static object SpinPropertyCoerceValueCallback(DependencyObject dependencyObject, object value)
        {
            var packIcon = dependencyObject as PackIconControl<TKind>;
            if (packIcon != null && (!packIcon.IsVisible || packIcon.Opacity <= 0 || packIcon.SpinDuration <= 0.0))
            {
                return false;
            }
            return value;
        }
#endif

        private static void SpinPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var packIcon = dependencyObject as PackIconControl<TKind>;
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

        private Storyboard spinningStoryboard;
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

            var animation = new DoubleAnimation
            {
                From = 0,
                To = 360,
                AutoReverse = this.SpinAutoReverse,
                EasingFunction = this.SpinEasingFunction,
                RepeatBehavior = RepeatBehavior.Forever,
                Duration = new Duration(TimeSpan.FromSeconds(this.SpinDuration))
            };

            var storyboard = new Storyboard();
            storyboard.Children.Add(animation);
            Storyboard.SetTarget(animation, element);

#if NETFX_CORE || WINDOWS_UWP
            Storyboard.SetTargetProperty(animation, $"(RenderTransform).(TransformGroup.Children)[{transformGroup.Children.Count - 1}].(Angle)");
#else
            Storyboard.SetTargetProperty(animation, new PropertyPath($"(0).(1)[{transformGroup.Children.Count - 1}].(2)", RenderTransformProperty, TransformGroup.ChildrenProperty, RotateTransform.AngleProperty));
#endif

            spinningStoryboard = storyboard;
            storyboard.Begin();
        }

        private void StopSpinAnimation()
        {
            var storyboard = spinningStoryboard;
            storyboard?.Stop();
            spinningStoryboard = null;
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
            = DependencyProperty.Register(
                nameof(SpinDuration),
                typeof(double),
                typeof(PackIconControl<TKind>),
#if !(NETFX_CORE || WINDOWS_UWP)
                new PropertyMetadata(1d, SpinDurationPropertyChangedCallback, SpinDurationCoerceValueCallback));
#else
                new PropertyMetadata(1d, SpinDurationPropertyChangedCallback));
#endif

        private static void SpinDurationPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var packIcon = dependencyObject as PackIconControl<TKind>;
            if (packIcon != null && e.OldValue != e.NewValue && packIcon.Spin && e.NewValue is double)
            {
                packIcon.StopSpinAnimation();
                packIcon.BeginSpinAnimation();
            }
        }

#if !(NETFX_CORE || WINDOWS_UWP)
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
            = DependencyProperty.Register(
                nameof(SpinEasingFunction),
#if NETFX_CORE || WINDOWS_UWP
                typeof(EasingFunctionBase),
#else
                typeof(IEasingFunction),
#endif
                typeof(PackIconControl<TKind>),
                new PropertyMetadata(null, SpinEasingFunctionPropertyChangedCallback));

        private static void SpinEasingFunctionPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var packIcon = dependencyObject as PackIconControl<TKind>;
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
#if NETFX_CORE || WINDOWS_UWP
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
            = DependencyProperty.Register(
                nameof(SpinAutoReverse),
                typeof(bool),
                typeof(PackIconControl<TKind>),
                new PropertyMetadata(default(bool), SpinAutoReversePropertyChangedCallback));

        private static void SpinAutoReversePropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var packIcon = dependencyObject as PackIconControl<TKind>;
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