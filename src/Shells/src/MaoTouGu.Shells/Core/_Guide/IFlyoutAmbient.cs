// ----------------------------------------------------------
//            文件：IFlyoutAmbient.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2025年12月25日 14:20
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.Shells.Core
{
    public interface IFlyoutAmbient
    {
        bool ShouldFlyout(ViewModelBase target);

        void WhenFlyout(ViewModelBase vm);
    }
}