// ----------------------------------------------------------
//            文件：NumericRange.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月10日 01:41
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Indexing
{

    public class NumericRange : NumericMethod<SettingRangeFilter, NumericRange>
    {
        public override Task Filter(List<Moniker> originalSource, IList<Moniker> collection)
        {
            return Task.Run(() =>
                            {
                                foreach (var moniker in collection.Where(x => !x.IsSoftDeleted))
                                {
                                    var v = GetValue(moniker, CustomFilter.Key);
                                    var r = CustomFilter.Start <= v && CustomFilter.End >= v;

                                    if (CustomFilter.IsReverse)
                                    {
                                        r = !r;
                                    }

                                    if (r)
                                    {
                                        originalSource.Add(moniker);
                                    }
                                }
                            });
        }
    }

    public class NumericGT : NumericMethod<SettingGTFilter, NumericGT>
    {
        public override Task Filter(List<Moniker> originalSource, IList<Moniker> collection)
        {
            return Task.Run(() =>
                            {
                                
                                var iterator = from moniker in collection.Where(x => !x.IsSoftDeleted)
                                    let v = GetValue(moniker, CustomFilter.Key)
                                    where v > CustomFilter.Value
                                    select moniker;
                                originalSource.AddRange(iterator);
                            });
        }
    }

    public class NumericGTE : NumericMethod<SettingGTEFilter, NumericGTE>
    {
        public override Task Filter(List<Moniker> originalSource, IList<Moniker> collection)
        {
            return Task.Run(() =>
                            {
                                var iterator = from moniker in collection.Where(x => !x.IsSoftDeleted)
                                    let v = GetValue(moniker, CustomFilter.Key)
                                    where v >= CustomFilter.Value
                                    select moniker;
                                originalSource.AddRange(iterator);
                            });
        }
    }

    public class NumericLT : NumericMethod<SettingLTFilter, NumericLT>
    {
        public override Task Filter(List<Moniker> originalSource, IList<Moniker> collection)
        {
            return Task.Run(() =>
                            {
                                var iterator = from moniker in collection.Where(x => !x.IsSoftDeleted)
                                    let v = GetValue(moniker, CustomFilter.Key)
                                    where v < CustomFilter.Value
                                    select moniker;
                                originalSource.AddRange(iterator);
                            });
        }
    }

    public class NumericLTE : NumericMethod<SettingLTEFilter, NumericLTE>
    {
        public override Task Filter(List<Moniker> originalSource, IList<Moniker> collection)
        {
            return Task.Run(() =>
                            {
                                var iterator = from moniker in collection.Where(x => !x.IsSoftDeleted)
                                    let v = GetValue(moniker, CustomFilter.Key)
                                    where v <= CustomFilter.Value
                                    select moniker;
                                originalSource.AddRange(iterator);
                            });
        }
    }

    public class KeywordIntersection : NumericMethod<KeywordIntersectionFilter, KeywordIntersection>
    {
        public override Task Filter(List<Moniker> originalSource, IList<Moniker> collection)
        {
            return Task.Run(() =>
                            {
                                var service = DatabaseManager.GetService<KeywordService>();
                                var setList = CustomFilter.Keywords
                                                          .Select(x => GetHashSet(service, x))
                                                          .ToList();
                                var set = IntersectAll(setList);
                                originalSource.AddRange(collection.Where(x => !x.IsSoftDeleted && 
                                                                              set.Contains(x.Id)));
                            });
        }
        
        
        static HashSet<T> IntersectAll<T>(IEnumerable<HashSet<T>> sets)
        {
            if (sets == null) throw new ArgumentNullException(nameof(sets));
            var enumerable = sets as IList<HashSet<T>> ?? sets.ToList();
            if (!enumerable.Any()) return new HashSet<T>();
        
            var result = new HashSet<T>(enumerable.First());
            foreach (var set in enumerable.Skip(1))
            {
                result.IntersectWith(set);
            }
            return result;
        }
        
        static HashSet<string> GetHashSet(KeywordService service, string name)
        {
            return service.FindByName(name)
                          .Select(x => x.DocumentID)
                          .ToHashSet();
        }
    }
}