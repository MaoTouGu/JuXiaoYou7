// ----------------------------------------------------------
//            文件：DataTemplateBuilder.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月24日 17:03
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------

namespace MaoTouGu.JuXiaoYou.Core
{
    public static class DataTemplateBuilder
    {
        private static readonly object Margin = new Thickness(8,4,8,4);
        private static readonly object VAlign = VerticalAlignment.Center;
        
        public static DataTemplate Build(Type controlType, Type dataContextType)
        {
            var factory = new FrameworkElementFactory(controlType);

            //
            // 设置DataContext为当前数据上下文
            factory.SetBinding(FrameworkElement.DataContextProperty, new Binding());

            var template = new DataTemplate
            {
                DataType   = dataContextType,
                VisualTree = factory,
            };

            return template;
        }


        public static DataTemplate BuildEnumTemplate()
        {
            var factory = new FrameworkElementFactory(typeof(TextBlock));
            
            
            //
            // 设置DataContext为当前数据上下文
            factory.SetBinding(TextBlock.TextProperty, new Binding
            {
                Converter = Converters.EnumToString,
            });
            
            factory.SetValue(FrameworkElement.MarginProperty, Margin);
            factory.SetValue(FrameworkElement.VerticalAlignmentProperty, VAlign);

            var template = new DataTemplate
            {
                VisualTree = factory,
            };

            return template;
        }
        
        public static DataTemplate BuildTextBlockTemplate(string property, bool useEnumConverter = true)
        {
            var factory = new FrameworkElementFactory(typeof(TextBlock));

            //
            // 设置DataContext为当前数据上下文
            factory.SetBinding(TextBlock.TextProperty, new Binding
            {
                Path      = new PropertyPath(property),
                Converter = useEnumConverter ? Converters.EnumToString : null,
            });
            
            factory.SetValue(FrameworkElement.MarginProperty, Margin);
            factory.SetValue(FrameworkElement.VerticalAlignmentProperty, VAlign);

            var template = new DataTemplate
            {
                VisualTree = factory,
            };

            return template;
        }
    }
}