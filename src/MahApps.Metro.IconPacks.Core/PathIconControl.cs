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
    /// Class PathIconControl which is the base class for any PathIcon control.
    /// </summary>
    /// <typeparam name="TKind">The type of the enum kind.</typeparam>
    /// <seealso cref="PathIconControl{TKind}" />
    public class PathIconControl<TKind> : PathIconBase<TKind>
    {
        private long _opacityRegisterToken;
        private long _visibilityRegisterToken;

        public PathIconControl(Func<IDictionary<TKind, string>> dataIndexFactory) : base(dataIndexFactory)
        {
            this.SetValue(RenderTransformOriginProperty, new Point(0.5, 0.5));
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
                new Binding() {Path = new PropertyPath(nameof(Rotation)), Source = this, Mode = BindingMode.OneWay});
            transformGroup.Children.Add(rotateTransform); // rotate
            transformGroup.Children.Add(new RotateTransform()); // spin
            this.RenderTransform = transformGroup;
        }

        private void CoerceSpinProperty(DependencyObject sender, DependencyProperty dp)
        {
            var pathIcon = sender as PathIconControl<TKind>;
            if (pathIcon != null && (dp == OpacityProperty || dp == VisibilityProperty))
            {
                this.Spin = pathIcon.Visibility == Visibility.Visible && pathIcon.SpinDuration > 0 && pathIcon.Opacity > 0;
            }
        }

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

        /// <summary>
        /// Identifies the Flip dependency property.
        /// </summary>
        public static readonly DependencyProperty FlipProperty
            = DependencyProperty.Register(
                nameof(Flip),
                typeof(PackIconFlipOrientation),
                typeof(PathIconControl<TKind>),
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
        /// Identifies the Rotation dependency property.
        /// </summary>
        public static readonly DependencyProperty RotationProperty
            = DependencyProperty.Register(
                nameof(Rotation),
                typeof(double),
                typeof(PathIconControl<TKind>),
                new PropertyMetadata(0d));

        /// <summary>
        /// Gets or sets the rotation (angle).
        /// </summary>
        /// <value>The rotation.</value>
        public double Rotation
        {
            get { return (double) this.GetValue(RotationProperty); }
            set { this.SetValue(RotationProperty, value); }
        }

        /// <summary>
        /// Identifies the Spin dependency property.
        /// </summary>
        public static readonly DependencyProperty SpinProperty
            = DependencyProperty.Register(
                nameof(Spin),
                typeof(bool),
                typeof(PathIconControl<TKind>),
                new PropertyMetadata(default(bool), SpinPropertyChangedCallback));

        private static void SpinPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var pathIcon = dependencyObject as PathIconControl<TKind>;
            if (pathIcon != null && e.OldValue != e.NewValue && e.NewValue is bool)
            {
                var spin = (bool) e.NewValue;
                if (spin)
                {
                    pathIcon.BeginSpinAnimation();
                }
                else
                {
                    pathIcon.StopSpinAnimation();
                }
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
                typeof(PathIconControl<TKind>),
                new PropertyMetadata(1d, SpinDurationPropertyChangedCallback));

        private static void SpinDurationPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var pathIcon = dependencyObject as PathIconControl<TKind>;
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
                typeof(PathIconControl<TKind>),
                new PropertyMetadata(null, SpinEasingFunctionPropertyChangedCallback));

        private static void SpinEasingFunctionPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var pathIcon = dependencyObject as PathIconControl<TKind>;
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
                typeof(PathIconControl<TKind>),
                new PropertyMetadata(default(bool), SpinAutoReversePropertyChangedCallback));

        private static void SpinAutoReversePropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var pathIcon = dependencyObject as PathIconControl<TKind>;
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
