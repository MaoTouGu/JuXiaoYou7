// ----------------------------------------------------------
//            文件：Swagger.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2025年12月23日 13:57
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.Studio
{

    partial class Program
    {
        static WebApplication EnableSwagger(WebApplicationBuilder builder)
        {
            var config  = builder.Configuration;
            var section = config.GetValue<bool>("EnableSwagger");
            var app     = builder.Build();
            
            // Configure the HTTP request pipeline.
            if (section)
            {
                app.MapSwagger();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            return app;
        }
    }
}