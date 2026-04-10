namespace MaoTouGu.Studio.Services
{
    public class DatabaseSchedulerService(IDatabaseService _Service) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                //
                // 延迟10分钟
                await Task.Delay(TimeSpan.FromMinutes(10), stoppingToken);

                //
                // 将所有操作写入数据库中。
                _Service.Checkpoint();
            }
        }
    }
}