// ReSharper disable PossibleNullReferenceException
// ReSharper disable IdentifierTypo

using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media.Animation;
using GraphShape;
using JetBrains.Annotations;
using Rect = System.Windows.Rect;
using Thickness = System.Windows.Thickness;
using Vector = System.Windows.Vector;

namespace MaoTouGu.Studio.Controls
{

    /// <summary>
    /// Zoom control.
    /// </summary>
    [TemplatePart(Name = PartPresenter, Type = typeof(ZoomContentPresenter))]
    public sealed class ZoomControl : ContentControl
    {

        private const string PartPresenter = "PART_Presenter";

        private Point                _mouseDownPos;
        private ZoomContentPresenter _presenter;
        private ScaleTransform       _scaleTransform;
        private Vector               _startTranslate;
        private TransformGroup       _transformGroup;
        private TranslateTransform   _translateTransform;
        private int                  _zoomAnimCount;
        private bool                 _isZooming;

        /// <summary/>
        static ZoomControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                                                     typeof(ZoomControl),
                                                     new FrameworkPropertyMetadata(typeof(ZoomControl)));
        }

        /// <summary/>
        public ZoomControl()
        {
            PreviewMouseWheel += OnZoomControlMouseWheel;
            PreviewMouseDown  += OnZoomControlPreviewMouseDown;
            MouseDown         += OnZoomControlMouseDown;
            MouseUp           += OnZoomControlMouseUp;
        }

        public Brush ZoomBoxBackground
        {
            get => (Brush)GetValue(ZoomBoxBackgroundProperty);
            set => SetValue(ZoomBoxBackgroundProperty, value);
        }


        public static readonly DependencyProperty ZoomBoxBackgroundProperty = DependencyProperty.Register(
                                                                                                          nameof(ZoomBoxBackground), typeof(Brush), typeof(ZoomControl), new UIPropertyMetadata(null));

        public Brush ZoomBoxBorderBrush
        {
            get => (Brush)GetValue(ZoomBoxBorderBrushProperty);
            set => SetValue(ZoomBoxBorderBrushProperty, value);
        }


        public static readonly DependencyProperty ZoomBoxBorderBrushProperty = DependencyProperty.Register(
                                                                                                           nameof(ZoomBoxBorderBrush), typeof(Brush), typeof(ZoomControl), new UIPropertyMetadata(null));

        public Thickness ZoomBoxBorderThickness
        {
            get => (Thickness)GetValue(ZoomBoxBorderThicknessProperty);
            set => SetValue(ZoomBoxBorderThicknessProperty, value);
        }


        public static readonly DependencyProperty ZoomBoxBorderThicknessProperty = DependencyProperty.Register(
                                                                                                               nameof(ZoomBoxBorderThickness), typeof(Thickness), typeof(ZoomControl), new UIPropertyMetadata(null));

        public double ZoomBoxOpacity
        {
            get => (double)GetValue(ZoomBoxOpacityProperty);
            set => SetValue(ZoomBoxOpacityProperty, value);
        }


        public static readonly DependencyProperty ZoomBoxOpacityProperty = DependencyProperty.Register(
                                                                                                       nameof(ZoomBoxOpacity), typeof(double), typeof(ZoomControl), new UIPropertyMetadata(0.5));

        public Rect ZoomBox
        {
            get => (Rect)GetValue(ZoomBoxProperty);
            set => SetValue(ZoomBoxProperty, value);
        }


        public static readonly DependencyProperty ZoomBoxProperty = DependencyProperty.Register(
                                                                                                nameof(ZoomBox), typeof(Rect), typeof(ZoomControl), new UIPropertyMetadata(new Rect()));

        public Point OrigoPosition => new Point(ActualWidth / 2.0, ActualHeight / 2.0);

        public double TranslateX
        {
            get => (double)GetValue(TranslateXProperty);
            set
            {
                BeginAnimation(TranslateXProperty, null);
                SetValue(TranslateXProperty, value);
            }
        }


        public static readonly DependencyProperty TranslateXProperty = DependencyProperty.Register(
                                                                                                   nameof(TranslateX), typeof(double), typeof(ZoomControl), new UIPropertyMetadata(0.0, OnTranslateXPropertyChanged, OnTranslateXCoerce));

        public double TranslateY
        {
            get => (double)GetValue(TranslateYProperty);
            set
            {
                BeginAnimation(TranslateYProperty, null);
                SetValue(TranslateYProperty, value);
            }
        }


        public static readonly DependencyProperty TranslateYProperty = DependencyProperty.Register(
                                                                                                   nameof(TranslateY), typeof(double), typeof(ZoomControl), new UIPropertyMetadata(0.0, OnTranslateYPropertyChanged, OnTranslateYCoerce));

        public TimeSpan AnimationLength
        {
            get => (TimeSpan)GetValue(AnimationLengthProperty);
            set => SetValue(AnimationLengthProperty, value);
        }


        public static readonly DependencyProperty AnimationLengthProperty = DependencyProperty.Register(
                                                                                                        nameof(AnimationLength), typeof(TimeSpan), typeof(ZoomControl), new UIPropertyMetadata(TimeSpan.FromMilliseconds(500.0)));

        public double MinZoom
        {
            get => (double)GetValue(MinZoomProperty);
            set => SetValue(MinZoomProperty, value);
        }


        public static readonly DependencyProperty MinZoomProperty = DependencyProperty.Register(
                                                                                                nameof(MinZoom), typeof(double), typeof(ZoomControl), new UIPropertyMetadata(0.01));

        public double MaxZoom
        {
            get => (double)GetValue(MaxZoomProperty);
            set => SetValue(MaxZoomProperty, value);
        }


        public static readonly DependencyProperty MaxZoomProperty = DependencyProperty.Register(
                                                                                                nameof(MaxZoom), typeof(double), typeof(ZoomControl), new UIPropertyMetadata(100.0));

        public double MaxZoomDelta
        {
            get => (double)GetValue(MaxZoomDeltaProperty);
            set => SetValue(MaxZoomDeltaProperty, value);
        }


        public static readonly DependencyProperty MaxZoomDeltaProperty = DependencyProperty.Register(
                                                                                                     nameof(MaxZoomDelta), typeof(double), typeof(ZoomControl), new UIPropertyMetadata(5.0));

        public double ZoomDeltaMultiplier
        {
            get => (double)GetValue(ZoomDeltaMultiplierProperty);
            set => SetValue(ZoomDeltaMultiplierProperty, value);
        }


        public static readonly DependencyProperty ZoomDeltaMultiplierProperty = DependencyProperty.Register(
                                                                                                            nameof(ZoomDeltaMultiplier), typeof(double), typeof(ZoomControl), new UIPropertyMetadata(100.0));

        public double Zoom
        {
            get => (double)GetValue(ZoomProperty);
            set
            {
                if (Math.Abs((double)GetValue(ZoomProperty) - value) < double.Epsilon)
                    return;

                BeginAnimation(ZoomProperty, null);
                SetValue(ZoomProperty, value);
            }
        }


        public static readonly DependencyProperty ZoomProperty = DependencyProperty.Register(
                                                                                             nameof(Zoom), typeof(double), typeof(ZoomControl), new UIPropertyMetadata(1.0, OnZoomPropertyChanged));

        private ZoomContentPresenter Presenter
        {
            get => _presenter;
            set
            {
                _presenter = value;
                if (_presenter is null)
                    return;

                _transformGroup     = new TransformGroup();
                _scaleTransform     = new ScaleTransform();
                _translateTransform = new TranslateTransform();
                _transformGroup.Children.Add(_scaleTransform);
                _transformGroup.Children.Add(_translateTransform);
                _presenter.RenderTransform       = _transformGroup;
                _presenter.RenderTransformOrigin = new Point(0.5, 0.5);
            }
        }

        public ZoomViewModifierMode ModifierMode
        {
            get => (ZoomViewModifierMode)GetValue(ModifierModeProperty);
            set => SetValue(ModifierModeProperty, value);
        }


        public static readonly DependencyProperty ModifierModeProperty = DependencyProperty.Register(
                                                                                                     nameof(ModifierMode), typeof(ZoomViewModifierMode), typeof(ZoomControl), new UIPropertyMetadata(ZoomViewModifierMode.None));

        public ZoomControlModes Mode
        {
            get => (ZoomControlModes)GetValue(ModeProperty);
            set => SetValue(ModeProperty, value);
        }


        public static readonly DependencyProperty ModeProperty = DependencyProperty.Register(
                                                                                             nameof(Mode), typeof(ZoomControlModes), typeof(ZoomControl), new UIPropertyMetadata(ZoomControlModes.Custom, OnModePropertyChanged));

        private static void OnModePropertyChanged(
            DependencyObject d,
            DependencyPropertyChangedEventArgs args)
        {
            var zoomControl = (ZoomControl)d;
            switch ((ZoomControlModes)args.NewValue)
            {
                case ZoomControlModes.Fill:
                    zoomControl.DoZoomToFill();
                    break;

                case ZoomControlModes.Original:
                    zoomControl.DoZoomToOriginal();
                    break;

                case ZoomControlModes.Custom:
                    break;

                default:
                    throw new NotSupportedException();
            }
        }

        [Pure]
        private static object OnTranslateXCoerce(DependencyObject d, object basevalue)
        {
            var zoomControl = (ZoomControl)d;
            return zoomControl.GetCoercedTranslateX((double)basevalue);
        }

        [Pure]
        private double GetCoercedTranslateX(double baseValue)
        {
            return _presenter is null ? 0.0 : baseValue;
        }

        [Pure]
        private static object OnTranslateYCoerce(DependencyObject d, object basevalue)
        {
            var zoomControl = (ZoomControl)d;
            return zoomControl.GetCoercedTranslateY((double)basevalue);
        }

        [Pure]
        private double GetCoercedTranslateY(double baseValue)
        {
            return _presenter is null ? 0.0 : baseValue;
        }

        private void OnZoomControlMouseUp(object sender, MouseButtonEventArgs args)
        {
            switch (ModifierMode)
            {
                case ZoomViewModifierMode.None:
                    break;

                case ZoomViewModifierMode.Pan:
                case ZoomViewModifierMode.ZoomIn:
                case ZoomViewModifierMode.ZoomOut:
                    ModifierMode     =  ZoomViewModifierMode.None;
                    PreviewMouseMove -= OnZoomControlPreviewMouseMove;
                    ReleaseMouseCapture();
                    break;

                case ZoomViewModifierMode.ZoomBox:
                    ZoomTo(ZoomBox);
                    goto case ZoomViewModifierMode.Pan;

                default:
                    throw new NotSupportedException();
            }
        }

        public void ZoomTo(Rect rect)
        {
            DoZoom(
                   Math.Min(ActualWidth / rect.Width, ActualHeight / rect.Height),
                   OrigoPosition,
                   new Point(rect.X + rect.Width / 2.0, rect.Y + rect.Height / 2.0),
                   OrigoPosition);
            ZoomBox = new Rect();
        }

        private void OnZoomControlPreviewMouseMove(object sender, MouseEventArgs args)
        {
            switch (ModifierMode)
            {
                case ZoomViewModifierMode.None:
                case ZoomViewModifierMode.ZoomIn:
                case ZoomViewModifierMode.ZoomOut:
                    break;

                case ZoomViewModifierMode.Pan:
                    var vector = _startTranslate + (args.GetPosition(this) - _mouseDownPos);
                    TranslateX = vector.X;
                    TranslateY = vector.Y;
                    break;

                case ZoomViewModifierMode.ZoomBox:
                    var position = args.GetPosition(this);
                    ZoomBox = new Rect(
                                       Math.Min(_mouseDownPos.X, position.X),
                                       Math.Min(_mouseDownPos.Y, position.Y),
                                       Math.Abs(_mouseDownPos.X - position.X),
                                       Math.Abs(_mouseDownPos.Y - position.Y));
                    break;

                default:
                    throw new NotSupportedException();
            }
        }

        private void OnZoomControlMouseDown(object sender, MouseButtonEventArgs args) => OnMouseDown(args, false);

        private void OnZoomControlPreviewMouseDown(object sender, MouseButtonEventArgs args) => OnMouseDown(args, true);

        private void OnMouseDown(MouseButtonEventArgs args, bool isPreview)
        {
            if (ModifierMode != ZoomViewModifierMode.None)
                return;

            switch (Keyboard.Modifiers)
            {
                case ModifierKeys.None:
                    if (!isPreview)
                    {
                        ModifierMode = ZoomViewModifierMode.Pan;
                        goto case ModifierKeys.Control;
                    }
                    else
                        goto case ModifierKeys.Control;

                case ModifierKeys.Alt:
                    ModifierMode = ZoomViewModifierMode.ZoomBox;
                    goto case ModifierKeys.Control;

                case ModifierKeys.Control:
                case ModifierKeys.Windows:
                    if (ModifierMode == ZoomViewModifierMode.None)
                        break;

                    _mouseDownPos   = args.GetPosition(this);
                    _startTranslate = new Vector(TranslateX, TranslateY);
                    Mouse.Capture(this);
                    PreviewMouseMove += OnZoomControlPreviewMouseMove;
                    break;

                case ModifierKeys.Shift:
                    ModifierMode = ZoomViewModifierMode.Pan;
                    goto case ModifierKeys.Control;
            }
        }

        private static void OnTranslateXPropertyChanged(
            DependencyObject d,
            DependencyPropertyChangedEventArgs args)
        {
            var zoomControl = (ZoomControl)d;
            if (zoomControl._translateTransform is null)
                return;

            zoomControl._translateTransform.X = (double)args.NewValue;
            if (zoomControl._isZooming)
                return;

            zoomControl.Mode = ZoomControlModes.Custom;
        }

        private static void OnTranslateYPropertyChanged(
            DependencyObject d,
            DependencyPropertyChangedEventArgs args)
        {
            var zoomControl = (ZoomControl)d;
            if (zoomControl._translateTransform is null)
                return;

            zoomControl._translateTransform.Y = (double)args.NewValue;
            if (zoomControl._isZooming)
                return;

            zoomControl.Mode = ZoomControlModes.Custom;
        }

        private static void OnZoomPropertyChanged(
            DependencyObject d,
            DependencyPropertyChangedEventArgs args)
        {
            var zoomControl = (ZoomControl)d;
            if (zoomControl._scaleTransform is null)
                return;

            var newValue = (double)args.NewValue;
            zoomControl._scaleTransform.ScaleX = newValue;
            zoomControl._scaleTransform.ScaleY = newValue;
            if (zoomControl._isZooming)
                return;

            var num = newValue / (double)args.OldValue;
            zoomControl.TranslateX *= num;
            zoomControl.TranslateY *= num;
            zoomControl.Mode       =  ZoomControlModes.Custom;
        }

        private void OnZoomControlMouseWheel(object sender, MouseWheelEventArgs args)
        {
            if ((Keyboard.Modifiers & ModifierKeys.Control) <= ModifierKeys.None || ModifierMode != ZoomViewModifierMode.None)
                return;

            args.Handled = true;
            var origoPosition = new Point(ActualWidth / 2.0, ActualHeight / 2.0);
            var position      = args.GetPosition(this);
            DoZoom(
                   Math.Max(1.0 / MaxZoomDelta, Math.Min(MaxZoomDelta, args.Delta / 10000.0 * ZoomDeltaMultiplier + 1.0)),
                   origoPosition,
                   position,
                   position);
        }

        private void DoZoom(
            double deltaZoom,
            Point origoPosition,
            Point startHandlePosition,
            Point targetHandlePosition)
        {
            var zoom              = Zoom;
            var num               = Math.Max(MinZoom, Math.Min(MaxZoom, zoom * deltaZoom));
            var vector1           = new Vector(TranslateX, TranslateY);
            var vector2           = startHandlePosition  - origoPosition;
            var vector3           = targetHandlePosition - origoPosition - ((vector2 - vector1) / zoom * num + vector1);
            var coercedTranslateX = GetCoercedTranslateX(TranslateX + vector3.X);
            var coercedTranslateY = GetCoercedTranslateY(TranslateY + vector3.Y);
            DoZoomAnimation(num, coercedTranslateX, coercedTranslateY);
            Mode = ZoomControlModes.Custom;
        }

        private void DoZoomAnimation(double targetZoom, double transformX, double transformY)
        {
            _isZooming = true;

            var duration = new Duration(AnimationLength);
            StartAnimation(TranslateXProperty, transformX, duration);
            StartAnimation(TranslateYProperty, transformY, duration);
            StartAnimation(ZoomProperty, targetZoom, duration);
        }

        private void StartAnimation(DependencyProperty dp, double toValue, Duration duration)
        {
            if (double.IsNaN(toValue) || double.IsInfinity(toValue))
            {
                if (dp != ZoomProperty)
                    return;

                _isZooming = false;
            }
            else
            {
                var doubleAnimation = new DoubleAnimation(toValue, duration);
                if (dp == ZoomProperty)
                {
                    ++_zoomAnimCount;
                    doubleAnimation.Completed += (_, _) =>
                                                 {
                                                     --_zoomAnimCount;
                                                     if (_zoomAnimCount > 0)
                                                         return;

                                                     var zoom = Zoom;
                                                     BeginAnimation(ZoomProperty, null);
                                                     SetValue(ZoomProperty, zoom);
                                                     _isZooming = false;
                                                 };
                }

                BeginAnimation(dp, doubleAnimation, HandoffBehavior.Compose);
            }
        }

        public void ZoomToOriginal() => Mode = ZoomControlModes.Original;

        private void DoZoomToOriginal()
        {
            if (_presenter is null)
                return;

            var initialTranslate = GetInitialTranslate();
            DoZoomAnimation(1.0, initialTranslate.X, initialTranslate.Y);
        }

        [Pure]
        private Vector GetInitialTranslate()
        {
            return _presenter is null
                ? default
                : new Vector(
                             -(_presenter.ContentSize.Width  - _presenter.DesiredSize.Width)  / 2.0,
                             -(_presenter.ContentSize.Height - _presenter.DesiredSize.Height) / 2.0);
        }

        public void ZoomToFill() => Mode = ZoomControlModes.Fill;

        private void DoZoomToFill()
        {
            if (_presenter is null)
                return;

            var targetZoom = Math.Min(
                                      ActualWidth  / _presenter.ContentSize.Width,
                                      ActualHeight / _presenter.ContentSize.Height);

            var initialTranslate = GetInitialTranslate();
            DoZoomAnimation(targetZoom, initialTranslate.X * targetZoom, initialTranslate.Y * targetZoom);
        }

        /// <inheritdoc />
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            Presenter = GetTemplateChild(PartPresenter) as ZoomContentPresenter;
            if (Presenter != null)
            {
                Presenter.SizeChanged += (_, _) =>
                                         {
                                             if (Mode != ZoomControlModes.Fill)
                                                 return;
                                             DoZoomToFill();
                                         };

                Presenter.ContentSizeChanged += (_, _) =>
                                                {
                                                    if (Mode != ZoomControlModes.Fill)
                                                        return;
                                                    DoZoomToFill();
                                                };
            }

            ZoomToFill();
        }
    }
}