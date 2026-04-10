// ----------------------------------------------------------
//            文件：FlyoutAmbient.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2025年12月25日 14:22
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.Shells.AppModels
{
    public abstract class FlyoutAmbient : Lifetime, IFlyoutAmbient
    {

        public virtual bool ShouldFlyout(ViewModelBase target) => false;

        public virtual void WhenFlyout(ViewModelBase vm)
        {

        }
    }
}