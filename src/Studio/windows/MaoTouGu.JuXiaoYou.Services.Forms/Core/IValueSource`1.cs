// ----------------------------------------------------------
//            文件：IValueSource`1.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月24日 14:09
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Services.CFE
{
    public interface IValueSource<T>
    {
        object Source { get; set; }
        
        T Value { get; set; }
    }
}