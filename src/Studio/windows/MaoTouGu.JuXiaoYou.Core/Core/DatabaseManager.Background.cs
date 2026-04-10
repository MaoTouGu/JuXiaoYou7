// ----------------------------------------------------------
//            文件：DatabaseManager.Background.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月06日 16:35
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Core
{
    partial class DatabaseManager
    {
        
        
        public static async Task Handle(Spot dataEvent)
        {
            if (dataEvent is not DataChangedSpot dcs)
            {
                return;
            }

            await Task.Run(() =>
                           {


                               var ivmPI = Ioc.Get<IAppModel>() as IViewModelProvider;

                               ivmPI?.GetContextList()
                                     .OfType<InstancePage>()
                                     .Where(x => x.InstanceID == dcs.DocumentID)
                                     .ForEach(x =>
                                              {
                                                  //
                                                  //
                                                  var usr = Ioc.Get<IUserService>()
                                                               .Dictionary
                                                               .GetValueOrDefault(dcs.HandlerID);
                                                  //
                                                  //
                                                  x.Info("提示", $"{usr?.DisplayName}已经修改了此文档！");
                                              });
                           });

            //
            //
            foreach (var service in Services.OfType<IBackgroundDataSyncService>()
                                            .Where(x => x.CanHandle(dcs.EventID)))
            {
                await service.Handle(dcs);
            }
        }
    }
}