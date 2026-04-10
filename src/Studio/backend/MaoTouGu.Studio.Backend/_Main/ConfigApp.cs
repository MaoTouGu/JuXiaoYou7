// ----------------------------------------------------------
//            文件：ConfigApp.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2025年12月23日 13:44
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using MaoTouGu.Studio.Middlewares;

namespace MaoTouGu.Studio
{
    partial class Program
    {
        private static void ConfigApp(WebApplication app)
        {
            app.UseRouting();
            app.UseWebSockets();
            app.UseStaticFiles();

            if (!app.Environment.IsDevelopment())
            {
                app.MapReverseProxy();
            }
            
            app.UseMiddleware<DatabaseNameValidationMiddleware>();
            app.UseMiddleware<CollectionNameValidationMiddleware>();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMiddleware<InternetEndpointValidationMiddleware>();
            app.MapDefaultControllerRoute();
            app.MapOpenApi();
        }
    }
}