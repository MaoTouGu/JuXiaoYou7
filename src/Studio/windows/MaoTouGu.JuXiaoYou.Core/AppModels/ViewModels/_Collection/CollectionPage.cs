// ----------------------------------------------------------
//            文件：CollectionPage.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年03月29日 23:48
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.AppModels
{
    public abstract class CollectionPage<T, TService> : SystemPage, ICollectionPage<T>
        where T : DatabaseObject
        where TService : DataService<T>
    {
        private T _selected;

        protected CollectionPage()
        {
            Collection = new ViewList<T>();
            Service    = GetService<TService>();

            Add = new DelegateCommand(DoAddCommand);
        }

        protected virtual void OnSelectedChanged(T value)
        {

        }

        private async void DoAddCommand()
        {
            var r = await this.SingleLine("新建", "新建世界");

            if (!r.IsFinished)
            {
                return;
            }

            var item = OnAddingItem(r.Value);

            await Service.Add(item);
            Collection.Add(item);

            Selected = item;
        }

        protected abstract T OnAddingItem(string name);

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
            }
        }


        public ICommandEX Add    { get; }
        public ICommandEX Edit   { get; }
        public ICommandEX Remove { get; }


        public TService Service { get; }

        public ViewList<T> Collection { get; }
    }
}