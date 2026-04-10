// ----------------------------------------------------------
//            文件：DatabaseObject.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2025年12月22日 16:37
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using LiteDB;
using MaoTouGu.Foundation;

namespace MaoTouGu.Studio.Database.Core
{
    public abstract class DatabaseObject : ObservableObject
    {
        [BsonId]
        public string Id { get; init; }
    }
}