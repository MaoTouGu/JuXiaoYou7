// ----------------------------------------------------------
//            文件：DatabaseStub.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月06日 16:14
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using System.Data;
using System.Security.Cryptography;
using LiteDB;
using LiteDB.Engine;
using MaoTouGu.Foundation;
using MaoTouGu.Studio.Database;

namespace MaoTouGu.Studio.Services
{
    public class DatabaseStub : Disposable
    {
        static readonly SHA256 SHA256 = SHA256.Create();

        private DatabaseStub()
        {

        }

        public static DatabaseStub CreateLocalDatabase(string path, string fileName, IBsonMapper mapper = null)
        {
            var stub         = new DatabaseStub();
            var dataFileName = Path.Combine(path, $"{fileName}.nosdb");
            var logFileName  = Path.Combine(path, $"{fileName}-log.nosdb");
            var dataFS       = new FileStream(dataFileName, FileMode.OpenOrCreate);
            var logFS        = new FileStream(logFileName, FileMode.OpenOrCreate);

            var settings = new EngineSettings
            {
                DataStream  = dataFS,
                InitialSize = 32 * 1024,
                LogStream   = logFS,
            };

            var engine = new LiteEngine(settings);

            stub.FileName   = fileName;
            stub.LogStream  = logFS;
            stub.DataStream = dataFS;
            stub.Database   = new LiteDatabase(engine, mapper ?? UniversalMapper.Instance);

            return stub;
        }

        public static DatabaseStub CreateMemoryDatabase()
        {
            var stub = new DatabaseStub();
            stub.LogStream  = new MemoryStream();
            stub.DataStream = new MemoryStream();

            var settings = new EngineSettings
            {
                DataStream  = stub.LogStream,
                InitialSize = 32 * 1024,
                LogStream   = stub.DataStream,
            };

            var engine = new LiteEngine(settings);
            stub.Database = new LiteDatabase(engine, UniversalMapper.Instance);


            return stub;
        }


        public static string Sha256ToBase64(Stream input)
        {
            var hash = SHA256.ComputeHash(input);
            return Convert.ToBase64String(hash);
        }


        private async Task<string> ComputeStreamHashCode(Stream stream)
        {
            DataStream.Seek(0, SeekOrigin.Begin);
            var thisHashCodeBuffer = await MD5.HashDataAsync(stream);
            var thisHashCode       = Convert.ToBase64String(thisHashCodeBuffer);
            DataStream.Seek(0, SeekOrigin.Begin);
            return thisHashCode;
        }

        private string ComputeStreamHashCodeSynchronize(Stream stream)
        {
            DataStream.Seek(0, SeekOrigin.Begin);
            var thisHashCodeBuffer = MD5.HashData(stream);
            var thisHashCode       = Convert.ToBase64String(thisHashCodeBuffer);
            DataStream.Seek(0, SeekOrigin.Begin);
            return thisHashCode;
        }

        private static async Task<string> ComputeStreamHashCode(Stream srcStream, Stream dstStream)
        {
            srcStream.Seek(0, SeekOrigin.Begin);
            await srcStream.CopyToAsync(dstStream);
            dstStream.Seek(0, SeekOrigin.Begin);
            var thisHashCodeBuffer = await MD5.HashDataAsync(dstStream);
            var thisHashCode       = Convert.ToBase64String(thisHashCodeBuffer);
            dstStream.Seek(0, SeekOrigin.Begin);
            return thisHashCode;
        }

        private static string ComputeStreamHashCodeSynchronize(Stream srcStream, Stream dstStream)
        {
            srcStream.Seek(0, SeekOrigin.Begin);
            srcStream.CopyToAsync(dstStream);
            dstStream.Seek(0, SeekOrigin.Begin);
            var thisHashCodeBuffer = MD5.HashData(dstStream);
            var thisHashCode       = Convert.ToBase64String(thisHashCodeBuffer);
            dstStream.Seek(0, SeekOrigin.Begin);
            return thisHashCode;
        }


        public void Checkpoint()
        {
            Database.Checkpoint();
            Database.Checkpoint();
        }

        public async Task<MemoryStream> DumpAsync()
        {
            Checkpoint();

            var oldDataPosition = DataStream.Position;
            var ms              = new MemoryStream();

            //
            //
            var hashCodeSrcA = await ComputeStreamHashCode(DataStream);
            var hashCodeDstA = await ComputeStreamHashCode(DataStream, ms);

            if (hashCodeDstA != hashCodeSrcA)
            {
                //
                // 数据不一致
                throw new DataException("数据完整性出错！");
            }

            //
            // 恢复
            DataStream.Seek(oldDataPosition, SeekOrigin.Begin);
            ms.Seek(0, SeekOrigin.Begin);
            return ms;
        }

        public async Task<string> DumpAsync(string path)
        {
            Checkpoint();

            var oldDataPosition = DataStream.Position;
            var dataFileName    = Path.Combine(path, $"{FileName}.nosdb");

            await using var dataFS = new FileStream(dataFileName, FileMode.Create);

            //
            //
            var hashCodeSrcA = await ComputeStreamHashCode(DataStream);
            var hashCodeDstA = await ComputeStreamHashCode(DataStream, dataFS);

            if (hashCodeDstA != hashCodeSrcA)
            {
                //
                // 数据不一致
                throw new DataException("数据完整性出错！");
            }

            //
            // 恢复
            DataStream.Seek(oldDataPosition, SeekOrigin.Begin);
            return dataFileName;
        }

        public bool TestDumpAsync()
        {
            Checkpoint();
            var oldDataPosition = DataStream.Position;
            var oldLogPosition  = LogStream.Position;

            using var dataFS = new MemoryStream();
            using var logFS  = new MemoryStream();

            //
            //
            var hashCodeSrcA = ComputeStreamHashCodeSynchronize(DataStream);
            var hashCodeDstA = ComputeStreamHashCodeSynchronize(DataStream, dataFS);

            //
            // 恢复
            DataStream.Seek(oldDataPosition, SeekOrigin.Begin);
            LogStream.Seek(oldLogPosition, SeekOrigin.Begin);

            return hashCodeDstA == hashCodeSrcA;
        }

        public string GetSha256()
        {
            Checkpoint();
            return Sha256ToBase64(DataStream);
        }

        protected sealed override void ReleaseUnmanagedResources()
        {
            Database.Commit();
            Database.Checkpoint();
            Database.Checkpoint();
            Database.Dispose();
        }

        public string FileName   { get; private set; }
        public Stream LogStream  { get; private set; }
        public Stream DataStream { get; private set; }

        public LiteDatabase Database { get; private set; }
    }
}