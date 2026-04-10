// ----------------------------------------------------------
//            文件：DatabaseNameValidationMiddleware.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月06日 14:01
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.Studio.Middlewares
{
    public class DatabaseNameValidationMiddleware
    {
        private readonly RequestDelegate _next;

        public DatabaseNameValidationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        
        public async Task Invoke(HttpContext context)
        {
            var req = context.Request;
            
            if ((req.Path.StartsWithSegments("/api/data", StringComparison.OrdinalIgnoreCase)) &&
                req.Query.TryGetValue("dbName", out var value))
            {
                var realValue = value.ToString();

                if (string.IsNullOrEmpty(realValue) || 
                    realValue.Length == 0           || 
                    realValue.Length > 20           ||
                    realValue.Any(x => !char.IsLetterOrDigit(x)))
                {
                    context.Response.StatusCode = 400;
                    return;
                }

            }

            await _next(context);
        }
    }
}