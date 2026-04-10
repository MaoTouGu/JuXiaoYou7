// ----------------------------------------------------------
//            文件：SecurityTracingService.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2025年12月23日 20:22
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------

using LiteDB;
using MaoTouGu.Studio.Database.Operations;

namespace MaoTouGu.Studio.Services.Backgrounds
{
    public class SecurityTracingService : BackgroundService
    {
        private readonly ILiteCollection<SecurityOperation> _collection;

        //
        // 设置数据保存在 %ContentRootPath%\System
        public SecurityTracingService(IDatabaseService _Env)
        {
            var database = _Env.EventDB;
            _collection = database.GetCollection<SecurityOperation>("Security");
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var reader = Channels.Security.Reader;

            await foreach (var task in reader.ReadAllAsync(stoppingToken))
            {
                try
                {
                    _collection.Insert(task);

                }
                catch(Exception ex)
                {
                    //TODO: 日志、重试、死信队列
                    Console.WriteLine($"DB Error: {ex.Message}");
                }
            }
        }
    }
}