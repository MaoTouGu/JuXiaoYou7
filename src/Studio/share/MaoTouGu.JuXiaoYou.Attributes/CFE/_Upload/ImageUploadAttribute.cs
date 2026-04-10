// ----------------------------------------------------------
//            文件：ImageUploadAttribute.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月25日 19:08
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Attributes.CFE
{
    /// <summary>
    /// 范围
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public sealed class ImageUploadAttribute : UploadAttribute
    {
        public ImageUploadAttribute(string name) : this(false, name)
        {
            
        }

        public ImageUploadAttribute(bool i18n, string name) : base(AssetType.Image, i18n, name)
        {
            
        }
    }
}