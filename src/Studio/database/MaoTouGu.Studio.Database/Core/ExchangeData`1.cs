// ----------------------------------------------------------
//            文件：ExchangeData`1.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年03月29日 14:33
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.Studio.Database.Core
{
    public sealed class ExchangeData<T> : ExchangeData
    {
        public T Data { get; init; }
    }
}