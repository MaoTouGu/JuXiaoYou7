// ----------------------------------------------------------
//            文件：CollectionNameValidationMiddleware.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月06日 14:01
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.Studio.Middlewares
{
    public class CollectionNameValidationMiddleware
    {
        private readonly RequestDelegate _next;

        public CollectionNameValidationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        
        public async Task Invoke(HttpContext context)
        {
            if ((context.Request.Path.StartsWithSegments("/api/data", StringComparison.OrdinalIgnoreCase)) &&
                context.Request.Query.TryGetValue("colName", out var value))
            {
                var realValue = value.ToString();

                if (string.IsNullOrEmpty(realValue) || 
                    realValue.Length == 0           || 
                    realValue.Length > 20           ||
                    !realValue.All(x => char.IsLetterOrDigit(x) || x == '$' || x == '_'))
                {
                    context.Response.StatusCode = 400;
                    return;
                }

            }

            await _next(context);
        }
    }
}