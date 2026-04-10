// ----------------------------------------------------------
//            文件：PrivateMessageService.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月10日 23:21
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using LiteDB;
using MaoTouGu.Studio.Database.Operations;

namespace MaoTouGu.Studio.Core
{
    public class PrivateMessageService(ILogger<PrivateMessageService> _Logger, IHubContext<IMHub> _Hub, IDatabaseService _DatabaseService): BackgroundService, IDisposable
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var reader = Channels.Private.Reader;

            var im  = _DatabaseService.GetDatabase("IM");
            var col = im.Database.GetCollection("Private");

            await foreach (var message in reader.ReadAllAsync(stoppingToken))
            {
                try
                {
                    message.Index = DateTime.Now.Ticks;
                    
                    var task = BsonMapper.Global
                                         .Serialize(message)
                                         .AsDocument;
                    
                    //
                    //
                    col.Insert(task);
                    
                    //
                    // 发送消息。
                    await _Hub.Clients
                              .User(message.TargetID)
                              .SendAsync("Received", message,  stoppingToken);
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