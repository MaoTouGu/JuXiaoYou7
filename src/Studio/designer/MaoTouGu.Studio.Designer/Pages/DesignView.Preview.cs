// ----------------------------------------------------------
//            文件：DesignView.Preview.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月12日 15:35
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using System.Windows;
using System.Windows.Controls;

namespace MaoTouGu.JuXiaoYou.Pages
{
    partial class DesignView
    {
        void RenderCanvasLoop(object sender, EventArgs r)
        {
            var dc   = ViewModel<DesignViewModel>();
            var page = dc.Page;
            var dpi  = (int)VisualTreeHelper.GetDpi(this).PixelsPerDip * 96;

            if (page is null)
            {
                return;
            }

            page.Bitmap = Xaml.Capture(Items, dpi);
        }
        
        private async void Selection_RenderCanvas(object sender, SelectionChangedEventArgs e)
        {
            await Task.Delay(1000);
            RenderCanvasLoop(null, null);
        }
    }
}