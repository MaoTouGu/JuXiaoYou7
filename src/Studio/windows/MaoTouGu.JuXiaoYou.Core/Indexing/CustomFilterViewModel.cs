// ----------------------------------------------------------
//            文件：CustomFilterViewModel.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月09日 21:39
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Indexing
{
    public sealed class CustomFilterViewModel : ObjectRoot<bool>
    {
        private readonly int _HashCode;

        public CustomFilterViewModel(CustomFilter filter)
        {
            Filter    = filter;
            _HashCode = filter.GetHashCode();
        }

        protected override bool OnFinish(bool edit) => Filter.GetHashCode() != _HashCode;

        public CustomFilter Filter { get; }
    }
}