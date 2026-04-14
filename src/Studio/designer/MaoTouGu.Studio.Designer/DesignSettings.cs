// ----------------------------------------------------------
//            文件：DesignSettings.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月12日 16:29
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using System.IO;
using MaoTouGu.JuXiaoYou.Pages;

namespace MaoTouGu.JuXiaoYou
{
    public static class DesignSettings
    {
        public static void Load()
        {
            var config   = Ioc.Get<IAppConfig>();
            var fileName = Path.Combine(config.DirOfSettings, AppSettings.FileName_Design);
            var t        = JSON.FromFile<ViewList<TemplateProject>>(fileName);

            Projects.AddMany(t, true);
        }

        public static void Save()
        {
            var config   = Ioc.Get<IAppConfig>();
            var fileName = Path.Combine(config.DirOfSettings, AppSettings.FileName_Design);
            JSON.ToFile(fileName, Projects);
        }


        public static ViewList<TemplateProject> Projects { get; } = new ViewList<TemplateProject>();
    }
}