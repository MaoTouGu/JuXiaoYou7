// ----------------------------------------------------------
//            文件：IViewModelProvider.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月23日 10:28
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.AppModels
{
    internal interface IViewModelProvider
    {
        IEnumerable<ViewModelBase> GetContextList();
    }
}