// ----------------------------------------------------------
//            文件：IndexSystem.Label.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年03月18日 20:14
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Indexing
{
    partial class IndexSystem
    {
        public static async Task<Keyword> SetKeyword(string domain, string subject, string documentID, string name)
        {
            var keyword = new Keyword
            {
                Id         = ID.Get(),
                Domain     = domain,
                DocumentID = documentID,
                Subject    = subject,
                Name       = name,
            };

            await KeywordService.Add(keyword);
            return keyword;
        }
    }
}