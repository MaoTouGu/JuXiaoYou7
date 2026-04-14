// ----------------------------------------------------------
//            文件：CollectionVisualizerOptions.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月12日 17:34
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using MaoTouGu.Shells.Inputs;

namespace MaoTouGu.JuXiaoYou.Visualizers
{
    [SuppressMessage("ReSharper", "ExplicitCallerInfoArgument")]
    public abstract class CollectionVisualizerOptions<TSetting, TView, TItem> : VisualizerOptions<TSetting, TView>
        where TSetting : UserControl
        where TView : VisualizerControl
        where TItem : DatabaseObject
    {
        private bool  _isEdit;
        private TItem _item;

        protected CollectionVisualizerOptions()
        {
            Collection = new ViewList<TItem>();

            Back     = new DelegateCommand(DoBackCommand);
            Add      = new DelegateCommand(DoAddCommand);
            Previous = new DelegateCommand<TItem>(DoPreviousCommand, DBHelper.NotNull);
            Next     = new DelegateCommand<TItem>(DoNextCommand, DBHelper.NotNull);
            Edit     = new DelegateCommand<TItem>(DoEditCommand, DBHelper.NotNull);
            Remove   = new DelegateCommand<TItem>(DoRemoveCommand, DBHelper.NotNull);
        }

        /*******************************************************************
         *
         *
         *
         *
         *
         *******************************************************************/
        private void Item_OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            FireStructureChanged();
        }


        /*******************************************************************
         *
         *
         *
         *
         *
         *******************************************************************/

        private async void DoAddCommand()
        {
            var r = await ViewModel.SingleLine("创建", "创建选项。");

            if (!r.IsFinished)
            {
                return;
            }

            //
            //
            var item = CreateItem(r.Value);

            if (item is null)
            {
                return;
            }

            //
            //
            Collection.Add(item);

            //
            // 订阅事件。
            item.PropertyChanged += Item_OnPropertyChanged;
        }

        private void DoBackCommand()
        {
            Item   = null;
            IsEdit = false;
            FireStructureChanged();
        }

        private void DoEditCommand(TItem item)
        {
            Item   = item;
            IsEdit = true;
        }

        private void DoPreviousCommand(TItem item)
        {
            var index = Collection.IndexOf(item);

            if (index < 0)
            {
                return;
            }

            if (index - 1 < 0)
            {
                index = Collection.Count - 1;
            }
            else
            {
                index -= 1;
            }

            Item   = Collection[index];
            IsEdit = true;
        }

        private void DoNextCommand(TItem item)
        {
            var index = Collection.IndexOf(item);

            if (index < 0)
            {
                return;
            }

            index = (index + 1) % Collection.Count;

            Item   = Collection[index];
            IsEdit = true;
        }

        private async void DoRemoveCommand(TItem item)
        {
            if (!await ViewModel.RemoveThis())
            {
                return;
            }

            Item   = item;
            IsEdit = true;

            //
            //
            Collection.Remove(item);
            FireStructureChanged();

            //
            // 取消订阅事件。
            item.PropertyChanged -= Item_OnPropertyChanged;
        }


        /*******************************************************************
         *
         *
         *
         *
         *
         *******************************************************************/
        protected abstract TItem CreateItem(string name);


        /*******************************************************************
         *
         *
         *
         *
         *
         *******************************************************************/

        public ViewList<TItem> Collection { get; }

        [BsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public ICommandEX Previous { get; }

        [BsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public ICommandEX Next { get; }

        [BsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public bool IsEdit
        {
            get => _isEdit;
            set => SetValue(ref _isEdit, value);
        }

        [BsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public TItem Item
        {
            get => _item;
            set
            {
                SetValue(ref _item, value);
                Edit.RaiseUpdate();
                Remove.RaiseUpdate();
            }
        }

        [BsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public ICommandEX Add { get; }

        [BsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public ICommandEX Back { get; }

        [BsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public ICommandEX Edit { get; }

        [BsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public ICommandEX Remove { get; }
    }
}