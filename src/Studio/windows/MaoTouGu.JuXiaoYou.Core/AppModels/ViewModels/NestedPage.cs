// ----------------------------------------------------------
//            文件：NestedPage.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月08日 00:22
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.AppModels
{
    public abstract class NestedPage : JuXiaoYouPage
    {
        protected NestedPage(DatabaseObject ob, JuXiaoYouPage parent)
        {
            Parent     = parent;
            InstanceID = ob.Id;
        }
        protected NestedPage(string id, JuXiaoYouPage parent)
        {
            Parent     = parent;
            InstanceID = id;
        }

        /// <summary>
        /// 
        /// </summary>
        public JuXiaoYouPage Parent { get; init; }
    }
}