// ----------------------------------------------------------
//            文件：BySettingFilterMethod.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月09日 20:44
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Indexing.BySetting
{
    public abstract class BySettingFilterMethod : FilterMethod
    {
        public static readonly List<BySettingFilterMethod> Methods = new List<BySettingFilterMethod>
        {
            new StringEQ(),
            new StringContains(),
            new NumericRange(),
            new NumericGT(),
            new NumericGTE(),
            new NumericLT(),
            new NumericLTE(),
            new KeywordIntersection(),
        };

        public static BySettingFilterMethod Get(CustomFilter filter)
        {
            return Methods.FirstOrDefault(x => x.CanHandle(filter))
                         ?.Create(filter);
        }

        protected abstract bool CanHandle(CustomFilter filter);

        protected abstract BySettingFilterMethod Create(CustomFilter filter);
    }

    public abstract class BySettingFilterMethod<T, S> : BySettingFilterMethod where T : CustomFilter
                                                                              where S : BySettingFilterMethod<T, S>, new()
    {
        protected sealed override bool CanHandle(CustomFilter filter)
        {
            return filter is T;
        }

        protected override BySettingFilterMethod Create(CustomFilter filter)
        {
            return new S
            {
                CustomFilter = (T)filter,
            };
        }

        public T CustomFilter { get; init; }
    }
    public abstract class NumericMethod<T, S> : BySettingFilterMethod<T, S> where T : CustomFilter
                                                                            where S : BySettingFilterMethod<T, S>, new()
    {
        protected int GetValue(Moniker x, string key)
        {
            if (x.Settings is null)
            {
                return -1;
            }

            if (!x.Settings.TryGetValue(key, out var v))
            {
                return -1;
            }

            if (int.TryParse(v, out var n))
            {
                return n;
            }

            return -1;
        }
    }
}