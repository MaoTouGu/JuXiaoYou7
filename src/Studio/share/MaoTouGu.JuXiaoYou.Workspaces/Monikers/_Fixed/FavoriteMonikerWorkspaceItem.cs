// ----------------------------------------------------------
//            文件：FavoriteMonikerWorkspaceItem.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月08日 01:59
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------


using MaoTouGu.Studio;

namespace MaoTouGu.JuXiaoYou.Workspaces.Monikers
{
    public sealed class FavoriteMonikerWorkspaceItem : MonikerWorkspaceContainer
    {
        private int _count;

        protected override void OnSetup()
        {
            //
            //
            MonikerService.Favorite
                          .Subscribe(OnMonikerChanged)
                          .DisposeWith(DisposableCollection);
        }

        void OnMonikerChanged(Moniker x)
        {
            if (x.IsSoftDeleted)
            {
                return;
            }

            if (x.IsStar)
            {
                AddOrTrimmed(x);
                Count += 1;
            }
            else
            {
                RemoveOrTrimmed(x);
                Count = Math.Clamp(Count - 1,0, int.MaxValue);
            }
        }


        public override void Initialize(Moniker x)
        {
            if (x.IsSoftDeleted)
            {
                return;
            }

            if (x.IsStar)
            {
                AddOrTrimmed(x);
                Count += 1;
            }

        }

        /// <summary>
        /// 展示的内容，懒得写Binding了。
        /// </summary>
        public string Text => $"星标设定（{_count}）";

        /// <summary>
        /// 设定的数量。
        /// </summary>
        public int Count
        {
            get => _count;
            set
            {
                SetValue(ref _count, value);
                RaiseUpdated(nameof(Count));
                RaiseUpdated(nameof(Text));
            }
        }
    }
}