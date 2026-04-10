// ----------------------------------------------------------
//            文件：ResourceLock.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月06日 19:08
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.Studio.Core
{
    public class ResourceLock
    {
        public void Refresh()
        {
            TimeOfExpires = TimeOfExpires + TimeSpan.FromMinutes(30);
        }

        public bool IsExpired => TimeOfExpires < TimeOfCreated;
        
        public string   DocumentID    { get; init; }
        public string   OwnerID       { get; init; }
        public DateTime TimeOfCreated { get; init; }
        public DateTime TimeOfExpires { get; set; }
    }
}