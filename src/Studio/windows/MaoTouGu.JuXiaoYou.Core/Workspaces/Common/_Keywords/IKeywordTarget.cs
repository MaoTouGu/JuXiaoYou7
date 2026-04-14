// ----------------------------------------------------------
//            文件：IKeywordTarget.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月10日 21:11
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Workspaces.Common
{
    public interface IKeywordTarget
    {
        Task AddKeyword();
        Task RemoveKeyword(Keyword keyword);

        ViewList<Keyword> Keywords { get; }
    }
}