// ----------------------------------------------------------
//            文件：MonikerTransitViewModel.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月08日 20:37
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using MaoTouGu.JuXiaoYou.Workspaces.Common;

namespace MaoTouGu.JuXiaoYou.Visualizers.Pages
{
    public class MonikerTransitViewModel : NestedPage, IKeywordTarget
    {
        public MonikerTransitViewModel(Moniker item, JuXiaoYouPage parent) : base(item, parent)
        {
            Moniker  = item;
            Keywords = new ViewList<Keyword>();
            Title = $"中转页：{item.Name}";

            AddFavorite    = new AddFavoriteCommand(this);
            SelectGravatar = new SelectGravatarCommand(this);
        }

        protected override async void OnStart()
        {
            var r = await MonikerHelper.FindKeyword(Moniker.Id);

            //
            //
            Keywords.AddMany(r, true);
        }

        public async Task AddKeyword()
        {
            var r = await MonikerHelper.AddKeyword(this, Moniker.Id);

            if (r is null)
            {
                return;
            }

            Keywords.Add(r);
        }

        public async Task RemoveKeyword(Keyword keyword)
        {
            if (keyword is null)
            {
                return;
            }

            if (!await this.RemoveThis())
            {
                return;
            }

            await MonikerHelper.RemoveKeyword(Moniker.Id);
            Keywords.Add(keyword);
        }

        public ViewList<Keyword> Keywords { get; }

        public Moniker Moniker { get; }

        public ICommandEX SelectGravatar { get; }
        public ICommandEX AddFavorite    { get; }
    }
}