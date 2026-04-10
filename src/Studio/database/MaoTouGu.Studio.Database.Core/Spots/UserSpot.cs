// ----------------------------------------------------------
//            文件：UserSpot.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月06日 16:47
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.Studio.Database.Spots
{
    public class UserSpot : Spot
    {
        public string        UserID    { get; init; }
        public DataOperation Operation { get; init; }
    }
}