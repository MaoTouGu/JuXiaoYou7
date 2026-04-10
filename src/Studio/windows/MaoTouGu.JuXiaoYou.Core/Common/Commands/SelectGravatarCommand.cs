// ----------------------------------------------------------
//            文件：SelectGravatarCommand.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年03月18日 22:24
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using MaoTouGu.JuXiaoYou.Services.Imaging;
using MaoTouGu.Studio.Database;

namespace MaoTouGu.JuXiaoYou.Common.Commands
{
    public sealed class SelectGravatarCommand(JuXiaoYouPage target) : ContextCommand<IGravatarTarget, JuXiaoYouPage>(target)
    {
        protected override async void Execute(IGravatarTarget target)
        {
            var r   = await GravatarSystem.PickGravatar(Context);
            var api = Ioc.SafeGet<IWebApi>();

            if (!r.IsFinished)
            {
                return;
            }

            var result   = r.Value;
            var oldValue = target.GetGravatar();


            if (!string.IsNullOrEmpty(oldValue))
            {
                //
                // 删除旧的文件。
                var root     = ImageSystem.RootPath;
                var imgs     = DirectoryExt.Combine(root, "Images");
                var fileName = Path.Combine(imgs, oldValue);

                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }

            }

            try
            {

                var ms = new MemoryStream(result.Buffer);
                var r2 = await api.UploadGravatar(result.Id, ms);


                if (r2.IsFinished)
                {

                    GUI.RunOnUIThread(() =>
                    {
                        target.SetGravatar(result.Id);
                    });

                    if (target is User usr)
                    {
                        //
                        // 更新
                        await api.UpdateUserAsync(usr);
                    }
                    else if (target is Moniker moniker)
                    {
                        await DatabaseManager.GetService<MonikerService>()
                                             .Update(moniker);
                    }

                }
                else
                {
                    Context.Warning("错误", r2.Reason);
                }

                await ms.DisposeAsync();
            }
            catch(Exception e)
            {
                Context.Warning("错误", e.Message);
                throw;
            }
        }
    }
}