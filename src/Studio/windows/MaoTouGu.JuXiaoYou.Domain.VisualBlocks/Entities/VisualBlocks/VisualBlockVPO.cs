// ----------------------------------------------------------
//            文件：VisualBlockVPO.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月15日 16:45
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------


using MaoTouGu.JuXiaoYou.Attributes;
using MaoTouGu.JuXiaoYou.Core;

namespace MaoTouGu.Studio.Database.Entities.VisualBlocks
{
    public abstract class VisualBlockVPO : ObservableObjectEX<JuXiaoYouPage>
    {
        /// <summary>
        /// 
        /// </summary>
        protected abstract VisualBlock GetVisualBlock();

        
        public string Name
        {
            get => GetVisualBlock().Name;
            set
            {
                GetVisualBlock().Name = value;
                RaiseUpdated();
            }
        }
    }
}