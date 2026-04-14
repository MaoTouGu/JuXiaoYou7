// ----------------------------------------------------------
//            文件：AsyncCollectionPage`1.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年03月29日 23:04
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using MaoTouGu.Studio.Database;

namespace MaoTouGu.JuXiaoYou.AppModels
{
    public abstract class AsyncCollectionPage<T, TService> : SystemPage, ICollectionPage<T>
        where T : DatabaseObject
        where TService : AsyncCollectionService<T>
    {
        private T _selected;

        protected AsyncCollectionPage()
        {
            Service    = GetService<TService>();
            Collection = Service.Collection;
            Add        = new DelegateCommand(DoAddCommand);
            Edit       = new DelegateCommand<T>(DoEditCommand, DBHelper.NotNull);
            Remove     = new DelegateCommand<T>(DoRemoveCommand, DBHelper.NotNull);
        }

        //------------------------------------------------------------
        //
        //                 OnException / OnLogging
        //
        //------------------------------------------------------------
        protected virtual void OnSelectedChanged(T value)
        {

        }

        protected override async void OnStart()
        {
            await Service.Start();

            //
            //
            Selected = Collection.FirstOrDefault();
        }

        //------------------------------------------------------------
        //
        //                 OnException / OnLogging
        //
        //------------------------------------------------------------
        private async void DoAddCommand()
        {
            var r = await this.SingleLine("新建", "新建世界");

            if (!r.IsFinished)
            {
                return;
            }

            var item = OnAddingItem(r.Value);

            await Service.Add(item);

            Selected = item;
        }

        private async void DoEditCommand(T target)
        {
            await OnEditingItem(target);
        }

        private async void DoRemoveCommand(T target)
        {
            if (!await this.RemoveThis())
            {
                return;
            }

            await Service.Remove(target);
            OnRemovingItem(target);
        }

        //------------------------------------------------------------
        //
        //                 OnException / OnLogging
        //
        //------------------------------------------------------------
        protected abstract T OnAddingItem(string name);
        protected abstract Task OnEditingItem(T target);
        protected virtual void OnRemovingItem(T target)
        {

        }

        //------------------------------------------------------------
        //
        //                 OnException / OnLogging
        //
        //------------------------------------------------------------
        public T Selected
        {
            get => _selected;
            set
            {
                SetValue(ref _selected, value);

                if (_selected is not null)
                {
                    OnSelectedChanged(_selected);
                }

                Edit.RaiseUpdate();
                Remove.RaiseUpdate();
            }
        }

        //------------------------------------------------------------
        //
        //                 OnException / OnLogging
        //
        //------------------------------------------------------------

        public ICommandEX Add    { get; }
        public ICommandEX Edit   { get; }
        public ICommandEX Remove { get; }


        public TService Service { get; }

        public ViewList<T> Collection { get; }
    }
}