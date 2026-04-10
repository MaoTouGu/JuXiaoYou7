// ----------------------------------------------------------
//            文件：CFElementCollection.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月23日 22:29
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------


namespace MaoTouGu.JuXiaoYou.Services.CFE
{
    public sealed class CFElementCollection : ViewList<CFElement>, ICloneable<CFElementCollection>
    {
        public CFElementCollection() : base(){}
        
        public CFElementCollection(List<CFElement> collection) : base(collection, true){}
        
        public CFElementCollection Clone()
        {
            var list       = new List<CFElement>();
            var collection = new CFElementCollection(list);
            
            
            list.AddRange(Items.Select(x => x.Clone()));

            return collection;
        }
    }
}