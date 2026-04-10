// ----------------------------------------------------------
//            文件：ScopedHybridSet.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月06日 14:54
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.Studio.Database.Core
{
    public class ScopedHybridSet<T> : ScopedService<T> where T : DatabaseObject
    {
        public ScopedHybridSet()
        {
            Dictionary = new Dictionary<string, T>();
            Collection = new ViewList<T>();
        }
        

        public override T GetInstance(string documentId) => Dictionary.SafetyGet(documentId);

        protected override void OnAdded(T target)
        {
            base.OnAdded(target);

            if (Dictionary.TryAdd(target.Id, target))
            {
                Collection.Add(target);
            }
        }

        protected override void OnRemoved(T target)
        {
            base.OnRemoved(target);

            if (Dictionary.Remove(target.Id))
            {
                Collection.Remove(target);
            }
        }
        
        public ViewList<T>           Collection { get; }
        public Dictionary<string, T> Dictionary { get; }
    }
}