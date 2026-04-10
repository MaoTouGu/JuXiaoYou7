// ----------------------------------------------------------
//            文件：Function_Received.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月10日 19:26
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------

namespace MaoTouGu.JuXiaoYou.Domain.IM.Pages
{
    public sealed partial class LobbyViewModel
    {
        Task Received(MSG msg)
        {
            return Task.Run(() =>
                            {
                                GUI.RunOnUIThread(() =>
                                                  {
                                                      if (string.IsNullOrEmpty(msg.GroupID))
                                                      {
                                                          HandleMessageReceive(C2CMsgCollection, msg);
                                                      }
                                                      else
                                                      {
                                                          
                                                      }
                                                  });
                            });
        }

        void HandleMessageReceive(ViewList<IMMessageVPO> collection, MSG msg)
        {
            var vpo = GetVPO(msg);

            if (vpo is null)
            {
                return;
            }
            
            collection.Add(vpo);
        }
    }
}