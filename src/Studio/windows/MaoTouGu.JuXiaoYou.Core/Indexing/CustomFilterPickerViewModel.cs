// ----------------------------------------------------------
//            文件：CustomFilterPickerViewModel.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月09日 21:45
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Indexing
{
    public class CustomFilterPickerViewModel : ObjectRoot<CustomFilter>
    {
        public CustomFilterPickerViewModel()
        {
            Filters = new ViewList<CustomFilter>
            {
                new SettingEQFilter { Id           = ID.Get(), Name = "文本筛选（全等)", },
                new SettingContainsFilter { Id     = ID.Get(), Name = "文本筛选（包含）" },
                new SettingRangeFilter { Id        = ID.Get(), Name = "数值筛选（范围）" },
                new SettingGTFilter { Id           = ID.Get(), Name = "数值筛选（＞）" },
                new SettingGTEFilter { Id          = ID.Get(), Name = "数值筛选（≥）" },
                new SettingLTFilter { Id           = ID.Get(), Name = "数值筛选（＜）" },
                new SettingLTEFilter { Id          = ID.Get(), Name = "数值筛选（≤）" },
                new KeywordIntersectionFilter { Id = ID.Get(), Name = "标签（交集）", Keywords = new ViewList<string>() },
            };

            Filter = Filters.FirstOrDefault();
        }

        protected override bool CanFinish() => Filter is not null;

        protected override CustomFilter OnFinish(bool edit) => Filter;

        private CustomFilter _filter;

        public ViewList<CustomFilter> Filters { get; }

        public CustomFilter Filter
        {
            get => _filter;
            set => TryFinishAndSetValue(ref _filter, value);
        }
    }
}