// ----------------------------------------------------------
//            文件：IndexSystem.Subordinate.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年03月18日 20:15
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------

namespace MaoTouGu.JuXiaoYou.Indexing
{
    partial class IndexSystem
    {

        public static async Task Subordinating(DatabaseObject target, string domain, string name)
        {
            var item = new Subordinate
            {
                Id         = ID.Get(),
                Name       = name,
                Domain     = domain,
                DocumentID = target.Id,
            };

            await UniqueReferenceService.Add(item);
        }

        public static async Task Subordinating(DatabaseObject target, string domain, string subject, string name)
        {
            var item = new Subordinate
            {
                Id         = ID.Get(),
                Name       = name,
                Domain     = domain,
                DocumentID = target.Id,
                Subject    = subject,
            };

            await UniqueReferenceService.Add(item);
        }

        public static async Task RebuildSubordinates(string domain, string subject)
        {
            //
            //
            var subordinates = UniqueReferenceService.Find(domain, subject);

            foreach (var subordinate in subordinates.Where(subordinate => MonikerService.IsDeleted(subordinate.DocumentID))
                                                    .ToList())
            {
                await UniqueReferenceService.Remove(subordinate.Id);
            }
        }

        public static async Task RemoveSubordinate(DatabaseObject target)
        {
            var items = UniqueReferenceService.FindById(target.Id);

            foreach (var item in items)
            {
                await UniqueReferenceService.Remove(item);
            }
        }
    }
}