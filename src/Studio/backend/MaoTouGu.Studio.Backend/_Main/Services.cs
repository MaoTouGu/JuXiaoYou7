// ----------------------------------------------------------
//            文件：Services.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2025年12月23日 13:44
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.Studio
{
    partial class Program
    {
        static void RegisterServices(WebApplicationBuilder builder)
        {
            var services = builder.Services;

            //services.AddSingleton<IPrivateChannelService, PrivateChannelService>();
            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<IDatabaseDumpService, DatabaseDumpService>();
            services.AddSingleton<IDatabaseService, DatabaseService>();
            services.AddSingleton<IResourceLockingService, ResourceLockingService>();
        }

        static void ConfigureServices(WebApplicationBuilder builder)
        {
            var services = builder.Services;

            //
            // 验证
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                    .AddCookie(options =>
                               {
                                   options.Cookie.Name       = "MaoTouGu_JuXiaoYou";
                                   options.LoginPath         = "/auth/unauthorized";
                                   options.AccessDeniedPath  = "/auth/forbidden"; // API 不跳转 HTML
                                   options.ExpireTimeSpan    = TimeSpan.FromHours(72);
                                   options.SlidingExpiration = true;
                                   options.Events
                                          .OnRedirectToLogin = ctx =>
                                                               {
                                                                   ctx.Response.StatusCode = 401;
                                                                   return Task.CompletedTask;
                                                               };
                                   options.Events
                                          .OnRedirectToAccessDenied = ctx =>
                                                                      {
                                                                          ctx.Response.StatusCode = 403;
                                                                          return Task.CompletedTask;
                                                                      };
                               });
            services.AddAuthorization();

            //
            // 启用SignalR
            services.AddSingleton<IUserIdProvider, UserIDProvider>();
            services.AddSignalR(x =>
                                {
                                    x.MaximumReceiveMessageSize = 48 * 1024;
                                })
                    .AddNewtonsoftJsonProtocol(options =>
                                               {
                                                   // 可选：自定义序列化设置 
                                                   options.PayloadSerializerSettings = new JsonSerializerSettings
                                                   {
                                                       TypeNameHandling = TypeNameHandling.All,
                                                   };
                                               });
            //
            // 启用日志
            services.AddLogging(x =>
                                {
                                    x.ClearProviders(); // 清除默认提供程序
                                    x.AddConsole();     // 添加控制台日志
                                    x.AddDebug();       // 添加调试日志
                                    x.AddEventSourceLogger();
                                    x.SetMinimumLevel(LogLevel.Information); // 设置全局最低日志级别
                                });

            //
            // 使用Newtonsoft作为JSON序列化和反序列化程序。
            services.AddControllers()
                    .AddNewtonsoftJson(o =>
                                       {
                                           o.SerializerSettings.TypeNameHandling = TypeNameHandling.Auto;
                                       });

            services.AddSwaggerGen();
            services.AddEndpointsApiExplorer();
            services.AddControllers(); //.AddApplicationPart(typeof(Setup).Assembly);
            // Add services to the container.
            // services.AddAuthorization();
            // services.AddAuthentication("JuXiaoYou")
            //         .AddCookie("JuXiaoYou", options =>
            //                                 {
            //                                     
            //                                 });
            services.AddOpenApi();


            if (!builder.Environment.IsDevelopment())
            {
                //
                //
                services.AddReverseProxy()
                        .LoadFromConfig(builder.Configuration
                                               .GetSection("ReverseProxy"));
            }
        }
    }
}