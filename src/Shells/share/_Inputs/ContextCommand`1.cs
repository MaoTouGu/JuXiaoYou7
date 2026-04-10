// ----------------------------------------------------------
//            文件：ContextCommand`1.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2025年12月23日 10:44
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.Shells.Inputs
{
    public abstract class ContextCommand<T>(T target) : _Command
    {
        protected T Context => target;
    }
}