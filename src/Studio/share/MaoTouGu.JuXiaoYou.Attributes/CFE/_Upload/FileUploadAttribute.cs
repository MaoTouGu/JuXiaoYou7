// ----------------------------------------------------------
//            文件：FileUploadAttribute.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月25日 19:11
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Attributes.CFE
{
    
    /// <summary>
    /// 文件上传
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public sealed class FileUploadAttribute : UploadAttribute
    {
        public FileUploadAttribute(string name) : this(false, name)
        {
            
        }

        public FileUploadAttribute(bool i18n, string name) : base(AssetType.File,i18n, name)
        {
            
        }
    }  
}