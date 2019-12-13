using System;
using System.Collections.Generic;
using System.Linq;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using MahApps.Metro.IconPacks.Converter;

namespace MahApps.Metro.IconPacks
{
    /// <summary>
    /// Class PathIconControlBase which is the base class for any PathIcon control.
    /// </summary>
    public abstract class PathIconControlBase : PathIconBase
    {
        private long _opacityRegisterToken;
        private long _visibilityRegisterToken;

        public PathIconControlBase()
        {
            this.SetValue(RenderTransformOriginProperty, new Point(0.5, 0.5));
            this.SetValue(FlowDirectionProperty, FlowDirection.LeftToRight);
            this.SetValue(HorizontalAlignmentProperty, HorizontalAlignment.Left);
            this.SetValue(VerticalAlignmentProperty, VerticalAlignment.Top);
            this.CreateRenderTransformGroup();

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

        private void CreateRenderTransformGroup()
        {
            var transformGroup = this.RenderTransform as TransformGroup ?? new TransformGroup();
            var scaleTransform = new ScaleTransform();
            BindingOperations.SetBinding(
                scaleTransform,
                ScaleTransform.ScaleXProperty,
                new Binding() {Path = new PropertyPath(nameof(Flip)), Source = this, Mode = BindingMode.OneWay, Converter = FlipToScaleXValueConverter.Instance});
            BindingOperations.SetBinding(
                scaleTransform,
                ScaleTransform.ScaleYProperty,
                new Binding() {Path = new PropertyPath(nameof(Flip)), Source = this, Mode = BindingMode.OneWay, Converter = FlipToScaleYValueConverter.Instance});
            transformGroup.Children.Add(scaleTransform); // flip
            var rotateTransform = new RotateTransform();
            BindingOperations.SetBinding(
                rotateTransform,
                RotateTransform.AngleProperty,
                new Binding() {Path = new PropertyPath(nameof(RotationAngle)), Source = this, Mode = BindingMode.OneWay});
            transformGroup.Children.Add(rotateTransform); // rotate
            transformGroup.Children.Add(new RotateTransform()); // spin
            this.RenderTransform = transformGroup;
        }

        private void CoerceSpinProperty(DependencyObject sender, DependencyProperty dp)
        {
            var pathIcon = sender as PathIconControlBase;
            if (pathIcon != null && (dp == OpacityProperty || dp == VisibilityProperty))
            {
                var spin = this.Spin && pathIcon.Visibility == Visibility.Visible && pathIcon.SpinDuration > 0 && pathIcon.Opacity > 0;
                pathIcon.ToggleSpinAnimation(spin);
            }
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            UpdateData();

            this.CoerceSpinProperty(this, SpinProperty);

            if (this.Spin)
            {
                this.StopSpinAnimation();
                this.BeginSpinAnimation();
            }
        }

        /// <summary>
        /// Identifies the Flip dependency property.
        /// </summary>
        public static readonly DependencyProperty FlipProperty
            = DependencyProperty.Register(
                nameof(Flip),
                typeof(PackIconFlipOrientation),
                typeof(PathIconControlBase),
                new PropertyMetadata(PackIconFlipOrientation.Normal));

        /// <summary>
        /// Gets or sets the flip orientation.
        /// </summary>
        public PackIconFlipOrientation Flip
        {
            get { return (PackIconFlipOrientation) this.GetValue(FlipProperty); }
            set { this.SetValue(FlipProperty, value); }
        }

        /// <summary>
        /// Identifies the RotationAngle dependency property.
        /// </summary>
        public static readonly DependencyProperty RotationAngleProperty
            = DependencyProperty.Register(
                nameof(RotationAngle),
                typeof(double),
                typeof(PathIconControlBase),
                new PropertyMetadata(0d));

        /// <summary>
        /// Gets or sets the rotation (angle).
        /// </summary>
        /// <value>The rotation.</value>
        public double RotationAngle
        {
            get { return (double) this.GetValue(RotationAngleProperty); }
            set { this.SetValue(RotationAngleProperty, value); }
        }

        /// <summary>
        /// Identifies the Spin dependency property.
        /// </summary>
        public static readonly DependencyProperty SpinProperty
            = DependencyProperty.Register(
                nameof(Spin),
                typeof(bool),
                typeof(PathIconControlBase),
                new PropertyMetadata(default(bool), SpinPropertyChangedCallback));

        private static void SpinPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var pathIcon = dependencyObject as PathIconControlBase;
            if (pathIcon != null && e.OldValue != e.NewValue && e.NewValue is bool)
            {
                pathIcon.ToggleSpinAnimation((bool)e.NewValue);
            }
        }

        private void ToggleSpinAnimation(bool spin)
        {
            if (spin)
            {
                this.BeginSpinAnimation();
            }
            else
            {
                this.StopSpinAnimation();
            }
        }

        private Storyboard spinningStoryboard;

        private void BeginSpinAnimation()
        {
            var element = this;
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

            Storyboard.SetTargetProperty(animation, $"(RenderTransform).(TransformGroup.Children)[{transformGroup.Children.Count - 1}].(Angle)");

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
            get { return (bool) this.GetValue(SpinProperty); }
            set { this.SetValue(SpinProperty, value); }
        }

        /// <summary>
        /// Identifies the SpinDuration dependency property.
        /// </summary>
        public static readonly DependencyProperty SpinDurationProperty
            = DependencyProperty.Register(
                nameof(SpinDuration),
                typeof(double),
                typeof(PathIconControlBase),
                new PropertyMetadata(1d, SpinDurationPropertyChangedCallback));

        private static void SpinDurationPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var pathIcon = dependencyObject as PathIconControlBase;
            if (pathIcon != null && e.OldValue != e.NewValue && pathIcon.Spin && e.NewValue is double)
            {
                pathIcon.StopSpinAnimation();
                pathIcon.BeginSpinAnimation();
            }
        }

        /// <summary>
        /// Gets or sets the duration of the spinning animation (in seconds). This will also restart the spin animation.
        /// </summary>
        /// <value>The duration of the spin in seconds.</value>
        public double SpinDuration
        {
            get { return (double) this.GetValue(SpinDurationProperty); }
            set { this.SetValue(SpinDurationProperty, value); }
        }

        /// <summary>
        /// Identifies the SpinEasingFunction dependency property.
        /// </summary>
        public static readonly DependencyProperty SpinEasingFunctionProperty
            = DependencyProperty.Register(
                nameof(SpinEasingFunction),
                typeof(EasingFunctionBase),
                typeof(PathIconControlBase),
                new PropertyMetadata(null, SpinEasingFunctionPropertyChangedCallback));

        private static void SpinEasingFunctionPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var pathIcon = dependencyObject as PathIconControlBase;
            if (pathIcon != null && e.OldValue != e.NewValue && pathIcon.Spin)
            {
                pathIcon.StopSpinAnimation();
                pathIcon.BeginSpinAnimation();
            }
        }

        /// <summary>
        /// Gets or sets the EasingFunction of the spinning animation. This will also restart the spin animation.
        /// </summary>
        /// <value>The spin easing function.</value>
        public EasingFunctionBase SpinEasingFunction
        {
            get { return (EasingFunctionBase) this.GetValue(SpinEasingFunctionProperty); }
            set { this.SetValue(SpinEasingFunctionProperty, value); }
        }

        /// <summary>
        /// Identifies the SpinAutoReverse dependency property.
        /// </summary>
        public static readonly DependencyProperty SpinAutoReverseProperty
            = DependencyProperty.Register(
                nameof(SpinAutoReverse),
                typeof(bool),
                typeof(PathIconControlBase),
                new PropertyMetadata(default(bool), SpinAutoReversePropertyChangedCallback));

        private static void SpinAutoReversePropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var pathIcon = dependencyObject as PathIconControlBase;
            if (pathIcon != null && e.OldValue != e.NewValue && pathIcon.Spin && e.NewValue is bool)
            {
                pathIcon.StopSpinAnimation();
                pathIcon.BeginSpinAnimation();
            }
        }

        /// <summary>
        /// Gets or sets the AutoReverse of the spinning animation. This will also restart the spin animation.
        /// </summary>
        /// <value><c>true</c> if [spin automatic reverse]; otherwise, <c>false</c>.</value>
        public bool SpinAutoReverse
        {
            get { return (bool) this.GetValue(SpinAutoReverseProperty); }
            set { this.SetValue(SpinAutoReverseProperty, value); }
        }
    }
}
