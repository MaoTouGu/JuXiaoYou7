using System.Reactive.Linq;
using Avalonia;
using Avalonia.Animation;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Templates;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Styling;
using Avalonia.Threading;
// ReSharper disable MethodHasAsyncOverload

namespace KinonekoSoftware.UI.Controls
{
    [PseudoClasses(":left", ":right", ":top", "bottom")]
    public class Drawer : ContentControl
    {
        public static readonly StyledProperty<bool>          IsLeftOpenProperty;
        public static readonly StyledProperty<object>        LeftContentProperty;
        public static readonly StyledProperty<IDataTemplate> LeftContentTemplateProperty;

        public static readonly StyledProperty<bool>          IsRightOpenProperty;
        public static readonly StyledProperty<object>        RightContentProperty;
        public static readonly StyledProperty<IDataTemplate> RightContentTemplateProperty;

        public static readonly StyledProperty<bool>          IsTopOpenProperty;
        public static readonly StyledProperty<object>        TopContentProperty;
        public static readonly StyledProperty<IDataTemplate> TopContentTemplateProperty;

        public static readonly StyledProperty<bool>          IsBottomOpenProperty;
        public static readonly StyledProperty<object>        BottomContentProperty;
        public static readonly StyledProperty<IDataTemplate> BottomContentTemplateProperty;


        static Drawer()
        {
            IsLeftOpenProperty          = AvaloniaProperty.Register<Drawer, bool>(nameof(IsLeftOpen));
            LeftContentProperty         = AvaloniaProperty.Register<Drawer, object>(nameof(LeftContent));
            LeftContentTemplateProperty = AvaloniaProperty.Register<Drawer, IDataTemplate>(nameof(LeftContentTemplate));

            IsRightOpenProperty          = AvaloniaProperty.Register<Drawer, bool>(nameof(IsRightOpen));
            RightContentProperty         = AvaloniaProperty.Register<Drawer, object>(nameof(RightContent));
            RightContentTemplateProperty = AvaloniaProperty.Register<Drawer, IDataTemplate>(nameof(RightContentTemplate));

            IsTopOpenProperty          = AvaloniaProperty.Register<Drawer, bool>(nameof(IsTopOpen));
            TopContentProperty         = AvaloniaProperty.Register<Drawer, object>(nameof(TopContent));
            TopContentTemplateProperty = AvaloniaProperty.Register<Drawer, IDataTemplate>(nameof(TopContentTemplate));

            IsBottomOpenProperty          = AvaloniaProperty.Register<Drawer, bool>(nameof(IsBottomOpen));
            BottomContentProperty         = AvaloniaProperty.Register<Drawer, object>(nameof(BottomContent));
            BottomContentTemplateProperty = AvaloniaProperty.Register<Drawer, IDataTemplate>(nameof(BottomContentTemplate));

            //
            // Event
            DrawerClosedEvent = RoutedEvent.Register<Drawer, RoutedEventArgs>(nameof(DrawerClosed), RoutingStrategies.Bubble);

            IsLeftOpenProperty.Changed.Subscribe(OnLeftOpenStateChanged);
            IsRightOpenProperty.Changed.Subscribe(OnRightOpenStateChanged);
            IsTopOpenProperty.Changed.Subscribe(OnTopOpenStateChanged);
            IsBottomOpenProperty.Changed.Subscribe(OnBottomOpenStateChanged);
        }


        private Border _partMask;
        private Border _left;
        private Border _right;
        private Border _top;
        private Border _bottom;

        private readonly CancellationTokenSource[] _CTSBuffer  = new CancellationTokenSource[4];
        private readonly Task[]                    _TaskBuffer = new Task[4];

        private void CreateMaskAnimation(bool close)
        {
            if (_partMask is null)
            {
                return;
            }

            if (close)
            {
                //
                // 开始动画
                _partMask.Opacity   = 0d;
                _partMask.IsVisible = false;

                // Task.Run(async () =>
                // {
                //     await Task.Delay(350);
                //     Dispatcher.UIThread.Invoke(() =>
                //     {
                //         _partMask.IsVisible = false;
                //     });
                // });
            }
            else
            {
                _partMask.IsVisible = true;
                _partMask.Opacity   = 1d;
            }
        }

        private async void CreateXAnimation(Animatable control, double start, double end, bool opposite)
        {
            var index         = opposite ? 0 : 1;
            var animationTask = _TaskBuffer[index];
            var animationCTS  = _CTSBuffer[index];
            
            var animation = new Animation
            {
                Duration = TimeSpan.FromMilliseconds(350),
                FillMode = FillMode.Forward,
            };
            animation.Children.Add(new KeyFrame
            {
                Cue = new Cue(0),
                Setters =
                {
                    new Setter { Property = ScaleTransform.ScaleXProperty, Value = start },
                },
            });
            animation.Children.Add(new KeyFrame
            {
                Cue = new Cue(1),
                Setters =
                {
                    new Setter { Property = ScaleTransform.ScaleXProperty, Value = end },
                },
            });

            //
            // opposite 
            // value true -> left : index[0]
            // value false -> right : index[1]

            if (animationTask is not null &&
                !animationTask.IsCompleted)
            {
                animationCTS?.Cancel();
            }
            
            //
            //
            animationCTS = new CancellationTokenSource();
            
            //
            //
            var task = animation.RunAsync(control, animationCTS.Token);
                
            //
            //
            _TaskBuffer[index] = task;
            _CTSBuffer[index]  = animationCTS;
            
            //
            //
            await task;

            //
            //
            _TaskBuffer[index] = null;
            _CTSBuffer[index]   = null;
        }


        private async void CreateYAnimation(Animatable control, double start, double end, bool opposite)
        {
            var index         = opposite ? 2 : 3;
            var animationTask = _TaskBuffer[index];
            var animationCTS  = _CTSBuffer[index];
            var animation = new Animation
            {
                Duration = TimeSpan.FromMilliseconds(350),
                FillMode = FillMode.Forward,
            };
            animation.Children.Add(new KeyFrame
            {
                Cue = new Cue(0),
                Setters =
                {
                    new Setter { Property = ScaleTransform.ScaleYProperty, Value = start },
                },
            });
            animation.Children.Add(new KeyFrame
            {
                Cue = new Cue(1),
                Setters =
                {
                    new Setter { Property = ScaleTransform.ScaleYProperty, Value = end },
                },
            });


            //
            // opposite 
            // value true -> top : index[2]
            // value false -> bottom : index[3]

            if (animationTask is not null &&
                !animationTask.IsCompleted)
            {
                animationCTS?.Cancel();
            }

            //
            //
            animationCTS = new CancellationTokenSource();
            //
            //
            var task = animation.RunAsync(control, animationCTS.Token);
                
            //
            //
            _TaskBuffer[index] = task;
            _CTSBuffer[index]  = animationCTS;
            
            //
            //
            await task;

            //
            //
            _TaskBuffer[index] = null;
            _CTSBuffer[index]   = null;
        }

        private static void OnLeftOpenStateChanged(AvaloniaPropertyChangedEventArgs e)
        {
            var s = ((Drawer)e.Sender);

            if (e.GetNewValue<bool>())
            {
                s._left.RenderTransformOrigin = new RelativePoint(0.5, 0.5, RelativeUnit.Relative);
                s._left.IsVisible             = true;
                //s.CreateXAnimation(s._left, 0, 1, true);
                s.CreateMaskAnimation(false);
            }
            else
            {
                s._left.RenderTransformOrigin = new RelativePoint(0, 0.5, RelativeUnit.Relative);
                s._left.IsVisible             = false;
                s.CreateMaskAnimation(true);
            }
        }

        private static void OnRightOpenStateChanged(AvaloniaPropertyChangedEventArgs e)
        {
            var s = ((Drawer)e.Sender);

            if (e.GetNewValue<bool>())
            {
                s._right.RenderTransformOrigin = new RelativePoint(0.5, 0.5, RelativeUnit.Relative);
                s._right.IsVisible             = true;
                //s.CreateXAnimation(s._right, 0, 1, false);
                s.CreateMaskAnimation(false);
            }
            else
            {
                s._right.RenderTransformOrigin = new RelativePoint(1, 0.5, RelativeUnit.Relative);
                s._right.IsVisible             = false;
                s.CreateMaskAnimation(true);
            }
        }

        private static void OnTopOpenStateChanged(AvaloniaPropertyChangedEventArgs e)
        {

            var s = ((Drawer)e.Sender);

            if (e.GetNewValue<bool>())
            {
                s._top.RenderTransformOrigin = new RelativePoint(0.5, 0.5, RelativeUnit.Relative);
                s._top.IsVisible             = true;
                s.CreateMaskAnimation(false);
            }
            else
            {
                s._top.RenderTransformOrigin = new RelativePoint(0.5, 1, RelativeUnit.Relative);
                s._top.IsVisible             = false;
                s.CreateMaskAnimation(true);
            }
        }

        private static void OnBottomOpenStateChanged(AvaloniaPropertyChangedEventArgs e)
        {

            var s = ((Drawer)e.Sender);

            if (e.GetNewValue<bool>())
            {
                s._bottom.RenderTransformOrigin = new RelativePoint(0.5, 0.5, RelativeUnit.Relative);
                s._bottom.IsVisible             = true;
                s.CreateMaskAnimation(false);
            }
            else
            {
                s._bottom.RenderTransformOrigin = new RelativePoint(0.5, 1, RelativeUnit.Relative);
                s._bottom.IsVisible             = false;
                s.CreateMaskAnimation(true);
            }
        }



        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);
            
            _partMask = e.NameScope.Get<Border>("mask");
            _left     = e.NameScope.Get<Border>("PART_Left");
            _right    = e.NameScope.Get<Border>("PART_Right");
            _top      = e.NameScope.Get<Border>("PART_Top");
            _bottom   = e.NameScope.Get<Border>("PART_Bottom");

            if (_partMask is not null)
            {
                _partMask.PointerPressed += ResetDrawerOpenState;
            }

            if (IsLeftOpen  ||
                IsRightOpen ||
                IsTopOpen   ||
                IsBottomOpen)
            {
                CreateMaskAnimation(false);
            }
        }

        public void Close()
        {
            IsLeftOpen =
                IsRightOpen =
                    IsBottomOpen =
                        IsTopOpen = false;

        }

        private void ResetDrawerOpenState(object sender, PointerPressedEventArgs e)
        {
            Close();
        }

        public bool IsLeftOpen
        {
            get => GetValue(IsLeftOpenProperty);
            set => SetValue(IsLeftOpenProperty, value);
        }

        public object LeftContent
        {
            get => GetValue(LeftContentProperty);
            set => SetValue(LeftContentProperty, value);
        }


        public IDataTemplate LeftContentTemplate
        {
            get => GetValue(LeftContentTemplateProperty);
            set => SetValue(LeftContentTemplateProperty, value);
        }


        public bool IsRightOpen
        {
            get => GetValue(IsRightOpenProperty);
            set => SetValue(IsRightOpenProperty, value);
        }


        public object RightContent
        {
            get => GetValue(RightContentProperty);
            set => SetValue(RightContentProperty, value);
        }



        public IDataTemplate RightContentTemplate
        {
            get => GetValue(RightContentTemplateProperty);
            set => SetValue(RightContentTemplateProperty, value);
        }

        public bool IsTopOpen
        {
            get => GetValue(IsTopOpenProperty);
            set => SetValue(IsTopOpenProperty, value);
        }

        public object TopContent
        {
            get => GetValue(TopContentProperty);
            set => SetValue(TopContentProperty, value);
        }



        public IDataTemplate TopContentTemplate
        {
            get => GetValue(TopContentTemplateProperty);
            set => SetValue(TopContentTemplateProperty, value);
        }

        public bool IsBottomOpen
        {
            get => GetValue(IsBottomOpenProperty);
            set => SetValue(IsBottomOpenProperty, value);
        }


        public object BottomContent
        {
            get => GetValue(BottomContentProperty);
            set => SetValue(BottomContentProperty, value);
        }

        public IDataTemplate BottomContentTemplate
        {
            get => GetValue(BottomContentTemplateProperty);
            set => SetValue(BottomContentTemplateProperty, value);
        }


        public static readonly RoutedEvent<RoutedEventArgs> DrawerClosedEvent;

        public event EventHandler<RoutedEventArgs> DrawerClosed
        {
            add => AddHandler(DrawerClosedEvent, value);
            remove => RemoveHandler(DrawerClosedEvent, value);
        }
    }
}