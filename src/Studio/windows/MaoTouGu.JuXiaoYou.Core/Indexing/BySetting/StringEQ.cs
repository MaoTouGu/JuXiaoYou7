// ----------------------------------------------------------
//            文件：StringEQ.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月10日 01:28
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Indexing.BySetting
{
    public class StringEQ : BySettingFilterMethod<SettingEQFilter, StringEQ>
    {
        public override Task Filter(List<Moniker> originalSource, IList<Moniker> collection)
        {
            return Task.Run(() =>
                            {
                                originalSource.AddRange(
                                                        collection.Where(moniker => !moniker.IsSoftDeleted && 
                                                                                    moniker.ContainSettingItem(CustomFilter.Key)));
                            });
        }
    }
}