// ----------------------------------------------------------
//            文件：IDatabaseManager.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月06日 13:27
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------

namespace MaoTouGu.Studio.Database.Core
{
    public interface IDatabaseManager
    {
        LiteDatabase GetDatabase(string dbName);
    }
}