// ----------------------------------------------------------
//            文件：FilterViewModel.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月09日 17:38
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using MaoTouGu.JuXiaoYou.Indexing;
using MaoTouGu.Studio.Database;
using Label = MaoTouGu.Studio.Database.References.Label;

namespace MaoTouGu.JuXiaoYou.Indexing
{
    public partial class FilterViewModel : NestedPage
    {
        protected readonly List<Moniker>        OriginalSource;
        protected readonly DisposableCollection DisposableCollection;

        private Moniker _moniker;

        void OnBackgroundEntityAdding(Moniker x)
        {
            //
            // 添加
            Monikers.Add(x);

            // Method.WhenAdding(x);
        }

        void OnBackgroundEntityRemoving(Moniker x)
        {
            //
            // 添加
            Monikers.Remove(x);

            // Method.WhenRemoving(x);
        }


        public FilterMethod   Method         { get; }
        public MonikerService MonikerService { get; }

        public ViewList<Moniker> Monikers { get; }


        public ICommandEX Add    { get; }
        public ICommandEX Edit   { get; }
        public ICommandEX Remove { get; }

        public ICommandEX SetGravatar { get; }

        public Moniker Moniker
        {
            get => _moniker;
            set
            {
                SetValue(ref _moniker, value);
                Edit.RaiseUpdate();
                Remove.RaiseUpdate();
            }
        }
    }
}