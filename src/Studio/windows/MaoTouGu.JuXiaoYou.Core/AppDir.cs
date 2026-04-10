// ----------------------------------------------------------
//            文件：AppDir.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2025年12月23日 02:55
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------



namespace MaoTouGu.JuXiaoYou
{
    public static class AppDir
    {
        public static void Initialize(string dir)
        {
            var myDocs = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            //
            //
            MaoTouGu = DirectoryExt.GetOrCreate(Path.Combine(myDocs, nameof(MaoTouGu)));
            App      = DirectoryExt.GetOrCreate(Path.Combine(MaoTouGu, dir));
            Settings = DirectoryExt.GetOrCreate(Path.Combine(App, "Settings"));
            Logs     = DirectoryExt.GetOrCreate(Path.Combine(App, "Logs"));
            UserData = DirectoryExt.GetOrCreate(Path.Combine(App, "UserData"));
        }

        public static string MaoTouGu { get; private set; }
        public static string App      { get; private set; }
        public static string Settings { get; private set; }
        public static string Logs     { get; private set; }
        public static string UserData { get; private set; }
    }
}