// ----------------------------------------------------------
//            文件：DatabaseDumpService.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2025年12月23日 14:36
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using System.Diagnostics.CodeAnalysis;
using System.IO.Compression;
using LiteDB;
using MaoTouGu.Foundation;
using MaoTouGu.Shells.Core;
using MaoTouGu.Studio.Database.Utils;

namespace MaoTouGu.Studio.Core
{
    public sealed class DumpInfo
    {
        public DateTime LastFDOp { get; set; }
        public DateTime LastIDOp { get; set; }
    }


    [SuppressMessage("Performance", "CA1873:避免进行可能成本高昂的日志记录")]
    public class DatabaseDumpService(IWebHostEnvironment _Env,
                                     IServiceProvider _Provider,
                                     ILogger<DatabaseDumpService> _Logger) : Disposable, IDatabaseDumpService
    {
        private DateTime _lastIncrementDumpTime;
        private DateTime _lastFullDumpTime;
        private DumpInfo _Info;
        private string   _dumpInfoFileName;
        private string   _dbFingerPrintFileName;

        //
        // 设置数据保存在 %ContentRootPath%\System
        // Dump数据保存在 %ContentRootPath%\Dumps\<Mode><Year>_<Month>_<Day>_<Kikaku_id>
        // 用户数据保存在 %ContentRootPath%\Data\<Kikaku_id>
        // 图片文件数据保存在 %ContentRootPath%\Images\<Kikaku_id>
        // 表情文件数据保存在 %ContentRootPath%\Emoji\<Kikaku_id>
        // 文件数据保存在 %ContentRootPath%\Files\<Kikaku_id>

        void Load(string fileName)
        {
            _Info = JSON.FromFile<DumpInfo>(fileName, () => new DumpInfo
            {
                LastFDOp = DateTime.Now,
                LastIDOp = DateTime.Now,
            });

            _lastFullDumpTime      = _Info.LastFDOp;
            _lastIncrementDumpTime = _Info.LastIDOp;
        }

        void Save(string fileName)
        {
            _Info.LastFDOp = _lastFullDumpTime;
            _Info.LastIDOp = _lastIncrementDumpTime;

            JSON.ToFile(fileName, _Info);
        }

        public void Initialize()
        {
            _dumpInfoFileName      = Path.Combine(SysDir, "LastDump.Json");
            _dbFingerPrintFileName = Path.Combine(SysDir, "FingerPrint.Json");


            Load(_dumpInfoFileName);
        }

        static bool HandleIncrementMode(Dictionary<string, string> dict, string sha256, string key)
        {
            var oldValue = dict.GetValueOrDefault(key);

            if (string.IsNullOrEmpty(oldValue) || oldValue != sha256)
            {
                dict[key] = sha256;
                return true;
            }

            return false;
        }

        static bool HandleFullMode(Dictionary<string, string> dict, string sha256, string key)
        {
            if (dict.ContainsKey(key))
            {
                dict[key] = sha256;
            }
            else
            {
                dict.TryAdd(key, sha256);
            }
            return true;
        }

        public async Task Dump(bool incrementMode)
        {
            string path;


            var now     = DateTime.Now;
            var dumpDir = DirectoryExt.Combine(_Env.ContentRootPath, "bin", "Dump");

            if (incrementMode)
            {
                _lastIncrementDumpTime = now;
                path                   = Path.Combine(dumpDir, $"IncreaseMode_{now:yyyy_MM_dd}.zip");
                Save(_dumpInfoFileName);
            }
            else
            {
                _lastIncrementDumpTime = _lastFullDumpTime = now;
                path                   = Path.Combine(dumpDir, $"FullMode_{now:yyyy_MM_dd}.zip");
                Save(_dumpInfoFileName);
            }

            var dict    = JSON.FromFile(_dbFingerPrintFileName, () => new Dictionary<string, string>());
            var manager = _Provider.GetRequiredService<IDatabaseService>();
            var list    = new List<string>();

            await using var zipToOpen   = new FileStream(path, FileMode.OpenOrCreate) ;
            await using var archive     = new ZipArchive(zipToOpen, ZipArchiveMode.Create);
            
            foreach (var (key, stub) in manager.DatabaseStubs)
            {
                var sha256 = stub.GetSha256();

                bool needDump;

                if (incrementMode)
                {
                    needDump = HandleIncrementMode(dict, sha256, key);
                }
                else
                {
                    needDump = HandleFullMode(dict, sha256, key);
                }


                if (needDump)
                {
                    var name        = $"{stub.FileName}.nosdb";
                    var fileName    = Path.Combine(path, name);
                    var readmeEntry = archive.CreateEntry(name);
                    
                    using (var writer = await readmeEntry.OpenAsync())
                    {
                        //
                        var ms = await stub.DumpAsync();
                        
                        await ms.CopyToAsync(writer);
                    }
                    //
                    //
                    list.Add(fileName);
                }
            }

            JSON.ToFile(_dbFingerPrintFileName, dict);

            await File.AppendAllTextAsync(Path.Combine(SysDir, $"DumpHistory_{now:yyyy_MM_dd_hh_mm}.Json"), JSON.Set(new DumpHistory { Utc = now, Files = list }));
        }

        private string SysDir => DirectoryExt.GetOrCreate(Path.Combine(_Env.ContentRootPath, "bin", "Sys"));

        public DateTime GetLastFullDumpTime() => _lastFullDumpTime;
        public DateTime GetLastIncrementDumpTime() => _lastIncrementDumpTime;

        sealed class DumpHistory
        {
            public DateTime     Utc   { get; init; }
            public List<string> Files { get; init; }
        }
    }
}