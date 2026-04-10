// ----------------------------------------------------------
//            文件：UserChangeSpot.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月06日 16:50
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------

namespace MaoTouGu.Studio.Database.Spots
{
    public class UserChangeSpot : Spot
    {
        public string UserID     { get; init; }
        
        public string OldName     { get; init; }
        public string NewName     { get; init; }
        public string OldGravatar { get; init; }
        public string NewGravatar { get; init; }
    }
}