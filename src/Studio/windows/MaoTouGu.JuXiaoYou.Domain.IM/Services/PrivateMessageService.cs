// ----------------------------------------------------------
//            文件：PrivateMessageService.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月06日 16:37
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Domain.IM.Services
{
    public class PrivateMessageService : DataService<MSG>
    {
        public PrivateMessageService()  : base(DbName, ColName_Private)
        {
            DbSet.EnsureIndex(nameof(MSG.SubjectID));
            DbSet.EnsureIndex(nameof(MSG.GroupID));
        }

        // public async Task<IEnumerable<MSG>> GetMessageCollectionAsync(string subjectID)
        // {
        //     var r = await FindAllAsync();
        //
        //     return r.Where(x => x.SubjectID == subjectID);
        // }

    }
}