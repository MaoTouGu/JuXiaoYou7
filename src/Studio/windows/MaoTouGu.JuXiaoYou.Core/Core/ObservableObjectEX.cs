// ----------------------------------------------------------
//            文件：ObservableObjectEX.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月25日 17:24
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Core
{
    public abstract class ObservableObjectEX<T> : ObservableObject, IVisualConnector where T : PageBase
    {
        public T ViewModel
        {
            get
            {
                var control = ((IVisualConnector)this).Control;
                if (control is null)
                {
                    return null;
                }

                var v = Xaml.FindVisualParent<ForestPage>(control);
                return v?.DataContext as T;
            }
        }

        FrameworkElement IVisualConnector.Control { get; set; }
    }
}