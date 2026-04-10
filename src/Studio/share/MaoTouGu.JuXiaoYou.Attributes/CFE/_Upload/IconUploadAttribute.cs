// ----------------------------------------------------------
//            文件：IconUploadAttribute.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月25日 19:11
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Attributes.CFE
{
    
    
    /// <summary>
    /// 图标上传
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public sealed class IconUploadAttribute : UploadAttribute
    {
        public IconUploadAttribute(string name) : this(false, name)
        {
            
        }

        public IconUploadAttribute(bool i18n, string name) : base(AssetType.Icon, i18n, name)
        {
            
        }
    }
}