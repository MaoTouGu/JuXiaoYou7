// ----------------------------------------------------------
//            文件：FilterMethod.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月09日 15:37
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using MaoTouGu.Studio.Database.Objects;

namespace MaoTouGu.JuXiaoYou.Services.Plugins
{
    public abstract class FilterMethod : IFilterMethod
    {

        public abstract bool CanAccept(Moniker x);
        public abstract PageBase OpenFilter() ;
        public abstract PageBase OpenSetting(PageBase page) ;
        
        public abstract string Name { get; }
    }
}