// ----------------------------------------------------------
//            文件：ScopedCollection.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月06日 14:54
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.Studio.Database.Core
{
    public class ScopedCollection<T> : ScopedService<T> where T : DatabaseObject
    {

        public ScopedCollection()
        {
            Collection = new ViewList<T>();
        }

        public override T GetInstance(string documentId) => Collection.FirstOrDefault(x => x.Id == documentId);

        protected override void OnAdded(T target)
        {
            base.OnAdded(target);
            
            Collection.Add(target);
        }

        protected override void OnRemoved(T target)
        {
            base.OnRemoved(target);

            Collection.Remove(target);
        }

        public ViewList<T> Collection { get; }
    }
}