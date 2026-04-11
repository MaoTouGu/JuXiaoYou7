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
    public abstract class VisualizerControl : UserControl
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
                                        new PropertyMetadata(default(Moniker), OnMonikerChanged));

        private readonly List<Binding> _bindings = new List<Binding>();
        
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

            _bindings.Add(binding);
            
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
                vc.OnBuildExpression(m, o);
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
                oldValue.PropertyChanged -= vc.OnOptionChanged;
            }

            if (e.NewValue is IVisualizerOptions o && vc.Moniker is {} m)
            {
                vc.OnBuildExpression(m, o);
                o.PropertyChanged += vc.OnOptionChanged;
            }
        }

        private void OnOptionChanged(object sender, PropertyChangedEventArgs e)
        {
            if (Options is {} o && Moniker is {} m)
            {
                OnBuildExpression(m, o);
            }
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