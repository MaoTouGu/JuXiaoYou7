// ----------------------------------------------------------
//            文件：ContainerStyleSelector.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月02日 17:21
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Indexing
{
    public class ContainerStyleSelector : StyleSelector
    {
        public override Style SelectStyle(object item, DependencyObject container)
        {
            var   listView = ItemsControl.ItemsControlFromItemContainer(container);
            Style style;

            if (item is CommandContainer)
            {
                style = listView?.Resources["Container"] as Style;
            }
            else
            {
                style = listView?.Resources["Command"] as Style;
            }

            return style;
        }
    }
}