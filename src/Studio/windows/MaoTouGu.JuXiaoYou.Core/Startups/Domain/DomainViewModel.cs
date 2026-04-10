// ----------------------------------------------------------
//            文件：DomainViewModel.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年03月29日 22:50
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------

using MaoTouGu.JuXiaoYou.Indexing;

namespace MaoTouGu.JuXiaoYou.Startups
{
    public class DomainViewModel : AsyncCollectionPage<Domain, DomainService>
    {
        public DomainViewModel()
        {
        }

        protected override async void OnStart()
        {
            base.OnStart();

            await Task.Delay(2000);
            // await this.Object(new IndexingOptionViewModel());
        }

        protected override Domain OnAddingItem(string name) => new Domain
        {
            Id   = ID.Get(),
            Name = name,

        };

        protected override async Task OnEditingItem(Domain target)
        {
            var r = await this.Object(new DomainEditorViewModel(target));

            if (r.IsFinished && r.Value)
            {
                await Service.Update(target);
            }
        }

        public ICommandEX MoveUp   { get; }
        public ICommandEX MoveDown { get; }
    }
}