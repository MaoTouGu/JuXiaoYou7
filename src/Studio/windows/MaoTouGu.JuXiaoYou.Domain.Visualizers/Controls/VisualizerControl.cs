// ----------------------------------------------------------
//            文件：VisualizerControl.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月11日 18:21
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using System.ComponentModel;
using MaoTouGu.Studio.Database.Core;
using MaoTouGu.Studio.Database.Objects;

namespace MaoTouGu.JuXiaoYou.Visualizers.Core
{
    public abstract partial class VisualizerControl : UserControl
    {
        public static readonly DependencyProperty OptionsProperty =
            DependencyProperty.Register(
                                        nameof(Options),
                                        typeof(IVisualizerOptions),
                                        typeof(VisualizerControl),
                                        new PropertyMetadata(null, OnOptionsChanged));

        public static readonly DependencyProperty MonikerProperty =
            DependencyProperty.Register(
                                        nameof(Moniker),
                                        typeof(Moniker),
                                        typeof(VisualizerControl),
                                        new PropertyMetadata(null, OnMonikerChanged));


        protected VisualizerControl()
        {
            //
            // 强制最小缩放。
            MinHeight = 20;
            MinWidth  = 20;
            VisualConnector.SetConnect(this, true);
            DataContextChanged += OnDataContextChanged;
        }

        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is TypographyBlockVPO vpo)
            {
                SetBinding(HeightProperty, new Binding { Source = vpo, Mode = BindingMode.OneWay, Path = new PropertyPath(nameof(TypographyBlockVPO.Height)) });
                SetBinding(WidthProperty, new Binding { Source  = vpo, Mode = BindingMode.OneWay, Path = new PropertyPath(nameof(TypographyBlockVPO.Width)) });
            }
        }

        /// <summary>
        /// 给定一个Moniker和一个喵喵咒语，获得绑定。
        /// </summary>
        /// <param name="moniker"></param>
        /// <param name="setting"></param>
        /// <param name="converter"></param>
        /// <returns></returns>
        protected Binding GetBinding(Moniker moniker, string setting, IValueConverter converter = null)
        {
            if (!moniker.ContainSettingItem(setting) && !string.IsNullOrEmpty(setting))
            {
                moniker.Settings.Add(setting, string.Empty);
            }

            var binding = new Binding
            {
                Source    = moniker.Settings,
                Path      = new PropertyPath($"[{setting}]"),
                Mode      = BindingMode.OneWay,
                Converter = converter,
            };

            return binding;
        }



        private static void OnMonikerChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not VisualizerControl vc)
            {
                return;
            }

            if (e.NewValue is Moniker m && vc.Options is {} o)
            {
                vc.BuildExpression(m, o);
            }
        }

        private static void OnOptionsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not VisualizerControl vc)
            {
                return;
            }

            if (e.OldValue is IVisualizerOptions oldValue)
            {
                oldValue.PropertyChanged -= vc.WhenOptionChanged;
            }

            if (e.NewValue is IVisualizerOptions o && vc.Moniker is {} m)
            {
                vc.BuildExpression(m, o);
                o.PropertyChanged += vc.WhenOptionChanged;
            }
        }


        /// <summary>
        /// 当<see cref="TypographyVisualizerVPO.Options"/>属性发生变化的时候，需要通知所有VisualizerControl变更绑定。
        /// </summary>
        private void WhenOptionChanged(object sender, PropertyChangedEventArgs e)
        {
            if (Options is not {} o || Moniker is not {} m)
            {
                return;
            }

            //
            // DONE: 需要做事件限流，防止过多的ToBase64调用进而导致CPU浪费。
            //
            // 采用4hz的频率，间隔250ms更新一次。
            if (!_requestTable.ContainsKey(GetHashCode()))
            {
                _throttleEvent.Moniker = m;
                _throttleEvent.Options = o;
                _throttleEvent.VPO     = DataContext as TypographyVisualizerVPO;

                _requestTable.TryAdd(GetHashCode(), 1);
                _throttleRequests.Enqueue(this);
            }

            //
            // OnBuildExpression(m, o);
            // if (DataContext is TypographyVisualizerVPO vpo)
            // {
            //     vpo.Instance
            //        .Base64 = o.ToBase64();
            // }
        }

        protected void BuildExpression(Moniker m, IVisualizerOptions options)
        {

            //
            //
            OnBuildExpression(m, options);
        }

        protected abstract void OnBuildExpression(Moniker m, IVisualizerOptions options);

        public Moniker Moniker
        {
            get => (Moniker)GetValue(MonikerProperty);
            set => SetValue(MonikerProperty, value);
        }

        public IVisualizerOptions Options
        {
            get => (IVisualizerOptions)GetValue(OptionsProperty);
            set => SetValue(OptionsProperty, value);
        }
    }
}