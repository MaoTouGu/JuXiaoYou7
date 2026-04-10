// ----------------------------------------------------------
//            文件：DatabaseManager.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月13日 19:36
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using MaoTouGu.Studio.Database.Core;
using MaoTouGu.Studio.Services;

namespace MaoTouGu.JuXiaoYou.Core
{
    public static partial class DatabaseManager
    {
        static DatabaseManager()
        {
            Services = new List<DataService>(32);
        }
        
        

        public static T GetService<T>() where T : DataService
        {
            var srv = Services.OfType<T>().FirstOrDefault();
            
            if (srv is null)
            {
                srv = Ioc.GetOrRegister<T>();
                Services.Add(srv);
            }

            return srv;
        }

        public static void SetDataSource<T>() where T : DataService, IDataSource
        {
            var srv = Ioc.GetOrRegister<T>();
            
            if (!Ioc.IsRegistered<T>())
            {
                //
                //
                Services.Add(srv);
            }
        } 

        public static IDatabaseManager Create(Project project)
        {
            if (project.IsOnline)
            {
                return new Remote();
            }

            return new Local(project.Url);
        }

        public static List<DataService> Services { get; }
    }
}