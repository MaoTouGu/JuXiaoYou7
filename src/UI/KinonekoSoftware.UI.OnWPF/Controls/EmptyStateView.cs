using System.Windows;
using System.Windows.Controls;


namespace KinonekoSoftware.UI.Controls
{
    public partial class EmptyStateView : ContentControl
    {
        public static readonly DependencyProperty    EmptyStateTemplateProperty;
        public static readonly DependencyProperty    EmptyStateTemplateSelectorProperty;
        public static readonly DependencyProperty    EmptyStateStringFormatProperty;
        public static readonly DependencyProperty    EmptyStateProperty;
        public static readonly DependencyProperty    IsEmptyProperty;
        public static readonly DependencyProperty    NotEmptyProperty;
        public static readonly DependencyPropertyKey IsEmptyStateVisiblePropertyKey;
        public static readonly DependencyProperty    IsEmptyStateVisibleProperty;

        static EmptyStateView()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(EmptyStateView), new FrameworkPropertyMetadata(typeof(EmptyStateView)));


            IsEmptyStateVisiblePropertyKey = DependencyProperty.RegisterReadOnly(
                                                                                 nameof(IsEmptyStateVisible),
                                                                                 typeof(bool),
                                                                                 typeof(EmptyStateView),
                                                                                 new FrameworkPropertyMetadata(Boxing.False));
            IsEmptyStateVisibleProperty = IsEmptyStateVisiblePropertyKey.DependencyProperty;
            IsEmptyProperty = DependencyProperty.Register(
                                                          nameof(IsEmpty),
                                                          typeof(bool),
                                                          typeof(EmptyStateView),
                                                          new FrameworkPropertyMetadata(Boxing.False, OnIsEmptyChanged));

            NotEmptyProperty = DependencyProperty.Register(
                                                           nameof(NotEmpty),
                                                           typeof(bool),
                                                           typeof(EmptyStateView),
                                                           new FrameworkPropertyMetadata(Boxing.True, OnNotEmptyChanged));

            EmptyStateProperty = DependencyProperty.Register(
                                                             nameof(EmptyState),
                                                             typeof(object),
                                                             typeof(EmptyStateView),
                                                             new FrameworkPropertyMetadata(default(object)));

            EmptyStateTemplateProperty = DependencyProperty.Register(
                                                                     nameof(EmptyStateTemplate),
                                                                     typeof(DataTemplate),
                                                                     typeof(EmptyStateView),
                                                                     new FrameworkPropertyMetadata(default(DataTemplate)));

            EmptyStateTemplateSelectorProperty = DependencyProperty.Register(
                                                                             nameof(EmptyStateTemplateSelector),
                                                                             typeof(DataTemplateSelector),
                                                                             typeof(EmptyStateView),
                                                                             new FrameworkPropertyMetadata(default(DataTemplateSelector)));

            EmptyStateStringFormatProperty = DependencyProperty.Register(
                                                                         nameof(EmptyStateStringFormat),
                                                                         typeof(string),
                                                                         typeof(EmptyStateView),
                                                                         new FrameworkPropertyMetadata(default(string)));
        }

        private static void OnNotEmptyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var v  = (bool)e.NewValue;
            var ev = (EmptyStateView)d;

            ev.IsEmptyStateVisible = !v;
        }


        private static void OnIsEmptyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var v  = (bool)e.NewValue;
            var ev = (EmptyStateView)d;

            ev.IsEmptyStateVisible = v;
        }

        #region EmptyState

        public bool IsEmptyStateVisible
        {
            get => (bool)GetValue(IsEmptyStateVisibleProperty);
            private set => SetValue(IsEmptyStateVisiblePropertyKey, value);
        }

        public bool IsEmpty
        {
            get => (bool)GetValue(IsEmptyProperty);
            set => SetValue(IsEmptyProperty, value);
        }

        public bool NotEmpty
        {
            get => (bool)GetValue(NotEmptyProperty);
            set => SetValue(NotEmptyProperty, value);
        }

        /// <summary>
        /// 获取或设置 <see cref="EmptyStateStringFormat"/> 属性。
        /// </summary>
        public string EmptyStateStringFormat
        {
            get => (string)GetValue(EmptyStateStringFormatProperty);
            set => SetValue(EmptyStateStringFormatProperty, value);
        }

        /// <summary>
        /// 获取或设置 <see cref="EmptyStateTemplateSelector"/> 属性。
        /// </summary>
        public DataTemplateSelector EmptyStateTemplateSelector
        {
            get => (DataTemplateSelector)GetValue(EmptyStateTemplateSelectorProperty);
            set => SetValue(EmptyStateTemplateSelectorProperty, value);
        }

        /// <summary>
        /// 获取或设置 <see cref="EmptyStateTemplate"/> 属性。
        /// </summary>
        public DataTemplate EmptyStateTemplate
        {
            get => (DataTemplate)GetValue(EmptyStateTemplateProperty);
            set => SetValue(EmptyStateTemplateProperty, value);
        }

        /// <summary>
        /// 获取或设置 <see cref="EmptyState"/> 属性。
        /// </summary>
        public object EmptyState
        {
            get => GetValue(EmptyStateProperty);
            set => SetValue(EmptyStateProperty, value);
        }

        #endregion
    }
}