// ----------------------------------------------------------
//            文件：ImageMSG.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月10日 20:57
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Domain.IM.Models
{
    public class ImageMSG: IMMessageVPO
    {
        public ImageMSG(LobbyViewModel context, ImageIMMessage msg) : base(context, msg)
        {
            Target = msg;
        }
        
        public string Source => Target.Source;

        public ImageIMMessage Target { get; }
    }
}