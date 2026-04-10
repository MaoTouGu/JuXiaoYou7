// ----------------------------------------------------------
//            文件：GravatarUploadAttribute.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月25日 19:09
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Attributes.CFE
{
    /// <summary>
    /// 头像上传
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public sealed class GravatarUploadAttribute : UploadAttribute
    {
        public GravatarUploadAttribute(string name) : this(false, name)
        {
            
        }

        public GravatarUploadAttribute(bool i18n, string name) : base(AssetType.Gravatar, i18n, name)
        {
            
        }
    } 
}