// ----------------------------------------------------------
//            文件：CompositableFormDrawerRoot.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月23日 22:27
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using KinonekoSoftware.UI.Controls;

namespace MaoTouGu.JuXiaoYou.Services.CFE
{
    public sealed class CompositableFormDrawerRoot : PageBase
    {
        private bool _result;
        public CompositableFormDrawerRoot()
        {
            Finish = new DelegateCommand(DoFinish);
            Cancel = new DelegateCommand(DoCancel);
        }

        private void DoFinish()
        {
            _result             = true;
            Drawer.IsLeftOpen   = false;
            Drawer.IsTopOpen    = false;
            Drawer.IsRightOpen  = false;
            Drawer.IsBottomOpen = false;
            OnDrawerClosed(null, null);
        }
        private void DoCancel()
        {
            _result             = false;
            Drawer.IsLeftOpen   = false;
            Drawer.IsTopOpen    = false;
            Drawer.IsRightOpen  = false;
            Drawer.IsBottomOpen = false;
            OnDrawerClosed(null, null);
        }

        private void OnDrawerClosed(object sender, RoutedEventArgs e)
        {
            Stop();


            Drawer.LeftContent   = null;
            Drawer.TopContent    = null;
            Drawer.RightContent  = null;
            Drawer.BottomContent = null;
        }


        protected override void OnStart()
        {
            //
            // Binding Drawer
            Drawer.DrawerClosed += OnDrawerClosed;
            base.OnStart();
        }

        protected override void StopBefore()
        {
            try
            {
                if (TCS.TrySetResult(_result))
                {
                }
            }
            catch(Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }

        protected override void OnStop()
        {
            Drawer.DrawerClosed -= OnDrawerClosed;


            base.OnStop();
        }

        public ICommandEX Finish { get; }
        public ICommandEX Cancel { get; }

        public object Root   { get; init; }
        public Drawer Drawer { get; init; }

        public CFElementCollection        Collection { get; init; }
        public TaskCompletionSource<bool> TCS        { get; init; }

    }
}