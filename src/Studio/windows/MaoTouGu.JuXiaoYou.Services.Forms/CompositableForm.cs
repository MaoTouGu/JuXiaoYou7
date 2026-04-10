// ----------------------------------------------------------
//            文件：CompositableForm.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月23日 22:24
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------


namespace MaoTouGu.JuXiaoYou.Services.CFE
{
    public static partial class CompositableForm
    {
        private static ICompositableForm GetCompositableForm(ViewModelBase dc, object data)
        {
            //
            // 优先考虑Data中的Attribute
            return dc as ICompositableForm;
        }
        
        public static Task<bool> WithDrawerEditor(this PageBase target, object data, Dock dock = Dock.Right)
        {
            var r = GetElementCollection(data);
            var v = Ioc.Get<IAppModel>().GetViewCache(target);

            if (v is null || !r.IsFinished)
            {
                return Task.FromResult(false);
            }

            var icf = GetCompositableForm(target, data);
            var tcs = new TaskCompletionSource<bool>();

            r.Value
             .ForEach(x =>
                      {
                          x.Owner  = icf;
                          x.Source = data;
                      });

            //
            // 放宽到256层，
            //
            // 因为从Page Based ContentHost到Drawer有8层
            // 从Page到SubPage，从SubPage到Control，给够128层。
            var drawer = Xaml.FindVisualParent<Drawer>(v as FrameworkElement, 256);
            var host   = new ContentHost();

            host.ViewModel = new CompositableFormDrawerRoot
            {
                Drawer     = drawer,
                TCS        = tcs,
                Root       = data,
                Collection = r.Value,
            };


            if (dock == Dock.Left)
            {
                drawer.LeftContent = host;
                drawer.IsLeftOpen  = true;
            }
            else if (dock == Dock.Top)
            {
                drawer.TopContent = host;
                drawer.IsTopOpen  = true;
            }
            else if (dock == Dock.Right)
            {
                drawer.RightContent = host;
                drawer.IsRightOpen  = true;
            }
            else
            {
                drawer.BottomContent = host;
                drawer.IsBottomOpen  = true;
            }


            //
            // 等待这个Task
            return tcs.Task;
        }
    }
}