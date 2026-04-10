// ----------------------------------------------------------
//            文件：Deserialize.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月06日 03:15
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.Studio.Database.Core
{
    partial class DataService<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected internal virtual T Deserialize(BsonDocument document) => Mapper.Deserialize<T>(document);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected internal virtual T Deserialize(BsonValue document) => Mapper.Deserialize<T>(document.AsDocument);
    }
}