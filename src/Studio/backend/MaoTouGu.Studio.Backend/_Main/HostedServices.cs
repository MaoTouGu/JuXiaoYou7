// ----------------------------------------------------------
//            文件：HostedServices.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2025年12月23日 13:56
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------

using MaoTouGu.Studio.Services.Backgrounds;

namespace MaoTouGu.Studio
{
    partial class Program
    {
        
        static void ConfigureHostedServices(WebApplicationBuilder builder)
        {
            var services    = builder.Services;
            // var dataService = new DatabaseService();
            //
            // services.AddSingleton<IDatabaseService>(dataService);
            // services.AddSingleton<IAccountService, AccountService>();
            // services.AddSingleton<ICreationSpaceService, CreationSpaceService>();
            // //
            // // services.AddSingleton<IFineGrainedLockerService, FineGrainedLockerService>()
            // services.AddHostedService<DatabaseSchedulerService>();
            services.AddHostedService<DatabaseSchedulerService>();
            services.AddHostedService<SecurityTracingService>();
            services.AddHostedService<IdentityTracingService>();
            services.AddHostedService<SystemMaintainService>();
            services.AddHostedService<DocumentLockService>();
            services.AddHostedService<PrivateMessageService>();
        }

    }
}