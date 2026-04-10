// ----------------------------------------------------------
//            文件：Function_MessageBuilder.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月10日 20:53
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Domain.IM.Pages
{
    partial class LobbyViewModel
    {
        IMMessageVPO GetVPO(MSG msg)
        {
            if (msg is PlainTextIMMessage im_msg_plainText)
            {
                return new PlainTextMSG(this, im_msg_plainText);
            }

            if (msg is ImageIMMessage im_msg_image)
            {
                return new ImageMSG(this, im_msg_image);
            }

            return null;
        }

        internal BrainHoleAndCreative GetGroup(string groupID) => null;
    }
}