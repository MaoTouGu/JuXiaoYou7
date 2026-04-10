// ----------------------------------------------------------
//            文件：DatabaseManager.Impl.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月06日 16:36
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using MaoTouGu.Studio.Services;

namespace MaoTouGu.JuXiaoYou.Core
{
    partial class DatabaseManager
    {
        
        class Remote : Disposable, IDatabaseManager
        {
            private readonly ConcurrentDictionary<string, DatabaseStub> _dictionary = new();

            public LiteDatabase GetDatabase(string dbName)
            {
                if (!_dictionary.TryGetValue(dbName, out var stub))
                {
                    stub = DatabaseStub.CreateMemoryDatabase();
                    _dictionary.TryAdd(dbName, stub);
                }


                return stub.Database;
            }
        }

        class Local(string _dir) : Disposable, IDatabaseManager
        {
            private readonly ConcurrentDictionary<string, DatabaseStub> _dictionary = new();

            public LiteDatabase GetDatabase(string dbName)
            {
                if (!_dictionary.TryGetValue(dbName, out var stub))
                {
                    stub = DatabaseStub.CreateLocalDatabase(_dir, dbName);
                    _dictionary.TryAdd(dbName, stub);
                }


                return stub.Database;
            }
        }

    }
}