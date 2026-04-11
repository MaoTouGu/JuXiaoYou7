// ----------------------------------------------------------
//            文件：FilterViewModel.Commands.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月10日 16:34
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Indexing
{
    partial class FilterViewModel
    {
        private async void DoEditCommand(Moniker target)
        {
            if (target is null)
            {
                return;
            }

            await Navigate(new SimpleMonikerSettingViewModel(target));
        }

        private async void DoAddCommand()
        {
            var r = await Method.AddAsync(this);

            if (r is null)
            {
                return;
            }

            

            try
            {

                Moniker = r;
            
                OriginalSource.Add(r);
                Monikers.Add(r);
            }
            catch(Exception e)
            {
                this.Warning("错误", $"删除时遇到错误:\n{e.Message}");
            }
        }

        private async void DoRemoveCommand(Moniker target)
        {
            if (target is null)
            {
                return;
            }

            if (!await this.RemoveThis())
            {
                return;
            }

            try
            {

                await Method.RemoveAsync(this, target);
            
            
                OriginalSource.Remove(target);
                Monikers.Remove(target);
            }
            catch(Exception e)
            {
                this.Warning("错误", $"删除时遇到错误:\n{e.Message}");
            }
        }
    }
}