// ----------------------------------------------------------
//            文件：SelectDomainImageCommand.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年03月29日 22:53
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------

namespace MaoTouGu.JuXiaoYou.Startups
{
    sealed class SelectDomainImageCommand(DomainEditorViewModel target) : SelectImageCommand<Domain, DomainEditorViewModel>(target)
    {
        protected override void OnSetImage(Domain target, string id, int w, int h)
        {
            target.ImageWidth  = w;
            target.ImageHeight = h;
            target.Width       = 240;
            target.Height      = 135;
            target.Image       = id;
            target.X           = target.Y = 0;
        }
        
        protected override void OnSetImageFailed(Domain target)
        {
            target.ImageWidth  = 0;
            target.ImageHeight = 0;
            target.Width       = 0;
            target.Height      = 0;
            target.Image       = null;
            target.X           = target.Y = 0;
        }
    }
}