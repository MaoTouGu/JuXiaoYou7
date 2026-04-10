// ----------------------------------------------------------
//            文件：InternetEndpointValidationMiddleware.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月06日 14:02
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.Studio.Middlewares
{
    public class InternetEndpointValidationMiddleware
    {
        private readonly RequestDelegate _next;

        public InternetEndpointValidationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        static bool SameSubnet(string ip1, string ip2)
        {
            var a = ip1.Split('.');
            var b = ip2.Split('.');
            
            return a[0] == b[0] &&
                   a[1] == b[1] &&
                   a[2] == b[2];
        }
        
        public async Task Invoke(HttpContext context)
        {
            if (context.User.Identity?.IsAuthenticated == true)
            {
                var loginIp   = context.User.FindFirst("LoginIP")?.Value;
                var currentIp = context.Connection.RemoteIpAddress?.ToString();

                if (loginIp != currentIp && !SameSubnet(loginIp, currentIp))
                {
                    // IP 不一致 → 强制登出
                    await context.SignOutAsync();
                    context.Response.StatusCode = 401;
                    return;
                }
            }

            await _next(context);
        }
    }
}