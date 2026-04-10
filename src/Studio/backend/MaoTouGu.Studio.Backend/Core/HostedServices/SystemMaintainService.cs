// ----------------------------------------------------------
//            文件：SystemMaintainService.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2025年12月23日 14:20
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using MaoTouGu.Studio.Database.Core;
using MaoTouGu.Studio.Database.Spots;
using MaoTouGu.Studio.Hubs;
#pragma warning disable CA2254

namespace MaoTouGu.Studio.Services.Backgrounds
{
    public class SystemMaintainService(IDatabaseDumpService _dumpService,
                                       ILogger<SystemMaintainService> _Logger,
                                       IHubContext<PushingHub> _Hub) : BackgroundService
    {
        private const int MinutesGapOf1Day  = 1440;
        private const int MinutesGapOf28Day = 40320;

        private int Current;

        private async Task NotifyClientsServiceAvailable()
        {
            await _Hub.Clients
                      .All
                      .SendAsync(nameof(ISpotRecipient.WhenDataChanged), new ServiceStateSpot
                       {
                           Offline = false,
                       });
        }

        private async Task NotifyClientsServiceUnavailable()
        {
            await _Hub.Clients
                      .All
                      .SendAsync(nameof(ISpotRecipient.WhenDataChanged), new ServiceStateSpot
                       {
                           Offline = true,
                       });
        }

        private async Task DoMaintainSystem_Seconds()
        {
            var now            = DateTime.Now;
            var last_fd_opTime = _dumpService.GetLastFullDumpTime();
            var last_id_opTime = _dumpService.GetLastIncrementDumpTime();

            var fdOpGap  = now - last_fd_opTime;
            var idOpGap  = now - last_id_opTime;
            var needFdOp = (fdOpGap.TotalSeconds * 10) >= MinutesGapOf28Day;
            var needIdOp = (idOpGap.TotalSeconds * 10) >= MinutesGapOf1Day;

            if (needIdOp || needFdOp)
            {
                _Logger.LogInformation("正在备份！系统将会提示所有客户端立即下线。");
                await NotifyClientsServiceUnavailable();
            }
            else
            {
                if ((Current % 60) == 0)
                {
                    var idBackup = (int)(MinutesGapOf1Day  - idOpGap.TotalSeconds * 10);
                    var fdBackup = (int)(MinutesGapOf28Day - fdOpGap.TotalSeconds * 10);
                    var str      = $"还未到自动备份时间，距离增量备份还有{idBackup}秒，距离全量备份还有{fdBackup}秒";
                    _Logger.LogInformation(str);
                }

                Current++;
            }

            if (needFdOp)
            {

                try
                {
                    await _dumpService.Dump(false);
                    await NotifyClientsServiceAvailable();
                }
                catch(Exception e)
                {
                    _Logger.LogWarning(e.Message);
                }
            }
            else if (needIdOp)
            {
                _Logger.LogInformation("正在全量备份！系统将会提示所有客户端立即下线。");

                try
                {
                    await _dumpService.Dump(true);
                    await NotifyClientsServiceAvailable();
                }
                catch(Exception e)
                {
                    _Logger.LogWarning(e.Message);
                }
            }
        }
        
        private async Task DoMaintainSystem_Minutes()
        {
            var now            = DateTime.Now;
            var last_fd_opTime = _dumpService.GetLastFullDumpTime();
            var last_id_opTime = _dumpService.GetLastIncrementDumpTime();

            var fdOpGap  = now - last_fd_opTime;
            var idOpGap  = now - last_id_opTime;
            var needFdOp = fdOpGap.TotalMinutes >= MinutesGapOf28Day;
            var needIdOp = idOpGap.TotalMinutes >= MinutesGapOf1Day;

            if (needIdOp || needFdOp)
            {
                _Logger.LogInformation("正在备份！系统将会提示所有客户端立即下线。");
                await NotifyClientsServiceUnavailable();
            }
            else
            {
                if ((Current % 60) == 0)
                {
                    var idBackup = (int)(MinutesGapOf1Day  - idOpGap.TotalMinutes);
                    var fdBackup = (int)(MinutesGapOf28Day - fdOpGap.TotalMinutes);
                    var str      = $"还未到自动备份时间，距离增量备份还有{idBackup}分钟，距离全量备份还有{fdBackup}分钟";
                    _Logger.LogInformation(str);
                }

                Current++;
            }

            if (needFdOp)
            {

                try
                {
                    await _dumpService.Dump(false);
                    await NotifyClientsServiceAvailable();
                }
                catch(Exception e)
                {
                    _Logger.LogWarning(e.Message);
                }
            }
            else if (needIdOp)
            {
                _Logger.LogInformation("正在全量备份！系统将会提示所有客户端立即下线。");

                try
                {
                    await _dumpService.Dump(true);
                    await NotifyClientsServiceAvailable();
                }
                catch(Exception e)
                {
                    _Logger.LogWarning(e.Message);
                }
            }
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //
            // 系统维护服务：
            // 1. 每间隔1天进行一次增量备份。
            // 2. 每间隔28天进行一次全量备份。
            while (!stoppingToken.IsCancellationRequested)
            {

                if (_Debug)
                {
                    //
                    // 每10ms检查一次
                    await DoMaintainSystem_Seconds();
                    await Task.Delay(TimeSpan.FromMilliseconds(1), stoppingToken);
                }
                else
                {
                    //
                    // 每分钟检查一次
                    await DoMaintainSystem_Minutes();
                    await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
                }

            }
        }
#if DEBUG
        private readonly bool _Debug = true;
#else
        private readonly bool _Debug = false;
#endif
    }
}