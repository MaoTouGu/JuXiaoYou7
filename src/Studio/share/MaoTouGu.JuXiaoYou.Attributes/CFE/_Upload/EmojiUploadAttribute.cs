// ----------------------------------------------------------
//            文件：EmojiUploadAttribute.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月25日 19:10
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Attributes.CFE
{
    /// <summary>
    /// Emoji上传
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public sealed class EmojiUploadAttribute : UploadAttribute
    {
        public EmojiUploadAttribute(string name) : this(false, name)
        {
            
        }

        public EmojiUploadAttribute(bool i18n, string name) : base(AssetType.Emoji, i18n, name)
        {
            
        }
    } 
}