// ----------------------------------------------------------
//            文件：IDatabaseService.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月06日 15:15
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using LiteDB;

namespace MaoTouGu.Studio.Core
{
    public interface IDatabaseService
    {
        void Checkpoint();
        void Initialize();

        DatabaseStub GetDatabase(string db);

        LiteDatabase IdentityDB { get; }
        LiteDatabase EventDB    { get; }

        IEnumerable<KeyValuePair<string, DatabaseStub>> DatabaseStubs { get; }

        string DataDir   { get; }
        string SysDir    { get; }
        string EmojiDir  { get; }
        string ImagesDir { get; }
        string FilesDir  { get; }
    }
}