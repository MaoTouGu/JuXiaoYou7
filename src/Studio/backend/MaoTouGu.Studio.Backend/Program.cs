namespace MaoTouGu.Studio
{
    public partial class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var builder = WebApplication.CreateBuilder(args);

                ConfigSettings(builder);
                ConfigureServices(builder);
                RegisterServices(builder);
                ConfigureHostedServices(builder);

                var app = EnableSwagger(builder);

                //
                // 初始化，文件位于：_Main\ConfigApp.cs
                ConfigApp(app);
                //
                // 初始化，文件位于：_Main\Middlewares.cs
                UseMiddlewares(app);
                
                //
                // 初始化，文件位于：_Main\MapHubs.cs
                MapHubs(app);

                //
                // 初始化，文件位于：_Main\Initialize.cs
                InitializeServices(app.Services);
                app.Run();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
        static void ConfigSettings(WebApplicationBuilder builder)
        {
            var path  = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Features.json");
            builder.Configuration.AddJsonFile(path, optional: false, reloadOnChange: true);
            
            //
            // 只有非Development模式下才重写选项。
            if(builder.Environment.IsDevelopment())
            {
                var path2 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "AppSettings.json");
                builder.Configuration.AddJsonFile(path2, optional: false, reloadOnChange: true);
                
                builder.WebHost.ConfigureKestrel(
                                                 options => options.Configure(builder.Configuration
                                                                                     .GetSection("Kestrel")));
                    
            }
        }
    }
}