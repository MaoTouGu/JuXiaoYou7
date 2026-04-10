// ----------------------------------------------------------
//            文件：IThreadingInvoker.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2025年12月26日 17:59
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.Shells.Threadings
{
    public interface IThreadingInvoker
    {
        void RunOnUIThread(Action callback);
    }
}