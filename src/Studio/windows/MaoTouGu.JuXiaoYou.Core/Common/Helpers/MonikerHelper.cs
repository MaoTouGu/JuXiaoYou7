// ----------------------------------------------------------
//            文件：MonikerHelper.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月10日 21:02
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Common.Helpers
{
    /// <summary>
    /// <see cref="MonikerHelper"/> 用于提供与设定相关的操作API。
    /// </summary>
    public static class MonikerHelper
    {
        public static async Task<IEnumerable<Keyword>> FindKeyword(string documentID, KeywordService service = null)
        {
            service ??= DatabaseManager.GetService<KeywordService>();

            await service.Start();

            return service.FindByDocumentId(documentID);
        }

        public static async Task<Keyword> AddKeyword(string name, string documentID, KeywordService service = null)
        {
            service ??= DatabaseManager.GetService<KeywordService>();

            var keyword = new Keyword
            {
                Id         = ID.Get(),
                Name       = name,
                DocumentID = documentID,
            };

            await service.Start();
            await service.Add(keyword);

            return keyword;
        }
        
        public static async Task RemoveKeyword(string id, KeywordService service = null)
        {
            service ??= DatabaseManager.GetService<KeywordService>();

            await service.Start();
            await service.Remove(id);
        }

        public static async Task<Keyword> AddKeyword(PageBase target, string id, KeywordService service = null)
        {
            var r = await target.SingleLine("新建", "新建一个标签");

            if (!r.IsFinished)
            {
                return null;
            }

            return await AddKeyword(r.Value, id, service);
        }
    }
}