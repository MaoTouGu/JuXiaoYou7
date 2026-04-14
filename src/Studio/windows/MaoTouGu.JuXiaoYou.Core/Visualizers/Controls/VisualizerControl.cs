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

namespace MaoTouGu.JuXiaoYou.Visualizers.Controls
{
    public abstract partial class VisualizerControl : UserControl
    {
        public static readonly DependencyProperty OptionsProperty =
            DependencyProperty.Register(
                                        nameof(Options),
                                        typeof(IVisualizerOptions),
                                        typeof(VisualizerControl),
                                        new PropertyMetadata(null, OnOptionChanged));

        public static readonly DependencyProperty MonikerProperty =
            DependencyProperty.Register(
                                        nameof(Moniker),
                                        typeof(Moniker),
                                        typeof(VisualizerControl),
                                        new PropertyMetadata(null, OnMonikerChanged));

        //
        // IVisualizerOptions 发生变化
        // MonikerSetting 发生变化
        // IVisualizerOptions内的集合发生变化。


        protected VisualizerControl()
        {
            //
            // 强制最小缩放。
            MinHeight = 20;
            MinWidth  = 20;

            //
            // 用一个VisualConnector连接V，方便寻找对应的ViewModel。
            VisualConnector.SetConnect(this, true);
            DataContextChanged += OnDataContextChanged;
            Loaded += OnLoaded;
        }
        
        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            if (Moniker is {} m && Options is {} o)
            {
                Setup(m, o);
            }
        }

        /*******************************************************************
         *
         *
         *
         *
         *
         *******************************************************************/


        /*******************************************************************
         *
         *
         *
         *
         *
         *******************************************************************/
        protected virtual void Setup(Moniker m, IVisualizerOptions options)
        {
        }

        protected virtual void OptionChangedOverride(Moniker m, IVisualizerOptions options)
        {

        }

        protected virtual void StructureChangedOverride(Moniker m, IVisualizerOptions options)
        {

        }


        /*******************************************************************
         *
         *
         *
         *
         *
         *******************************************************************/



        private static void OnMonikerChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not VisualizerControl vc)
            {
                return;
            }

            if (e.NewValue is Moniker m && vc.Options is {} o)
            {
                vc.Setup(m, o);
            }
        }

        private static void OnOptionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not VisualizerControl vc)
            {
                return;
            }

            if (e.OldValue is IVisualizerOptions oldValue)
            {
                oldValue.StructureChanged -= vc.OnStructureChanged;
                oldValue.OptionChanged    -= vc.OnOptionChanged;
            }

            if (e.NewValue is IVisualizerOptions o && vc.Moniker is {} m)
            {
                vc.Setup(m, o);

                o.StructureChanged += vc.OnStructureChanged;
                o.OptionChanged    += vc.OnOptionChanged;
            }
        }

        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is TypographyBlockVPO vpo)
            {
                SetBinding(HeightProperty, new Binding { Source = vpo, Mode = BindingMode.OneWay, Path = new PropertyPath(nameof(TypographyBlockVPO.Height)) });
                SetBinding(WidthProperty, new Binding { Source  = vpo, Mode = BindingMode.OneWay, Path = new PropertyPath(nameof(TypographyBlockVPO.Width)) });
            }
        }



        /*******************************************************************
         *
         *
         *
         *
         *
         *******************************************************************/
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