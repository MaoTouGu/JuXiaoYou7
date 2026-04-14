// ----------------------------------------------------------
//            文件：Settings.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月06日 13:39
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using MaoTouGu.JuXiaoYou.Services.Networks;

namespace MaoTouGu.JuXiaoYou.AppModels
{
    public static class Settings
    {

        public static RemoteApi WebApi { get; set; }
        public static Mutex     Mutex  { get; set; }

        public static AppSettings     App     { get; set; }
        public static ProjectSettings Project { get; set; }

        public static ProjectSettings LoadProject()
        {
            var fileName = Path.Combine(Ioc.Get<IAppConfig>().DirOfSettings, AppSettings.FileName_Project);

            return JSON.FromFile<ProjectSettings>(fileName, () => new ProjectSettings
            {

            });
        }


        public static Task SaveProject(this ProjectSettings setting)
        {
            return Task.Run(() =>
                            {
                                var config   = (AppConfig)Ioc.Get<IAppConfig>();
                                var fileName = Path.Combine(config.DirOfSettings, AppSettings.FileName_Project);

                                try
                                {
                                    JSON.ToFile(fileName, setting);
                                }
                                catch(Exception e)
                                {
                                    Console.WriteLine(e);
                                    throw;
                                }
                            });
        }
    }
}