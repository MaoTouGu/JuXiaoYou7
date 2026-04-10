// ----------------------------------------------------------
//            文件：Initialize.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2025年12月23日 13:45
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.Studio
{
    partial class Program
    {
        static void InitializeServices(IServiceProvider p)
        {
            p.GetRequiredService<IDatabaseDumpService>().Initialize();
            p.GetRequiredService<IUserService>().Initialize();
            p.GetRequiredService<IDatabaseService>().Initialize();
        }
    }
}