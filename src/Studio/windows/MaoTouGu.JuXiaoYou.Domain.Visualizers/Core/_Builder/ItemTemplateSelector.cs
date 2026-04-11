// ----------------------------------------------------------
//            文件：ItemTemplateSelector.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月15日 19:27
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Visualizers.Core
{
    partial class VisualBlockBuilder
    {
        sealed class InternalDataTemplateSelector : DataTemplateSelector
        {
            public override DataTemplate SelectTemplate(object item, DependencyObject container)
            {
                if (item is null)
                {
                    return null;
                }

                return _DataTemplateTable.GetValueOrDefault(item.GetType(), null);
            }
        }

        public static DataTemplateSelector Selector { get; } = new InternalDataTemplateSelector();
    }
}