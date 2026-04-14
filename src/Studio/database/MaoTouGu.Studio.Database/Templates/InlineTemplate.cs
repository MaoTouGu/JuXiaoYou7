// ----------------------------------------------------------
//            文件：InlineTemplate.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月11日 00:03
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.Studio.Database.Templates
{
    /// <summary>
    /// 帮助用户创建视觉项。
    /// </summary>
    public sealed class InlineTemplate : Nameable
    {
        public List<InlineTemplateItem> Items { get; init; }
    }
}