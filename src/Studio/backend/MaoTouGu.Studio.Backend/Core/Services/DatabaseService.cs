// ----------------------------------------------------------
//            文件：DatabaseService.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月06日 16:02
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using System.Collections.Concurrent;
using LiteDB;
using MaoTouGu.Foundation;
using MaoTouGu.Foundation.Collections;

namespace MaoTouGu.Studio.Services
{
    public class DatabaseService : Disposable, IDatabaseService
    {
        private readonly LiteDatabase _EventDB;
        private readonly LiteDatabase _IdentityDB;
        private readonly LiteDatabase _TrackDB;

        private readonly ConcurrentDictionary<string, SemaphoreSlim> _FileLocker = new();
        private readonly ConcurrentDictionary<string, DatabaseStub>  _Map        = new();


        private readonly string _sysDir;
        private readonly string _dataDir;
        private readonly string _imagesDir;
        private readonly string _emojiDir;

        private readonly string _filesDir;
        //
        // 设置数据保存在 ..\Bin\System
        // Dump数据保存在 ..\Bin\Dumps\<Mode><Year>_<Month>_<Day>
        // 用户数据保存在 ..\Bin\Data\
        // 图片文件数据保存在 ..\Bin\Images\
        // 表情文件数据保存在 ..\Bin\Emoji\
        // 文件数据保存在 ..\Bin\Files\
        public DatabaseService(IWebHostEnvironment _Env, ILogger<DatabaseService> logger)
        {
            //
            //
            var bin = DirectoryExt.Combine(_Env.ContentRootPath, "bin");

            _sysDir    = DirectoryExt.Combine(bin, "sys");
            _dataDir   = DirectoryExt.Combine(bin, "data");
            _imagesDir = DirectoryExt.Combine(bin, "images");
            _emojiDir  = DirectoryExt.Combine(bin, "emoji");
            _filesDir  = DirectoryExt.Combine(bin, "files");

            _IdentityDB = new LiteDatabase(Path.Combine(_sysDir, "Identity.nosdb"));
            _EventDB    = new LiteDatabase(Path.Combine(_sysDir, "Event.nosdb"));
            _TrackDB    = new LiteDatabase(Path.Combine(_sysDir, "Track.nosdb"));
        }

        protected override void ReleaseUnmanagedResources()
        {
            Release(_EventDB);
            Release(_IdentityDB);
            Release(_TrackDB);

            foreach (var stub in _Map.Values)
            {
                stub.Dispose();
            }
        }

        static void Release(LiteDatabase database)
        {
            database.Checkpoint();
            database.Checkpoint();
            database.Dispose();
        }

        static void Checkpoint(LiteDatabase database)
        {
            database.Checkpoint();
            database.Checkpoint();
        }

        public void Checkpoint()
        {
            Checkpoint(_EventDB);
            Checkpoint(_IdentityDB);
            Checkpoint(_TrackDB);

            _Map.Values
                .ForEach(x => x.Checkpoint());
        }

        public void Initialize()
        {
            try
            {
                var files = Directory.GetFiles(DataDir, "*.nosdb");

                foreach (var file in files)
                {
                    if (file.Contains("-log"))
                    {
                        continue;
                    }

                    var dbName = Path.GetFileNameWithoutExtension(file);

                    if (_Map.TryAdd(dbName, DatabaseStub.CreateLocalDatabase(DataDir, dbName)))
                    {
                        
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public DatabaseStub GetDatabase(string databaseName)
        {
            if (_Map.TryGetValue(databaseName, out var stub))
            {
                return stub;
            }

            if (_FileLocker.TryGetValue(databaseName, out var slim))
            {
                //
                // 等待操作完成，若完成则重试。
                slim.Wait();
                return _Map.GetValueOrDefault(databaseName);
            }

            slim = new SemaphoreSlim(1, 32);

            _FileLocker.TryAdd(databaseName, slim);

            slim.Wait();

            stub = DatabaseStub.CreateLocalDatabase(DataDir, databaseName);

            //
            // 释放锁。
            slim.Release();

            //
            // 释放锁。
            _FileLocker.Remove(databaseName, out _);
            _Map.TryAdd(databaseName, stub);


            return stub;
        }

        public LiteDatabase IdentityDB => _IdentityDB;
        public LiteDatabase EventDB    => _EventDB;

        public IEnumerable<KeyValuePair<string, DatabaseStub>> DatabaseStubs => _Map;

        public string DataDir   => _dataDir;
        public string SysDir    => _sysDir;
        public string EmojiDir  => _emojiDir;
        public string ImagesDir => _imagesDir;
        public string FilesDir  => _filesDir;
    }
}