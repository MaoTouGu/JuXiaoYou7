// ----------------------------------------------------------
//            文件：IFilterMethod.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月09日 15:33
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using MaoTouGu.Studio.Database.Objects;

namespace MaoTouGu.JuXiaoYou.Services.Plugins
{
    public interface IFilterMethod
    {
        /// <summary>
        /// 判断是否可以接受。
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        bool CanAccept(Moniker x);
        
        /// <summary>
        /// 打开列表页
        /// </summary>
        /// <returns></returns>
        PageBase OpenFilter();

        /// <summary>
        /// 打开设定页
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        PageBase OpenSetting(PageBase page);
        
        /// <summary>
        /// 
        /// </summary>
        string Name { get; }
    }
}