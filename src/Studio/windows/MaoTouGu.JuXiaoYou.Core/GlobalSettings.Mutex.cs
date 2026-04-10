// ----------------------------------------------------------
//            文件：GlobalSettings.Mutex.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月13日 19:19
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou
{
    partial class GlobalSettings
    {
        /// <summary>
        /// 判断是否重复运行应用？
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool EnsureAppNotRun(string str)
        {
            try
            {
                Mutex = new Mutex(true, str, out var createdNew);

                if (!createdNew)
                {
                    //
                    // 已经运行了。
                    Mutex.Dispose();
                    return true;
                }

                return false;
            }
            catch
            {
                //
                // 未知错误。
                return false;
            }
        }
        
        /// <summary>
        /// 用于避免重复打开数据库的Mutex。
        /// </summary>
        public static Mutex Mutex { get; set; }
    }
}