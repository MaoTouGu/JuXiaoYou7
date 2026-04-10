// ----------------------------------------------------------
//            文件：UploadAttribute.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月23日 22:39
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Attributes.CFE
{

    
    
    /// <summary>
    /// 范围
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public abstract class UploadAttribute : CFEAttribute
    {
        protected UploadAttribute(AssetType type, string name) : this(type, false, name)
        {
            
        }

        protected UploadAttribute(AssetType type,bool i18n, string name) : base(i18n, name)
        {
            Type = type;
        }
        
        public AssetType Type { get; }
    } 
    
}